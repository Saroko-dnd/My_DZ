using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СomposerPattern.ComposerClasses
{
    public interface IComponent
    {
        /// <summary>
        /// Удаляет все слова заданной длины, начинающиеся на согласную букву
        /// </summary>
        /// <returns></returns>
        string DeleteAllWords(int WordLength);
        /// <summary>
        /// Удаляет из каждого слова в тексте последующие вхождения первой буквы слова
        /// </summary>
        /// <returns></returns>
        string ChangeAllWords();
        void Parse(string NewString);
        string TextToString();
        void AddComponent(IComponent NewComponent);
        void RemoveComponent(int IndexOfComponent);
        IComponent GetChildComponent(int IndexOfComponent);
    }
}