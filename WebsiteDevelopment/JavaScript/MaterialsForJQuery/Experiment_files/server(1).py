/* This software is licensed under a BSD license; see the LICENSE file for details. */

define_ibex_controller({
name: "AcceptabilityJudgment",

jqueryWidget: {
    _init: function () {
        var opts = {
            options:     this.options,
            triggers:    [1],
            children:    [this.options._dashed ? "DashedSentence" : "FlashSentence",
                          this.options._dashed ? {
                                                     s: this.options.s,
                                                     mode: this.options.mode,
                                                     display: this.options.display,
                                                     blankText: this.options.blankText,
                                                     wordTime: this.options.wordTime,
                                                     wordPauseTime: this.options.wordPauseTime,
                                                     sentenceDescType: this.options.sentenceDescType,
                                                     showAhead: this.options.showAhead,
                                                     showBehind: this.options.showBehind
                                                 } :
                                                 {
                                                     s: this.options.s,
                                                     timeout: null, // Already present for 'Question'
                                                     audio: this.options.audio,
                                                     audioMessage: this.options.audioMessage,
                                                     audioTrigger: this.options.audioTrigger
                                                 },
                          this.options._dashed ? "!Question" : (this.options.s.audio ? "*Question" : "Question"),
                          { q:                   this.options.q,
                            as:                  this.options.as,
                            hasCorrect:          dget(this.options, "hasCorrect", false),
                            presentAsScale:      this.options.presentAsScale,
                            presentHorizontally: this.options.presentHorizontally,
                            autoFirstChar:       typeof(this.options.autoFirstChar) == "undefined" ? this.options.presentAsScale : this.options.autoFirstChar,
                            randomOrder:         this.options.randomOrder,
                            showNumbers:         this.options.showNumbers,
                            timeout:             this.options.timeout,
                            instructions:        this.options.instructions,
                            leftComment:         this.options.leftComment,
                            rightComment:        this.options.rightComment }]/*,
            manipulators: [
                [0, function(div) { div.css('font-size', "larger"); return div; }]
            ]*/
        };

        this.element.VBox(opts);
    }
},

properties: {
    obligatory: ["s", "as"],
    htmlDescription:
        function (opts) {
            var s = ibex_controller_get_property("FlashSentence", "htmlDescription")(opts);
            var q = ibex_controller_get_property("Question", "htmlDescription")(opts);
            var p =
                $("<p>")
                .append($("<p>").append("Q: ").append($(q)))
                .append("<br>").append($("<b>").text("S:"))
                .append($(s));
             return p;
        }
}
});


/* This software is licensed under a BSD license; see the LICENSE file for details. */

define_ibex_controller({
    name: "DashedAcceptabilityJudgment",
    jqueryWidget: {
        _init: function () {
            this.options._dashed = true;
            if (this.options.mode === undefined)
                this.options.mode = "speeded acceptability";
            $(this.element).AcceptabilityJudgment(this.options);
        }
    },
    properties: {
        obligatory: ["s", "as"],
        htmlDescription: function (opts) {
            var s = ibex_controller_get_property("DashedSentence", "htmlDescription")(opts);
            var q = ibex_controller_get_property("Question", "htmlDescription")(opts);
            var p =
                $("<p>")
                .append($("<p>").append("Q: ").append($(q)))
                .append("<br>").append($("<b>").text("S:"))
                .append($(s));
            return p;
        }
    }
});

/* This software is licensed under a BSD license; see the LICENSE file for details. */

function boolToInt(x) { if (x) return 1; else return 0; }

