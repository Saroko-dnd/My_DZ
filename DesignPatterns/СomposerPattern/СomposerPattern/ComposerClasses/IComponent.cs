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
        /// Выводит все слова текста в аклфавитном порядке по первой букве
        /// </summary>
        /// <returns></returns>
        string PrintAllWords();
        void Parse(string NewString);
        string TextToString();
        void AddComponent(IComponent NewComponent);
        void RemoveComponent(int IndexOfComponent);
        IComponent GetChildComponent(int IndexOfComponent);
    }
}
