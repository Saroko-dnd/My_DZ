using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string[] ListOfParagraphs = NewString.Split('\r','\n');
            foreach (string CurrentString in ListOfParagraphs)
            {
                ParagraphComponent
                ChildComponents.Add(new ParagraphComponent(CurrentString + "\r\n"));
            }
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
    }
}