define_ibex_controller({
name: "DashedSentence",

jqueryWidget: {
    _init: function() {
        this.cssPrefix = this.options._cssPrefix;
        this.utils = this.options._utils;
        this.finishedCallback = this.options._finishedCallback;

        if (typeof(this.options.s) == "string") {
            // replace all linebreaks (and surrounding space) with 'space-return-space'
            var inputString = this.options.s.replace(/\s*[\r\n]\s*/g, " \r ");
            this.words = inputString.split(/[ \t]+/);
        } else {
            assert_is_arraylike(this.options.s, "Bad value for 's' option of DashedSentence.");
            this.words = this.options.s;
        }
        this.mode = dget(this.options, "mode", "self-paced reading");
        assert(this.mode == "self-paced reading" || this.mode == "speeded acceptability",
               "Value of 'mode' option for DashedSentence controller must be either " +
               "'self-paced reading' or 'speeded acceptability'.");
        this.display = dget(this.options, "display", "dashed");
        this.blankText = dget(this.options, "blankText", "\u2014\u2014"/*mdash*/);
        this.wordTime = dget(this.options, "wordTime", this.display == "in place" ? 400 : 300); // Only for speeded accpetability.
        this.wordPauseTime = dget(this.options, "wordPauseTime", this.display == "in place" ? 0 : 100); // Ditto.
        this.showAhead = dget(this.options, "showAhead", true);
        this.showBehind = dget(this.options, "showBehind", true);
        assert(this.display == "dashed" || this.display == "in place",
               "Value of 'display' option for DashedSentence controller must be either " +
               "'dashed' (default) or 'in place'.");

        this.currentWord = 0;

        // Is there a "stopping point" specified?
        this.stoppingPoint = this.words.length;
        for (var i = 0; i < this.words.length; ++i) {
            if (stringStartsWith("@", this.words[i])) {
                this.words[i] = this.words[i].substring(1);
                this.stoppingPoint = i + 1;
                break;
            }
        }

        this.hideUnderscores = dget(this.options, "hideUnderscores", true);
        if (this.hideUnderscores) {
            this.words = $.map(this.words, function(word) { return word.replace(/_/g, ' ') });
        }

        this.mainDiv = $("<div>");
        this.element.append(this.mainDiv);

        this.background = this.element.css('background-color') || "white";
        this.isIE7;
        /*@cc_on this.isIE = true; @*/
        if (this.isIE)
            this.background = "white";

        // Defaults.
        this.unshownBorderColor = dget(this.options, "unshownBorderColor", "#9ea4b1");
        this.shownBorderColor = dget(this.options, "shownBorderColor", "black");
        this.unshownWordColor = dget(this.options, "unshownWordColor", this.background);
        this.shownWordColor = dget(this.options, "shownWordColor", "black");

        // Precalculate MD5 of sentence.
        this.sentenceDescType = dget(this.options, "sentenceDescType", "literal");
        assert(this.sentenceDescType == "md5" || this.sentenceDescType == "literal", "Bad value for 'sentenceDescType' option of DashedSentence.");
        if (this.sentenceDescType == "md5") {
            var canonicalSentence = this.words.join(' ');
            this.sentenceDesc = hex_md5(canonicalSentence);
        }
        else {
	    if (typeof(this.options.s) == "string")
		this.sentenceDesc = csv_url_encode(this.options.s);
	    else
		this.sentenceDesc = csv_url_encode(this.options.s.join(' '));
        }

        this.mainDiv.addClass(this.cssPrefix + "sentence");

        this.resultsLines = [];
        if (this.mode == "self-paced reading") {
            // Don't want to be allocating arrays in time-critical code.
            this.sprResults = [];
            for (var i = 0; i < this.words.length; ++i)
                this.sprResults[i] = new Array(2);
        }
        this.previousTime = null;

        if (this.display == "in place") {
            this.wordSpan = $(document.createElement("span")).text(this.blankText);
            if (conf_centerItems) {
                this.mainDiv.css('text-align', 'center');
                this.wordSpan.css('text-align', 'center');
            }
            this.mainDiv.append(this.wordSpan);

            this.blankWord = this.blankWord_inplace;
            this.showWord = this.showWord_inplace;
        }
        else { // dashed
            this.blankWord = this.blankWord_dashed;
            this.showWord = this.showWord_dashed;

            this.wordISpans = []; // Inner spans.
            this.wordOSpans = []; // Outer spans.
            this.owsnjq = []; // 'outer word spans no jQuery'.
            this.iwsnjq = []; // 'inner word spans no jQuery'.
            for (var j = 0; j < this.words.length; ++j) {
                if ( this.words[j] == "\r" ) {
                    this.mainDiv.append('<br/>');

                    if (j <= this.stoppingPoint)
                        this.stoppingPoint--;
                    
                    continue;
                }

                var ispan;
                var ospan = $(document.createElement("span"))
                            .addClass(this.cssPrefix + 'ospan')
                            .append(ispan = $(document.createElement("span"))
                                            .addClass(this.cssPrefix + 'ispan')
                                            .text(this.words[j]));
                if (! this.showAhead)
                    ospan.css('border-color', this.background);
                this.mainDiv.append(ospan);
                if (j + 1 < this.words.length)
                    this.mainDiv.append("&nbsp; ");
                this.wordISpans.push(ispan);
                this.wordOSpans.push(ospan);
                this.iwsnjq.push(ispan[0]);
                this.owsnjq.push(ospan[0]);
            }
        }

        if (this.mode == "speeded acceptability") {
            this.showWord(0);
            var t = this;
            function wordTimeout() {
                t.blankWord(t.currentWord);
                ++(t.currentWord);
                if (t.currentWord >= t.stoppingPoint)
                    t.finishedCallback([[["Sentence (or sentence MD5)", t.sentenceDesc]]]);
                else
                    t.utils.setTimeout(wordPauseTimeout, t.wordPauseTime);
            }
            function wordPauseTimeout() {
                t.showWord(t.currentWord);
                t.utils.clearTimeout(wordPauseTimeout);
                t.utils.setTimeout(wordTimeout, t.wordTime);
            }
            this.utils.setTimeout(wordTimeout, this.wordTime);
        }
        else if (this.mode == "self-paced reading") {
            var t = this;
            // Inlining this to minimize function calls in code for updating screen after space is pressed.
/*            function goToNext(time) {
                t.recordSprResult(time, t.currentWord);

                if (t.currentWord - 1 >= 0)
                    t.blankWord(t.currentWord - 1);
                if (t.currentWord < t.stoppingPoint)
                    t.showWord(t.currentWord);
                ++(t.currentWord);
                if (t.currentWord > t.stoppingPoint) {
                    t.processSprResults();
                    t.finishedCallback(t.resultsLines);
                }

                return false;
            }*/

            this.safeBind($(document), 'keydown', function(e) {
                var time = new Date().getTime();
                var code = e.keyCode;

                if (code == 32) {
                    // *** goToNext() ***
//                    t.recordSprResult(time, t.currentWord);
                    var word = t.currentWord;
                    if (word > 0 && word <= t.stoppingPoint) {
                        var rs = t.sprResults[word-1];
                        rs[0] = time;
                        rs[1] = t.previousTime;
                    }
                    t.previousTime = time;

                    if (t.currentWord - 1 >= 0)
                        t.blankWord(t.currentWord - 1);
                    if (t.currentWord < t.stoppingPoint)
                        t.showWord(t.currentWord);
                    ++(t.currentWord);
                    if (t.currentWord > t.stoppingPoint) {
                        t.processSprResults();
                        t.finishedCallback(t.resultsLines);
                    }
                    return false;
                    // ***
                }
                else {
                    return true;
                }
            });

            // For iPhone/iPod touch -- add button for going to next word.
            if (isIPhone) {
                var btext = dget(this.options, "iPhoneNextButtonText", "next");
                var next = $("<div>")
                           .addClass(this.cssPrefix + "iphone-next")
                           .text(btext);
                this.element.append(next);
                next.click(function () {
                    var time = new Date().getTime();

                    // *** goToNext() ***
                    //t.recordSprResult(time, t.currentWord);
                    var word = t.currentWord;
                    if (word > 0 && word < t.stoppingPoint) {
                        var rs = t.sprResults[word-1];
                        rs[0] = time;
                        rs[1] = t.previousTime;
                    }
                    t.previousTime = time;

                    if (t.currentWord - 1 >= 0)
                        t.blankWord(t.currentWord - 1);
                    if (t.currentWord < t.stoppingPoint)
                        t.showWord(t.currentWord);
                    ++(t.currentWord);
                    if (t.currentWord > t.stoppingPoint) {
                        t.processSprResults();
                        t.finishedCallback(t.resultsLines);
                    }

                    return false;
                    // ***
                });
            }
        }
    },

    // Not using JQuery in these two methods just in case it slows things down too much.
    // NOTE: [0] subscript gets DOM object from JQuery selector.
    blankWord_dashed: function(w) {
        if (this.currentWord <= this.stoppingPoint) {
            this.owsnjq[w].style.borderColor = this.unshownBorderColor;
            this.iwsnjq[w].style.visibility = "hidden";
            if (! this.showBehind)
                this.owsnjq[w].style.borderColor = this.background;
        }
    },
    showWord_dashed: function(w) {
        if (this.currentWord < this.stoppingPoint) {
            if (this.showAhead || this.showBehind)
                this.owsnjq[w].style.borderColor = this.shownBorderColor;
            this.iwsnjq[w].style.visibility = "visible";
        }
    },

    blankWord_inplace: function (w) {
        if (this.wordPauseTime > 0 && this.currentWord <= this.stoppingPoint) {
            this.wordSpan.empty();
        }
    },
    showWord_inplace: function (w) {
        if (this.currentWord < this.stoppingPoint) {
            this.wordSpan.text(this.words[this.currentWord]);
        }
    },

    // Inlining this now.
    /*recordSprResult: function(time, word) {
        if (word > 0 && word < this.stoppingPoint) {
            var rs = this.sprResults[word-1];
            rs[0] = time;
            rs[1] = this.previousTime;
        }
        this.previousTime = time;
    },*/

    processSprResults: function () {
        var nonSpaceWords = [];
        for (var i = 0; i < this.words.length; ++i) {
        	if ( this.words[i] != "\r" )
	            nonSpaceWords.push(this.words[i]);
        }

        for (var i = 0; i < nonSpaceWords.length; ++i) {
            this.resultsLines.push([
                ["Word number", i+1],
                ["Word", csv_url_encode(nonSpaceWords[i])],
                ["Reading time", this.sprResults[i][0] - this.sprResults[i][1]],
                ["Newline?", (! this.display == "in place") &&
                             boolToInt(((i+1) < this.wordOSpans.length) &&
                             (this.wordOSpans[i].offset().top != this.wordOSpans[i+1].offset().top))],
                ["Sentence (or sentence MD5)", this.sentenceDesc]
            ]);
        }
    }
},

properties: {
    obligatory: ["s"],
    htmlDescription: function (opts) {
        return $(document.createElement("div")).text(opts.s);
    }
}
});


/* This software is licensed under a BSD license; see the LICENSE file for details. */

