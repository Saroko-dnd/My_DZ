using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СomposerPattern.ComposerClasses
{
    class WordComponent : AbstractComponent
    {
        public override List<IComponent> PrintAllSentences()
        {
            return null;
        }

        public override string ChangeAllWords()
        {
            StringBuilder BulderForWord = new StringBuilder();
            string LetterForDelete = string.Empty;
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                if (LetterForDelete == string.Empty)
                {
                    LetterForDelete = CurrentComponent.ChangeAllWords();
                    BulderForWord.Append(CurrentComponent.ChangeAllWords());
                }
                else
                {
                    if (CurrentComponent.ChangeAllWords() != LetterForDelete)
                    {
                        BulderForWord.Append(CurrentComponent.ChangeAllWords());
                    }
                }

            }
            return BulderForWord.ToString();
        }

        public override void Parse(string NewString)
        {
            foreach (char CurrentChar in NewString.ToCharArray())
            {
                ChildComponents.Add(new CharComponent());
                ChildComponents.Last().Parse(CurrentChar.ToString());
            }
        }

        public override string TextToString()
        {
            StringBuilder BulderForWord = new StringBuilder();
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForWord.Append(CurrentComponent.TextToString());
            }
            return BulderForWord.ToString();
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
