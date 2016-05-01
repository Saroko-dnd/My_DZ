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

        public abstract List<IComponent> PrintAllSentences();

        public abstract string ChangeAllWords();

        public abstract void Parse(string NewString);

        public abstract string TextToString();

        public abstract void AddComponent(IComponent NewComponent);

        public abstract void RemoveComponent(int IndexOfComponent);

        public abstract IComponent GetChildComponent(int IndexOfComponent);
    }
}