(function () {
var soundId = 0;

define_ibex_controller({
name: "FlashSentence",

init: function () {

},

jqueryWidget: {
    _init: function () {
        var self = this;
        self.cssPrefix = self.options._cssPrefix;

        var $loading;
        var doneLoading = false;
        if (typeof(self.options.s) != "string") {
            if (self.options.s.audio) {
                if (self.options.audioMessage) {
                    self.element.append($loading = $("<div>").addClass(self.cssPrefix + 'loading'));
                    setTimeout(function () {
                        if (! doneLoading)
                            $loading.text(conf_loadingMessage);
                    }, 250);
                }
                withSoundManager(completeInit);
            }
            else {
                self.sentenceDom = htmlCodeToDOM(self.options.s)
                completeInit();
            }
        }
        else {
            self.sentenceDom = $("<div>").text(self.options.s);
            completeInit();
        }
        this.sentenceDescType = dget(this.options, "sentenceDescType", "literal");
        assert(this.sentenceDescType == "literal", "Bad value for 'sentenceDescType' option of FlashSentence controller ('md5' no longer supported).");

        function completeInit(sm) {
            if (sm) {
                if (self.options.audioMessage) {
                    if (typeof(self.options.audioMessage) != "string") {
                        self.sentenceDom = $(htmlCodeToDOM(self.options.audioMessage));
                    }
                    else {
                        self.sentenceDom = $("<div>").text(self.options.audioMessage);
                    }
                }
                var names = null;
                if ($.isArray(self.options.s.audio))
                    names = self.options.s.audio;
                else
                    names = [self.options.s.audio];
                var urls = [ ];
                for (var i = 0; i < names.length; ++i) {
                    if (names[i].match(/^(?:https?)|(?:ftps?):\/\//))
                        urls.push(names[i]);
                    else
                        urls.push(__server_py_script_name__ + '?resource=' + escape(names[i]));
                }
                var sids = [ ];
                for (var i = 0; i < names.length; ++i)
                    sids.push(soundId++);
                for (var i = 0; i < names.length; ++i)
                    sm.createSound('sound' + sids[i], urls[i]);

                var nextSoundToPlayIndex = 0;

                function hideSD() { if (self.sentenceDom) self.sentenceDom.hide(); }
                if (self.options.audioTrigger == "click") {
                    self.sentenceDom.css('cursor', 'pointer');
                    self.sentenceDom.click(function () {
                        hideSD();
                        sm.play('sound' + sids[nextSoundToPlayIndex++], { onfinish: fin });
                    });
                }
                else { // Immediate
                    hideSD();
                    sm.play('sound' + sids[nextSoundToPlayIndex++], { onfinish: fin });
                }

                function fin() {
                    if (nextSoundToPlayIndex >= names.length)
                        setTimeout(function () { self.finishedCallback([[["Sentence (or sentence MD5)", self.sentenceMD5]]]); }, 200);
                    else
                        sm.play('sound' + sids[nextSoundToPlayIndex++], { onfinish: fin });
                }
            }

            self.finishedCallback = self.options._finishedCallback;
            self.utils = self.options._utils;

            self.timeout = dget(self.options, "timeout", 2000);

            self.sentenceMD5 = csv_url_encode(self.options.s.html ? self.options.s.html : (self.options.s.audio+'' ? self.options.s.audio+'' : (self.options.s+'')));

            self.element.addClass(self.cssPrefix + "flashed-sentence");
            if (self.sentenceDom) {
                if ($loading) {
                    doneLoading = true;
                    $loading.replaceWith(self.sentenceDom)
                }
                else
                    self.element.append(self.sentenceDom);
            }

            if (self.timeout) {
                self.utils.setTimeout(function() {
                    self.finishedCallback([[["Sentence (or sentence MD5)", self.sentenceMD5]]]);
                }, self.timeout);
            }
            else if (! self.options.s.audio) {
                // Give results without actually finishing.
                if (self.utils.setResults)
                    self.utils.setResults([[["Sentence (or sentence MD5)", self.sentenceMD5]]]);
            }
        }
    }
},

properties: {
    obligatory: ["s"],
    htmlDescription: function (opts) {
        return $(document.createElement("div")).text(opts.s)[0];
    }
}
});

})();


/* This software is licensed under a BSD license; see the LICENSE file for details. */

define_ibex_controller({
name: "Form",

jqueryWidget: {
    _init: function () {
        this.cssPrefix = this.options._cssPrefix;
        this.finishedCallback = this.options._finishedCallback;
        this.utils = this.options._utils;

        this.html = dget(this.options, "html");
        this.continueOnReturn = dget(this.options, "continueOnReturn", false);
        this.continueMessage = dget(this.options, "continueMessage", "Click here to continue");
        this.checkedValue = dget(this.options, "checkedValue", "yes");
        this.uncheckedValue = dget(this.options, "uncheckedValue", "no");
        this.validators = dget(this.options, "validators", { });
        this.errorCSSClass = dget(this.options, "errorCSSClass", "error");
        this.saveReactionTime = dget(this.options, "saveReactionTime", false);
        this.obligatoryErrorGenerator =
            dget(this.options, "obligatoryErrorGenerator",
                 function (field) { return "The \u2018" + field + "\u2019 field is obligatory."; });
        this.obligatoryCheckboxErrorGenerator =
            dget(this.options, "obligatoryCheckboxErrorGenerator",
                 function (field) { return "You must check the " + field + " checkbox to continue."; });
        this.obligatoryRadioErrorGenerator =
            dget(this.options, "obligatoryRadioErrorGenerator",
                 function (field) { return "You must select an option for \u2018" + field + "\u2019."; });

        var t = this;

        function alertOrAddError(name, error) {
            var ae = $("label." + escape(t.errorCSSClass) + "[for=__ALL_FIELDS__]");
            if (ae.length > 0) {
                ae.addClass(t.cssPrefix + "error-text").text(error);
                return;
            }

            var e = $("label." + escape(t.errorCSSClass) + "[for=" + escape(name) + "]");
            if (e.length > 0)
                e.addClass(t.cssPrefix + "error-text").text(error);
            else 
                alert(error);
        }

        var HAS_LOADED = false;

        function handleClick(dom) {
            return function (e) {
                var answerTime = new Date().getTime();

                e.preventDefault();
                if (! HAS_LOADED) return;

                // Get rid of any previous errors.
                $("." + t.cssPrefix + "error-text").empty();

                var rlines = [];

                var inps = $(dom).find("input[type=text]");
                var tas = $(dom).find("textarea");
                for (var i = 0; i < tas.length; ++i) { inps.push(tas[i]); }

                for (var i = 0; i < inps.length; ++i) {
                    var inp = $(inps[i]);

                    if (inp.hasClass("obligatory") && ((! inp.attr('value')) || inp.attr('value').match(/^\s*$/))) {
                        alertOrAddError(inp.attr('name'), t.obligatoryErrorGenerator(inp.attr('name')));
                        return;
                    }

                    if (t.validators[inp.attr('name')]) {
                        var er = t.validators[inp.attr('name')](inp.attr('value'));
                        if (typeof(er) == "string") {
                            alertOrAddError(inp.attr('name'), er);
                            return;
                        }
                    }

                    rlines.push([["Field name", csv_url_encode(inp.attr('name'))],
                                 ["Field value", csv_url_encode(inp.attr('value'))]]);
                }

                var checks = $(dom).find("input[type=checkbox]");
                for (var i = 0; i < checks.length; ++i) {
                    var check = $(checks[i]);
 
                    // Checkboxes with the 'obligatory' class must be checked.
                    if (! check.attr('checked') && check.hasClass('obligatory')) {
                        alertOrAddError(check.attr('name'), t.obligatoryCheckboxErrorGenerator(check.attr('name')));
                        return;
                    }

                    rlines.push([["Field name", check.attr('name')],
                                 ["Field value", check.attr('checked') ? t.checkedValue : t.uncheckedValue]]);
                }

                var rads = $(dom).find("input[type=radio]");
                // Sort by name.
                var rgs = { };
                for (var i = 0; i < rads.length; ++i) {
                    var rad = $(rads[i]);
                    if (rad.attr('name')) {
                        if (! rgs[rad.attr('name')])
                            rgs[rad.attr('name')] = [];
                        rgs[rad.attr('name')].push(rad);
                    }
                }
                for (k in rgs) {
                    // Check if it's oblig.
                    var oblig = false;
                    var oneIsSelected = false;
                    var oneThatWasSelected;
                    var val;
                    for (var i = 0; i < rgs[k].length; ++i) {
                        if (rgs[k][i].hasClass('obligatory')) oblig = true;
                        if (rgs[k][i].attr('checked')) {
                            oneIsSelected = true;
                            oneThatWasSelected = i;
                            val = rgs[k][i].attr('value');
                        }
                    }
                    if (oblig && (! oneIsSelected)) {
                        alertOrAddError(rgs[k][0].attr('name'), t.obligatoryRadioErrorGenerator(rgs[k][0].attr('name')));
                        return;
                    }
                    if (oneIsSelected) {
                        rlines.push([["Field name", rgs[k][0].attr('name')],
                                     ["Field value", rgs[k][oneThatWasSelected].attr('value')]]);
                    }
                }

                if (t.saveReactionTime) {
                    rlines.push([["Field name", "_REACTION_TIME_"],
                                 ["Field value", answerTime - t.creationTime]]);
                }
                t.finishedCallback(rlines);
            }
        }

        var dom = htmlCodeToDOM(this.html, function (dom) {
            HAS_LOADED = true;

            if (t.continueOnReturn) {
                t.safeBind($(dom).find("input[type=text]"), 'keydown', function (e) { if (e.keyCode == 13) { console.log("H"); return handler(e);  } });
            }
        });
        var handler = handleClick(dom);

        this.element.append(dom);

        if (this.continueMessage) {
            this.element.append($("<p>").append($("<a>").attr('href', '').text("\u2192 " + this.continueMessage)
                                                .addClass(ibex_controller_name_to_css_prefix("Message") + "continue-link")
                                                .click(handler)));
        }

        this.creationTime = new Date().getTime();
    }
},

properties: {
    obligatory: ["html"],
    countsForProgressBar: false,
    htmlDescription: function (opts) {
        return htmlCodeToDOM(opts.html);
    }
}
});


/* This software is licensed under a BSD license; see the LICENSE file for details. */

define_ibex_controller({
name: "Message",

jqueryWidget: {
    _init: function () {
        this.cssPrefix = this.options._cssPrefix;
        this.utils = this.options._utils;
        this.finishedCallback = this.options._finishedCallback;

        this.html = this.options.html;
        this.element.addClass(this.cssPrefix + "message");
        this.element.append(htmlCodeToDOM(this.html));

        // Bit of copy/pasting from 'Separator' here.
        this.transfer = dget(this.options, "transfer", "click");
        assert((! this.transfer) || this.transfer == "click" || this.transfer == "keypress" || typeof(this.transfer) == "number",
               "Value of 'transfer' option of Message must either be the string 'click' or a number");

        if (this.transfer == "click") {
            this.continueMessage = dget(this.options, "continueMessage", "Click here to continue.");
            this.consentRequired = dget(this.options, "consentRequired", false);
            this.consentMessage = dget(this.options, "consentMessage", "I have read the above and agree to do the experiment.");
            this.consentErrorMessage = dget(this.options, "consentErrorMessage", "You must consent before continuing.");

            // Add the consent checkbox if necessary.
            var checkbox = null;
            if (this.consentRequired) {
                var names = { };
                var checkbox;
                var message;
                var dom =
                    $(document.createElement("form"))
                    .append($(document.createElement("table"))
                            .css('border', 'none').css('padding', 0).css('margin', 0)
                            .append($(document.createElement("tr"))
                                    .append($(document.createElement("td"))
                                            .css('border', 0).css('padding-left', 0).css('margin-left', 0)
                                            .append(checkbox = $(document.createElement("input"))
                                                    .attr('id', 'consent_checkbox')
                                                    .attr('type', 'checkbox')
                                                    .attr('checked', 0)))
                                    .append(message = $(document.createElement("td"))
                                            .css('border', 0).css('margin-left', 0).css('padding-left', 0)
                                            .append($("<label>")
                                                    .attr('for', 'consent_checkbox')
                                                    .text(this.consentMessage)))));

                this.element.append(dom);
                // Change cursor to pointer when hovering over the message (have to use JS because
                // IE doesn't support :hover for anything other than links).
                message.mouseover(function () {
                    message.css('cursor', "default");
                });
            }

            var t = this;
            // Get a proper lexical scope for the checkbox element so we can capture it in a closure.
            // ALEX: Looking at this again, I don't see why it's necessary to create a local scope here
            // but I am leaving it in as I may be missing something and it won't do any harm.
            (function (checkbox) {
                t.element.append(
                    $(document.createElement("p"))
                    .css('clear', 'left')
                        .append($(document.createElement("a"))
                            .attr('href', '')
                            .addClass(t.cssPrefix + 'continue-link')
                            .text("\u2192 " + t.continueMessage)
                            .click(function () {
                                if ((! checkbox) || checkbox.attr('checked'))
                                    t.finishedCallback();
                                else
                                    alert(t.consentErrorMessage);
                                return false;
                            }))
                );
            })(checkbox);
        }
        else if (this.transfer == "keypress") {
            var t = this;
            this.safeBind($(document), 'keydown', function () {
                t.finishedCallback(null);
                return false;
            });
        }
        else if (typeof(this.transfer) == "number") {
            assert(! this.consentRequired, "The 'consentRequired' option of the Message controller can only be set to true if the 'transfer' option is set to 'click'.");
            this.utils.setTimeout(this.finishedCallback, this.transfer);
        }
    }
},

properties: {
    obligatory: ["html"],
    countsForProgressBar: false,
    htmlDescription: function (opts) {
        return truncateHTML(htmlCodeToDOM(opts.html), 100);
    }
}
});

/* This software is licensed under a BSD license; see the LICENSE file for details. */

//
// TODO: Replace this controller with something that's not such a horrible mess!
// 

(function () {

var __Question_callback__ = null;
var __Questions_answers__ = null;

define_ibex_controller({
name: "Question",

jqueryWidget: {
    _init: function () {
        this.cssPrefix = this.options._cssPrefix;
        this.utils = this.options._utils;
        this.finishedCallback = this.options._finishedCallback;

        var questionField = "Question (NULL if none).";
        var answerField = "Answer";
        var correctField = "Whether or not answer was correct (NULL if N/A)";
        var timeField = "Time taken to answer.";

        this.question = dget(this.options, "q");
        this.answers = this.options.as;

        this.hasCorrect = dget(this.options, "hasCorrect", false);
        // hasCorrect is either false, indicating that there is no correct answer,
        // true, indicating that the first answer is correct, or an integer giving
        // the index of the correct answer, OR a string giving the correct answer.
        // Now we change it to either false or an index.
        if (this.hasCorrect === true)
            this.hasCorrect = 0;
        if (typeof(this.hasCorrect) == "string") {
            var foundIt = false;
            for (var i = 0; i < this.answers.length; ++i) {
                if (this.answers[i].toLowerCase() == this.hasCorrect.toLowerCase()) {
                    this.hasCorrect = i;
                    foundIt = true;
                    break;
                }
            }
            assert(foundIt, "Value of 'hasCorrect' option not recognized in Question");
        }
        this.showNumbers = dget(this.options, "showNumbers", true);
        this.presentAsScale = dget(this.options, "presentAsScale", false);
        this.randomOrder = dget(this.options, "randomOrder", ! (this.hasCorrect === false || this.presentAsScale));
        this.timeout = dget(this.options, "timeout", null);
        this.instructions = dget(this.options, "instructions");
        this.leftComment = dget(this.options, "leftComment");
        this.rightComment = dget(this.options, "rightComment");
        this.autoFirstChar = dget(this.options, "autoFirstChar", false);
        this.presentHorizontally = dget(this.options, "presentHorizontally", false);

        if (! (this.hasCorrect === false))
            assert(typeof(this.hasCorrect) == "number" && this.hasCorrect < this.answers.length,
                   "Bad index for correct answer in Question");

        if (this.randomOrder) {
            assert(typeof(this.answers[0]) != "object",
                  "Cannot set 'randomOrder' option to a list of keys when keys are included with the 'as' option.");
            assert(typeof(this.answers[0]) != "object" || this.answers.length == this.randomOrder.length,
                   "Length of 'randomOrder' doesn't match length of 'as'.");

            this.orderedAnswers = new Array(this.answers.length);
            for (var i = 0; i < this.answers.length; ++i)
                this.orderedAnswers[i] = this.answers[i];
            fisherYates(this.orderedAnswers);
        }
        else {
            this.orderedAnswers = this.answers;
        }

        this.setFlag = function(correct) {
            if (! correct) {
                this.utils.setValueForNextElement("failed", true);
            }
        }

        if (this.question) {
            this.qp = $(document.createElement("p"))
            .addClass(this.cssPrefix + "question-text")
            .css('text-align', conf_centerItems ? 'center' : 'left')
            .append(this.question);
        }
        this.xl = $(document.createElement(((!this.presentAsScale && !this.presentHorizontally) && this.showNumbers) ? "ol" : "ul"))
            .css('margin-left', "2em").css('padding-left', 0);
        __Question_answers__ = new Array(this.answers.length);

        if ((this.presentAsScale || this.presentHorizontally) && this.leftComment) {
            var lcd = $(document.createElement("li"))
                      .addClass(this.cssPrefix + "scale-comment-box")
                      .append(this.leftComment);
            this.xl.append(lcd);
        }
        for (var i = 0; i < this.orderedAnswers.length; ++i) {
            var li;
            li = $(document.createElement("li"));
            if (this.presentAsScale || this.presentHorizontally) {
                li.addClass(this.cssPrefix + "scale-box");
                var t = this;
                 // IE doesn't support :hover for anything other than links, so we
                 // have to use JS.
                 (function (li) {
                     li.mouseover(function () {
                         li.css('border-color', "black")
                           .css('cursor', 'pointer');
                     });
                     li.mouseout(function () {
                         li.css('border-color', "#9ea4b1")
                           .css('cursor', "default");
                     });
                 })(li);
            }
            else {
                li.addClass(this.cssPrefix + "normal-answer");
            }
            (function(i) {
                li.click(function () { __Question_callback__(i); });
            })(i);
            var ans = typeof(this.orderedAnswers[i]) == "string" ? this.orderedAnswers[i] : this.orderedAnswers[i][1];
            var t = this; // 'this' doesn't behave as a lexically scoped variable so can't be
                          // captured in the closure defined below.
            var a = $(document.createElement("span")).addClass(this.cssPrefix + "fake-link");
            __Question_answers__[i] = ans;
            __Question_callback__ = function (i) {
                var answerTime = new Date().getTime();
                var ans = __Question_answers__[i];
                var correct = "NULL";
                if (! (t.hasCorrect === false)) {
                    var correct_ans = typeof(t.answers[t.hasCorrect]) == "string" ? t.answers[t.hasCorrect] : t.answers[t.hasCorrect][1];
                    correct = (ans == correct_ans ? 1 : 0);
                    t.setFlag(correct);
                }
                t.finishedCallback([[[questionField, t.question ? csv_url_encode(t.question) : "NULL"],
                                     [answerField, csv_url_encode(ans)],
                                     [correctField, correct],
                                     [timeField, answerTime - t.creationTime]]]);
            };
            this.xl.append(li.append(a.append(ans)));
        }
        if ((this.presentAsScale || this.presentHorizontally) && this.rightComment) {
            this.xl.append($(document.createElement("li"))
                           .addClass(this.cssPrefix + 'scale-comment-box')
                           .append(this.rightComment));
        }

        if (! (this.qp === undefined))
            this.element.append(this.qp);

        // Again, using tables to center because IE sucks.
        var table = $("<table" + (conf_centerItems ? " align='center'" : "") + ">");
        var tr = $(document.createElement("tr"));
        var td = $("<td" + (conf_centerItems ? " align='center'" : "") + ">")
        if (conf_centerItems)
            td.attr('align', 'center');
        this.element.append(table.append(tr.append(td.append(this.xl))));

        if (this.instructions) {
            this.element.append($(document.createElement("p"))
                                .addClass(this.cssPrefix + "instructions-text")
                                .css('text-align', conf_centerItems ? 'center' : 'left')
                                .text(this.instructions));
        }

        if (this.timeout) {
            var t = this;
            this.utils.setTimeout(function () {
                var answerTime = new Date().getTime();
                t.setFlag(false);
                t.finishedCallback([[[questionField, t.question ? csv_url_encode(t.question) : "NULL"],
                                     [answerField, "NULL"], [correctField, "NULL"],
                                     [timeField, answerTime - t.creationTime]]]);
            }, this.timeout);
        }

        // TODO: A bit of code duplication in this function.
        var t = this;
        this.safeBind($(document), 'keydown', function(e) {
            var code = e.keyCode;
            var time = new Date().getTime();

            var answerTime = new Date().getTime();
            if ((! t.presentAsScale && !t.presentHorizontally) && t.showNumbers &&
                ((code >= 48 && code <= 57) || (code >= 96 && code <= 105))) {
                // Convert numeric keypad codes to ordinary keypad codes.
                var n = code >= 96 ? code - 96 : code - 48;
                if (n > 0 && n <= t.orderedAnswers.length) {
                    var ans = typeof(t.orderedAnswers[n-1]) == "string" ? t.orderedAnswers[n-1] : t.orderedAnswers[n-1][1];
                    var correct = "NULL";
                    if (! (t.hasCorrect === false)) {
                        var correct_ans = typeof(t.answers[t.hasCorrect]) == "string" ? t.answers[t.hasCorrect] : t.answers[t.hasCorrect][1];
                        correct = (correct_ans == ans ? 1 : 0);
                        t.setFlag(correct);
                    }
                    t.finishedCallback([[[questionField, t.question ? csv_url_encode(t.question) : "NULL"],
                                         [answerField, csv_url_encode(ans)],
                                         [correctField, correct],
                                         [timeField, answerTime - t.creationTime]]]);

                    return false;
                }
                else {
                    return true;
                }
            }
            // Letters (and numbers in the case of scales).
            else if ((code >= 65 && code <= 90) || (t.presentAsScale && ((code >= 48 && code <= 57) || (code >= 96 && code <= 105)))) {
                // Convert numeric keypad codes to ordinary keypad codes.
                code = (code >= 96 && code <= 105) ? code - 48 : code;
                var ans = null;

                if (typeof(t.randomOrder) != "object") {
                    for (var i = 0; i < t.answers.length; ++i) {
                        if (t.autoFirstChar && typeof(t.answers[i]) == "string" && code == t.answers[i].toUpperCase().charCodeAt(0)) {
                            ans = t.answers[i];
                            break;
                        }
                        else if ($.isArray(t.answers[i]) && code == t.answers[i][0].toUpperCase().charCodeAt(0)) {
                            ans = t.answers[i][1];
                            break;
                        }
                    }
                }
                else {
                    for (var i = 0; i < t.randomOrder.length; ++i) {
                        if (code == t.randomOrder[i].toUpperCase().charCodeAt(0)) {
                            ans = t.orderedAnswers[i];
                            break;
                        }
                    }
                }

                if (ans) {
                    var correct = "NULL";
                    if (! (t.hasCorrect === false)) {
                        var correct_ans = typeof(t.answers[t.hasCorrect]) == "string" ? t.answers[t.hasCorrect] : t.answers[t.hasCorrect][1];
                        correct = (correct_ans == ans ? 1 : 0);
                        t.setFlag(correct);
                    }
                    t.finishedCallback([[[questionField, t.question ? csv_url_encode(t.question) : "NULL"],
                                         [answerField, csv_url_encode(ans)],
                                         [correctField, correct],
                                         [timeField, answerTime - t.creationTime]]]);
                    
                    return false;
                }
            }

            return true;
        });

        // Store the time when this was first displayed.
        this.creationTime = new Date().getTime();
    }
},

properties: {
    obligatory: ["as"],
    htmlDescription: function(opts) {
        return $(document.createElement("div")).text(opts.q || "");
    }
}
});

})();




(function () {

function norm256(x) {
    if (x < 0)
        return 0;
    else if (x > 255)
        return 255;
    return x;
}

function rgbToS(col) {
    function pad0(s) {
        if (s.length == 1)
            s = '0' + s;
        return s;
    }

    var cols = '#';
    cols += pad0(col[0].toString(16));
    cols += pad0(col[1].toString(16));
    cols += pad0(col[2].toString(16));

    return cols;
}

function parseColor(col) {
    var m;
    if ($.isArray(col)) {
        return col;
    }
    else if (typeof(col) == 'string') {
        if (col.length > 0 && col[0] == '#') {
            var r,g,b;
            if (col.length == 7) {
                r = parseInt(col.substr(1,2), 16);
                g = parseInt(col.substr(3,2), 16);
                b = parseInt(col.substr(5,2), 16);
                if (isNaN(r) || isNaN(g) || isNaN(b))
                    assert(false, "Could not parse color '" + col + "'");
                return [r,g,b];
            }
            else if (col.length == 4) {
                r = parseInt(col.substr(1,1), 16);
                g = parseInt(col.substr(1,1), 16);
                b = parseInt(col.substr(1,1), 16)
                if (isNaN(r) || isNaN(g) || isNaN(b))
                    assert(false, "Could not parse color '" + col + "'");
                return [r,g,b];
            }
            else {
                assert(false, "Could not parse color '" + col + "'");
            }
        }
        else if (m = col.match(/\s*rgb\(\s*([\d.]+)\s*,\s*([\d.]+)\s*,\s*([\d.]+)\)\s*/)) {
            var r = parseFloat(m[1]);
            var g = parseFloat(m[2]);
            var b = parseFloat(m[3]);
            if (isNaN(r) || isNaN(g) || isNaN(b))
                assert(false, "Could not parse color '" + col + "'");
            r = parseInt(Math.round(r * 256.0));
            g = parseInt(Math.round(g * 256.0));
            b = parseInt(Math.round(b * 256.0));
            r = norm256(r);
            g = norm256(g);
            b = norm256(b);
            return [r,g,b];
        }
        else {
            // Support standard HTML color names.
            var map = {
                red: [255,0,0],
                green: [0,255,0],
                blue: [0,0,255],
                aqua: [0,255,255],
                black: [0,0,0],
                fuchsia: [255,0,255],
                gray: [128,128,128],
                lime: [0,255,0],
                maroon: [128,0,0],
                navy: [0,0,128],
                olive: [128,128,0],
                orange: [256,165,0],
                purple: [128,0,128],
                silver: [192,192,192],
                teal: [0, 128,128],
                white: [255,255,255],
                yellow: [255,255,0]
            };
            var u = col.toLowerCase();
            if (map[u])
                return map[u];
            assert(false, "Could not parse color '" + col + "'");
        }
    }
    else {
        assert(false, "Could not parse color '" + col + "'");
    }
}

define_ibex_controller({
name: "Scale",

jqueryWidget: {
    _init: function () {
        var self = this;

        this.cssPrefix = this.options._cssPrefix;
        this.finishedCallback = this.options._finishedCallback;

        this.html = this.options.html;
        this.decimalPlaces = (this.options.decimalPlaces == null ? 2 : this.options.decimalPlaces);
        this.startColor = this.options.startColor ? parseColor(this.options.startColor) : parseColor("#5947FD");
        this.endColor = this.options.endColor ? parseColor(this.options.endColor) : parseColor("#59BAFD");

        this.startValue = this.options.startValue;
        assert(typeof(this.startValue) == "number", "'startValue' option must be a number");
        this.endValue = this.options.endValue;
        assert(typeof(this.endValue) == "number", "'endValue' option must be a number");
        this.buttonMessage = this.options.buttonMessage || "Continue";

        this.$html = htmlCodeToDOM(this.html);
        this.element.append($("<div>").addClass(this.cssPrefix + 'html').append(this.$html));

        this.currentMousePos = { x: 0, y: 0};

        var $bar = $("<div>").addClass(this.cssPrefix + 'bar');
        var $handle = $("<div>").addClass(this.cssPrefix + 'handle');
        var $handleLabel = $("<div>").addClass(this.cssPrefix + 'handle-label');
        var $leftLabel = $("<div>").addClass(this.cssPrefix + 'scale-label');
        var $rightLabel = $("<div>").addClass(this.cssPrefix + 'scale-label');
        this.$bar = $bar;
        this.$handle = $handle;
        this.$handleLabel = $handleLabel;
        this.$leftLabel = $leftLabel;
        this.$rightLabel = $rightLabel;

        this.scaleWidth = this.options.scaleWidth || 300;
        this.scaleHeight = this.options.scaleHeight || 20;
        this.handleWidth = this.options.handleWidth || 30;
        this.handleHeight = this.options.handleHeight || 30;
        this.scaleWidth = parseInt(this.scaleWidth);
        this.scaleHeight = parseInt(this.scaleHeight);
        this.handleWidth = parseInt(this.handleWidth);
        this.handleHeight = parseInt(this.handleWidth);
        $bar.css('width', this.scaleWidth + 'px');
        $bar.css('height', this.scaleHeight + 'px');
        $handle.css({ width: this.handleWith + 'px',
                      height: this.handleHeight + 'px' });

        $bar.append($handle);
        if (this.options.scaleLabels) {
            $bar.append($handleLabel);
            $bar.append($leftLabel);
            $bar.append($rightLabel);

            this.$leftLabel.text(this.startValue.toFixed(this.decimalPlaces));
            this.$rightLabel.text(this.endValue.toFixed(this.decimalPlaces));
        }
        this.element.append($bar);

        this.handleLeft = parseInt(this.scaleWidth / 2);
        this.fraction = 0.5;
        this.setHandlePos();
        $handle.css('background', rgbToS(this.getHandleColor()));

        this.setLinearGradient($bar, this.startColor, this.endColor);

        this.setupDragHandler();
        self.safeBind($bar, 'click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            self.handleBarClick(e);
        });

        this.$button = $("<div>").addClass(this.cssPrefix + 'button');
        this.$button.text(this.buttonMessage);
        this.element.append(this.$button);
        self.safeBind(this.$button, 'click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            self.handleButtonClick(e);
        });

        this.safeBind($(window), 'resize', function (e) {
            self.setHandlePos();
        });
    },

    handleButtonClick: function () {
        var val = (this.fraction * (this.endValue - this.startValue)) + this.startValue;
        //console.log("VAL", val);
        this.finishedCallback([[
            ["html", csv_url_encode(this.$html.innerHTML)],
            ["startValue", this.startValue.toFixed(this.decimalPlaces)],
            ["endValue", this.endValue.toFixed(this.decimalPlaces)],
            ["value", val.toFixed(this.decimalPlaces)]
        ]]);
    },

    handleBarClick: function (e) {
        var self = this;

        // Calculate handle screen position.
        var o = self.$handle.offset();
        var x = o.left;
        var y = o.top;
        x += $(window).scrollLeft();
        y += $(window).scrollTop();

        var clickedBar = false;
        if (e.pageX > x - 4 && e.pageX < x + self.handleWidth + 4) {
            if (e.pageY > y - 4 && e.pageY < y + self.handleHeight + 4)
                clickedBar = true;
        }

        if (! clickedBar) {
            // Move the handle to the position on the bar where user clicked.
            var barLeft = self.$bar.offset().left + $(window).scrollLeft();
            self.handleLeft = e.pageX - barLeft;
            if (self.handleLeft < 0)
                self.handleLeft = 0;
            else if (self.handleLeft > self.scaleWidth)
                self.handleLeft = self.scaleWidth;
            self.setFraction(self.handleLeft);
            self.setHandlePos();
        }
    },

    getBarO: function () {
        var barO = this.$bar.offset();
        var barLeft = barO.left;
        var barTop = barO.top;
        barLeft += $(window).scrollLeft();
        barTop += $(window).scrollTop();
    },

    setFraction: function (x) {
        this.fraction = (x / this.scaleWidth);
    },

    setHandlePos: function () {
        var x = this.fraction * this.scaleWidth;

        var barO = this.$bar.offset();
        var barLeft = barO.left;
        var barTop = barO.top;
        barLeft += $(window).scrollLeft();
        barTop += $(window).scrollTop();
        var hleft = (barLeft + parseInt(x) - parseInt(Math.round(this.handleWidth/2)));
        var htop = (barTop - parseInt(Math.round((this.handleHeight - this.scaleHeight)/2.0)));
        this.$handle.css('left', hleft + 'px');
        this.$handle.css('top', htop + 'px');
        this.$handleLabel.text(this.fraction.toFixed(this.decimalPlaces));
        this.$handleLabel.css('left', parseInt(hleft + this.handleWidth/2 - this.$handleLabel.width()/2) + 'px');
        this.$handleLabel.css('top', parseInt(htop - this.handleHeight) + 'px');
        // Set color for handle.
        var col = this.getHandleColor();
        this.$handle.css('background', rgbToS(col));

        if (this.options.scaleLabels) {
            this.$leftLabel.css('left', parseInt(barLeft - this.$leftLabel.width()/2) + 'px');
            this.$leftLabel.css('top', (barTop + this.handleHeight) + 'px');
            this.$rightLabel.css('left', parseInt(barLeft + this.scaleWidth - this.$rightLabel.width()/2) + 'px');
            this.$rightLabel.css('top', (barTop + this.handleHeight) + 'px');
        }
    },

    getHandleColor: function () {
        var self = this;
        var frac = self.handleLeft / self.scaleWidth;
        var rd = parseInt(Math.round(frac * (self.endColor[0] - self.startColor[0])));
        var gd = parseInt(Math.round(frac * (self.endColor[1] - self.startColor[1])));
        var bd = parseInt(Math.round(frac * (self.endColor[2] - self.startColor[2])));
        var r = self.startColor[0] + rd;
        var g = self.startColor[1] + gd;
        var b = self.startColor[2] + bd;
        r = norm256(r);
        g = norm256(g);
        b = norm256(b);
        return [r,g,b];
    },

    setupDragHandler: function () {
        var self = this;

        var mouseIsDown = false;
        self.currentMousePos = { x: 0, y: 0 };
        var refMousePos = { x: 0, y: 0};
        var alreadyMoved = 0;
        self.safeBind($(document), 'mousemove', function (e) {
            self.currentMousePos.x = e.pageX;
            self.currentMousePos.y = e.pageY;

            if (mouseIsDown) {
                var offset = self.currentMousePos.x - refMousePos.x;
                self.handleLeft += offset - alreadyMoved;
                alreadyMoved = offset;
                if (self.handleLeft < 0)
                    self.handleLeft = 0;
                else if (self.handleLeft >= self.scaleWidth)
                    self.handleLeft = self.scaleWidth;
                self.setFraction(self.handleLeft);
                self.setHandlePos();
            }
        });

        self.safeBind(self.$handle, 'mousedown', function (e) {
            e.preventDefault();
            e.stopPropagation();

            if (! mouseIsDown) {
                mouseIsDown = true;
                refMousePos.x = self.currentMousePos.x;
                refMousePos.y = self.currentMousePos.y;
                alreadyMoved = 0;
            }
        });
        self.safeBind($(document), 'mouseup', function () {
            mouseIsDown = false;
        });
    },

    setLinearGradient: function ($elem, startColor, endColor) {
        var scol = rgbToS(startColor);
        var ecol = rgbToS(endColor);

        $elem.css('filter', "progid:DXImageTransform.Microsoft.Gradient(startColorstr='" + scol + "', endColorstr='" + ecol + "', GradientType=1)");
        $elem.css('background-image', '-ms-linear-gradient(left,' + scol + ' 0%, ' + ecol + ' 100%)');
        $elem.css('background-image', '-webkit-gradient(linear, left rop, right top, color-stop(0, ' + scol + '), color-stop(1,' + ecol + '))');
        $elem.css('background-image', '-webkit-linear-gradient(left, ' + scol + ' 0%,' + ecol + ' 100%)');
        $elem.css('background-image', '-o-linear-gradient(left, ' + scol + ',' + ecol + ')');
        $elem.css('background-image', '-moz-linear-gradient(left, ' + scol + ',' + ecol + ')');
        $elem.css('background-image', 'linear-gradient(to right' + scol + ',' + ecol + ')');
    }
},

properties: {
    obligatory: ["html", "startValue", "endValue"],
    htmlDescription: function(opts) {
        return $(document.createElement("div")).text(opts.s || "");
    }
}
});

})();


