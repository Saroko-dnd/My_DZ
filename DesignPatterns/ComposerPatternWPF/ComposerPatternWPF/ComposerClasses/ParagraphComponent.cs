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
            StringBuilder BulderForParagraph = new StringBuilder();
            BulderForParagraph.Append("\t");
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForParagraph.Append(CurrentComponent.ChangeAllWords());
            }
            BulderForParagraph.Append("\r\n");
            return BulderForParagraph.ToString();
        }

        public override void Parse(string NewString)
        {
            Regex SentencesRegex = new Regex(@"(\S.+?[.!?])(\s+|$|(\r\n))", RegexOptions.Singleline);
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

        public override string TextToString()
        {
            StringBuilder BulderForParagraph = new StringBuilder();
            BulderForParagraph.Append("\t");
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForParagraph.Append(CurrentComponent.TextToString());
            }
            BulderForParagraph.Append("\r\n");
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
