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
        /// Выводит все предложения текста в порядке возрастания количества слов в них
        /// </summary>
        /// <returns></returns>
        List<IComponent> PrintAllSentences();
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