/* This software is licensed under a BSD license; see the LICENSE file for details. */

define_ibex_controller({
name: "Separator",

jqueryWidget: {
    _init: function () {
        this.cssPrefix = this.options._cssPrefix;
        this.utils = this.options._utils;
        this.finishedCallback = this.options._finishedCallback;

        this.ignoreFailure = dget(this.options, "ignoreFailure", false);
        this.style = this.ignoreFailure ? "normal" : (this.utils.getValueFromPreviousElement("failed") ? "error" : "normal");
        var x = this.utils.getValueFromPreviousElement("style");
        if (x) this.style = x;
        assert(this.style == "normal" || this.style == "error", "'style' property of Separator must either be 'normal' or 'error'");

        this.transfer = dget(this.options, "transfer", "keypress");
        assert(this.transfer == "keypress" || typeof(this.transfer) == "number",
               "Value of 'transfer' option of Separator must either be the string 'keypress' or a number");

        var normal_message = dget(this.options, "normalMessage", "Press any key to continue.");
        var x = this.utils.getValueFromPreviousElement("normalMessage");
        if (x) normal_message = x;

        var error_message = dget(this.options, "errorMessage", "Wrong. Press any key to continue.");
        var x = this.utils.getValueFromPreviousElement("errorMessage");
        if (x) error_message = x;

        var p = $(document.createElement("p"));
        this.element.append(p);
        if (this.style == "error") {
            this.element.addClass(this.cssPrefix + "next-item-failure-message");
            p.text(error_message);
        }
        else {
            this.element.addClass(this.cssPrefix + "next-item-message");
            p.text(normal_message);
        }

        if (this.transfer == "keypress") {
	    var t = this;
	    this.safeBind($(document), 'keydown', function () {
		t.finishedCallback(null);
		return false;
	    });
        }
        else {
            var t = this;
            this.utils.setTimeout(function () {
                t.finishedCallback(null);
            }, this.transfer);
        }
    }
},

properties: {
    countsForProgressBar: false,
    htmlDescription: function (opts) {
        return $(document.createElement("div")).text(opts.normalMessage)[0];
    }
}
});

