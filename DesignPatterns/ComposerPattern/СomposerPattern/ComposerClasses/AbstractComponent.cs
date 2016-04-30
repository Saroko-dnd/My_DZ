using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СomposerPattern.ComposerClasses
{
    public abstract class AbstractComponent : IComponent
    {
        public List<IComponent> ChildComponents = new List<IComponent>();
        public string Value;

        public abstract List<IComponent> PrintAllSentences();


        public abstract string PrintAllWords();

        public abstract void Parse(string NewString);

        public abstract string TextToString();

        public abstract void AddComponent(IComponent NewComponent);

        public abstract void RemoveComponent(int IndexOfComponent);

        public abstract IComponent GetChildComponent(int IndexOfComponent);
    }
}
