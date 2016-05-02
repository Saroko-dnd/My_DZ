using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace СomposerPattern.ComposerClasses
{
    public class SentenceComponent : AbstractComponent
    {
        public override string DeleteAllWords(int WordLength)
        {
            StringBuilder BulderForSentence = new StringBuilder();

            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForSentence.Append(CurrentComponent.DeleteAllWords(WordLength));
            }
            return BulderForSentence.ToString();
        }

        public override string ChangeAllWords()
        {
            StringBuilder BulderForSentence = new StringBuilder();

            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForSentence.Append(CurrentComponent.ChangeAllWords());
            }
            return BulderForSentence.ToString();
        }

        public override void Parse(string NewString)
        {          
            Regex WordsRegex = new Regex(@"^(\s+|\d+|\w+|[^\d\s\w])+$");
            Match match = WordsRegex.Match(NewString);
            foreach (Capture capture in match.Groups[1].Captures)
            {
                ChildComponents.Add(new WordComponent());
                ChildComponents.Last().Parse(capture.Value);
            }
        }

        public override string TextToString()
        {
            StringBuilder BulderForSentence = new StringBuilder();

            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForSentence.Append(CurrentComponent.TextToString());
            }
            return BulderForSentence.ToString();
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