/* This software is licensed under a BSD license; see the LICENSE file for details. */

define_ibex_controller({
name: "VBox",

jqueryWidget: {
    _init: function () {
        this._finishedCalledAlready = false;

        this.cssPrefix = this.options.options._cssPrefix;
        this.utils = this.options.options._utils;
        this.finishedCallback = this.options.options._finishedCallback;
        this.controllerDefaults = this.options.options._controllerDefaults;
        this.utilsClass = this.options.options._utilsClass;
        this.callbackWhenChildFinishes = this.options.options._vboxCallbackWhenChildFinishes;

        this.children = this.options.children;
        this.triggers = this.options.triggers;
        this.padding = dget(this.options, "padding", "2em");

        assert_is_arraylike(this.children, "The 'children' option of VBox must be an array");
        assert(this.children.length % 2 == 0, "The 'children' array for VBox must contain an even number of elements");

        assert_is_arraylike(this.triggers, "The 'triggers' option of VBox must be an array");
        assert(this.triggers.length > 0, "The 'triggers' array for VBox must be an array of length > 0");

        var t = this;
        $.each(this.triggers, function (_, tr) {
            assert(typeof(tr) == "number", "The 'triggers' array for VBox must be an array of integers");
            assert(tr >= 0 && tr < t.children.length / 2,
                   "Numbers in the 'triggers' array must be indices into the 'children' array starting from 0");
        });

        this.indicesAndResultsOfThingsThatHaveFinished = [];
//        this.childInstances = [];
        this.childUtils = [];

        this.childElements = new Array(this.children.length / 2);
        for (var i = 0; i < this.children.length; i += 2) {
            var controllerClass = this.children[i];
            var addImmediately = true;
            var removePrevious = false;
            if (controllerClass.charAt(0) == "*" || controllerClass.charAt(0) == "!") {
                addImmediately = false;
                if (controllerClass.charAt(0) == "!")
                    removePrevious = true;
                controllerClass = controllerClass.substr(1);
            }
            var childOptions = this.children[i + 1];
            childOptions = merge_dicts(this.controllerDefaults[controllerClass], childOptions);

            var u = new this.utilsClass(this.utils.getValuesFromPreviousElement());
            this.childUtils.push(u);
            (function(i) {
                u.setResults = function(results) {
                    t.indicesAndResultsOfThingsThatHaveFinished.push([i, results]);
                };
            })(i);

            function makeDiv() {
                var d = $(document.createElement("p")).css('clear', 'both');

                // Call a manipulator if one was supplied.
                if (! (t.options.manipulators === undefined)) {
                    for (var j = 0; j < t.options.manipulators.length; ++j) {
                        if (t.options.manipulators[j][0] == (i / 2))
                            d = t.options.manipulators[j][1](d);
                    }
                }

                // Add padding if requested.
                var dd = null;
                if (t.padding && i > 0) {
                    dd = $(document.createElement("div"))
                        .css('margin-top', t.padding)
                        .css('margin-bottom', 0)
                        .append(d);
                }

                // Wrap in a table if we're centering things.
                var ddd = null;
                if (conf_centerItems) {
                    ddd = $("<table align='center'>");
                    var tr = $(document.createElement("tr"));
                    var td = $(document.createElement("td"));
                    ddd.append(tr.append(td.append(dd ? dd : d)));
                }
                
                // Add the actual child.
                var ac = ddd ? ddd : (dd ? dd : d);

                var l = t.childUtils.length - 1;
                // Get around JavaScript's silly closure capture behavior (deriving
                // from weird variable scoping rules).
                // See http://calculist.blogspot.com/2005/12/gotcha-gotcha.html
                (function(l) {
                    childOptions._finishedCallback = function (r) { t.myFinishedCallback(l, r); };
                    childOptions._cssPrefix = ibex_controller_name_to_css_prefix(controllerClass);
                    childOptions._utils = u;
                    addSafeBindMethodPair(controllerClass);
                    d[controllerClass](childOptions);
                })(l);

                return ac;
            }

            if (addImmediately) {
                var ac = makeDiv()
                this.childElements[i/2] = { child: ac, addImmediately: true, removePrevious: false };
                this.element.append(ac);
            }
            else {
                this.childElements[i/2] = { makeDiv: makeDiv, addImmediately: false, removePrevious: removePrevious };
            }
        }
    },

    myFinishedCallback: function(index, results) {
        if (this._finishedCalledAlready)
            return;

        this.childUtils[index].gc();
        this.indicesAndResultsOfThingsThatHaveFinished.push([index, results]);

        var satisfied = true;
        for (var i = 0; i < this.triggers.length; ++i) {
            var foundIt = false;
            for (var j = 0; j < this.indicesAndResultsOfThingsThatHaveFinished.length; ++j) {
                if (this.indicesAndResultsOfThingsThatHaveFinished[j][0] == this.triggers[i]) {
                    foundIt = true;
                    break;
                }
            }
            if (! foundIt) {
                satisfied = false;
                break;
            }
        }

        if (satisfied) {
            // Merge values for next element.
            var merged = merge_list_of_dicts($.map(this.childUtils,
                                             function (x) { return x.valuesForNextElement; }));
            this.utils.valuesForNextElement = merged;

            this._finishedCalledAlready = true;
            this.finishedCallback(this.concatResults(this.indicesAndResultsOfThingsThatHaveFinished));
        }

        if (this.callbackWhenChildFinishes)
            this.callbackWhenChildFinishes(index, this.childElements[index].child, results);

        // Add next child if it wasn't added at the beginning.
        if (index + 1 < this.childElements.length) {
            var ce = this.childElements[index+1];
            if (! ce.addImmediately) {
                var ac = ce.makeDiv();
                ce.child = ac;
                ce.child.insertAfter(this.childElements[index].child);
                if (ce.removePrevious)
                    this.childElements[index].child.remove();
            }
        }
    },

    concatResults: function(iar) {
        iar = iar.sort(function(x, y) { return x[0] - y[0]; });
        var res = [];
        for (var i = 0; i < iar.length; ++i) {
            if (iar[i][1]) {
                for (var j = 0; j < iar[i][1].length; ++j) {
                    var line = [];
                    for (var k = 0; k < iar[i][1][j].length; ++k)
                        line.push(iar[i][1][j][k]);
                    res.push(line);
                }
            }
        }
        return res;
    }
},

properties: { obligatory: ["children", "triggers"] }

});


