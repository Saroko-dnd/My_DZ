using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СomposerPattern.ComposerClasses
{
    class WordComponent : AbstractComponent
    {
        private static readonly List<char> EnglishConsonants = new List<char>() { 'Q', 'W', 'R', 'T', 'P', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M' };
        private static readonly List<char> RussianConsonants = new List<char>() { 'Й', 'Ц', 'К', 'Н', 'Г', 'Ш', 'Щ', 'З', 'Х', 'Ф', 'В', 'П', 'Р', 'Л', 'Д', 'Ж', 'Ч', 'С', 'М', 'Т','Б' };

        public override string DeleteAllWords(int WordLength)
        {           
            StringBuilder BulderForWord = new StringBuilder();
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                BulderForWord.Append(CurrentComponent.DeleteAllWords(WordLength));
            }
            string CurrentWord = BulderForWord.ToString();
            string CurrentWordUpperCase = CurrentWord.ToUpper();
            if (CurrentWord.Length == WordLength)
            {
                foreach (char CurrentEnglishConsonant in EnglishConsonants)
                {
                    if (CurrentWordUpperCase.StartsWith(CurrentEnglishConsonant.ToString()))
                    {
                        CurrentWord = string.Empty;
                        break;
                    }
                }
                if (CurrentWord != string.Empty)
                {
                    foreach (char CurrentRussianConsonant in RussianConsonants)
                    {
                        if (CurrentWordUpperCase.StartsWith(CurrentRussianConsonant.ToString()))
                        {
                            CurrentWord = string.Empty;
                            break;
                        }
                    }
                }
            }
            return CurrentWord;
        }

        public override string ChangeAllWords()
        {
            StringBuilder BulderForWord = new StringBuilder();
            string LetterForDeleteLowerCase = string.Empty;
            string LetterForDeleteUpperCase = string.Empty;
            foreach (IComponent CurrentComponent in ChildComponents)
            {
                if (LetterForDeleteLowerCase == string.Empty)
                {
                    LetterForDeleteLowerCase = CurrentComponent.ChangeAllWords().ToLower();
                    LetterForDeleteUpperCase = CurrentComponent.ChangeAllWords();
                    BulderForWord.Append(CurrentComponent.ChangeAllWords());
                }
                else
                {
                    if (CurrentComponent.ChangeAllWords() != LetterForDeleteLowerCase && CurrentComponent.ChangeAllWords() != LetterForDeleteUpperCase)
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
