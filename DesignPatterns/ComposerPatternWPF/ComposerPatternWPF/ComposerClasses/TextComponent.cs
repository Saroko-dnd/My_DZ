﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace СomposerPattern.ComposerClasses
{
    public class Text : AbstractComponent
    {
        //Выводит все предложения в порядке количества слов в них
        public override List<IComponent> PrintAllSentences()
        {
            StringBuilder CurrentStringBuilder = new StringBuilder();
            foreach (IComponent ChildComponent in ChildComponents)
            {
                CurrentStringBuilder.Append(ChildComponent.PrintAllSentences());
            }
            return null;
        }

        public override string ChangeAllWords()
        {
            StringBuilder BulderForText = new StringBuilder();
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForText.Append(CurrentComponent.ChangeAllWords());
            }
            return BulderForText.ToString();
        }

        public override void Parse(string NewString)
        {
            //Regex ParagraphRegex = new Regex(@"(\t)(\S+?)($|\t)", RegexOptions.Singleline);
            string[] ListOfParagraphs = NewString.Split('\t').Where(res => res != string.Empty).ToArray();
            foreach (string CurrentParagraph in ListOfParagraphs)
            {
                ChildComponents.Add(new ParagraphComponent());
                ChildComponents.Last().Parse(CurrentParagraph);
            }
            /*foreach (Match CurrentMatch in ParagraphRegex.Matches(NewString))
            {
                ChildComponents.Add(new ParagraphComponent());
                ChildComponents.Last().Parse(CurrentMatch.Value);
            }*/
        }

        public override string TextToString()
        {
            StringBuilder BulderForText = new StringBuilder();
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForText.Append(CurrentComponent.TextToString());
            }
            return BulderForText.ToString();
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