/**
 * Purpose/Usage
 * The following snippet ensures that select pages are always loaded "on top",
 * i.e. that the browser window displays the page from the top as opposed to 
 * wherever the previous page left off.
 * To implement this snippet, the user needs to ensure that the html code for the 
 * relevant pages contains a div with the id "top".
 *
 * Rationale/Implementation/Rant
 * This solution is hacky because the initial setup is a bit unusual: technically
 * no new webpages are loaded from the server. Instead, each new page is generated
 * by modifying the html of the previous page. This makes the more conventional
 * checks for element loading/readiness somewhat useless. The existing solution,
 * however, (checking for image loading) was also hacky (e.what if the user 
 * wanted to have a page with no images to scroll up?). In order to improve this,
 * I added an event listener to the document that reacts whenever the document
 * gets modified. This, I think, is both faster than waiting for images to load
 * and also conceptually better fits "the point" of the whole operation.
 * When the listener gets triggered
 *
 * Issues
 * This gets triggered many times during page change because the changes happen
 * incrementally and this event listener is low-level enough to pick up on that.
 * This also means that sometimes the function gets triggered before the previous
 * page had been fully replaced; it picks up on the "top" div and moves the browser
 * window to the top. This can lead to undesirable behavior as
 */
$(document).bind("DOMSubtreeModified", function() {
  // first check if the document contains any elements with the id "top"
  // this is an indication that it is necessary to scroll up
  if ($('#top').length != 0) {
    // if we do need to ensure the page displays on top we check if we are already there
    if ($(document).scrollTop() != 0) {
      // if we aren't at the top, move window to there
      $(document).scrollTop(0)
    }
    console.log('ran')
  }
})



