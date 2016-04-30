using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string PrintAllWords()
        {
            StringBuilder CurrentStringBuilder = new StringBuilder();
            foreach (IComponent ChildComponent in ChildComponents)
            {
                CurrentStringBuilder.Append(ChildComponent.PrintAllWords());
            }
            return CurrentStringBuilder.ToString();
        }

        public override void Parse(string NewString)
        {

        }

        public override string TextToString()
        {
            return null;
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

        public ParagraphComponent(string NewParagraph)
        {
            Value = NewParagraph;
        }
    }
}
