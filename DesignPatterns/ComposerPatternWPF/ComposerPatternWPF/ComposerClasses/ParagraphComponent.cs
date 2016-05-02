using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace СomposerPattern.ComposerClasses
{
    public class ParagraphComponent : AbstractComponent
    {
        private bool Code = false;
        public override string DeleteAllWords(int WordLength)
        {
            StringBuilder BulderForParagraph = new StringBuilder();
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForParagraph.Append(CurrentComponent.DeleteAllWords(WordLength));
            }
            return BulderForParagraph.ToString();
        }

        public override string ChangeAllWords()
        {
            StringBuilder BulderForParagraph = new StringBuilder();
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForParagraph.Append(CurrentComponent.ChangeAllWords());
            }
            return BulderForParagraph.ToString();
        }

        public override void Parse(string NewString)
        {
            Regex CodeStartRegex = new Regex(@"(.+?({(\s)*?((\r\n)+|(\r\n)*$)))", RegexOptions.Singleline);
            Regex CodeEndRegex = new Regex(@"(\s)*?[}](\s)*?(\r\n)*", RegexOptions.Singleline);
            if (CodeStartRegex.IsMatch(NewString))
            {
                Code = true;
                ChildComponents.Add(new SentenceComponent());
                ChildComponents.Last().Parse(NewString);
            }
            else if (CodeEndRegex.IsMatch(NewString))
            {
                Code = false;
                ChildComponents.Add(new SentenceComponent());
                ChildComponents.Last().Parse(NewString);
            }
            else if (Code)
            {
                ChildComponents.Add(new SentenceComponent());
                ChildComponents.Last().Parse(NewString);
            }
            else
            {
                Regex SentencesRegex = new Regex(@"(([\t\S.\r\n ]+?[.!?])(\s+)((\r\n)*|$))|(.+?((\r\n)+|$))", RegexOptions.Singleline);

                bool FirstTime = true;
                foreach (Match CurrentMatch in SentencesRegex.Matches(NewString))
                {
                    ChildComponents.Add(new SentenceComponent());
                    if (FirstTime)
                    {
                        ChildComponents.Last().Parse(CurrentMatch.Value);
                        FirstTime = false;
                    }
                    else
                    {
                        ChildComponents.Last().Parse(" " + CurrentMatch.Value);
                    }
                }
            }
        }

        public override string TextToString()
        {
            StringBuilder BulderForParagraph = new StringBuilder();
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForParagraph.Append(CurrentComponent.TextToString());
            }
            return BulderForParagraph.ToString();
        }

        public override void AddComponent(IComponent NewComponent)
        {
            ChildComponents.Add(NewComponent);
        }
        public override void RemoveComponent(int IndexOfComponent)
        {
            ChildComponents.RemoveAt(IndexOfComponent);
        }
        public override IComponent GetChildComponent(int IndexOfComponent)
        {
            return ChildComponents[IndexOfComponent];
        }
    }
}
