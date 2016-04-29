using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СomposerPattern.ComposerClasses
{
    public class CharComponent : AbstractComponent
    {
        public override List<IComponent> PrintAllSentences()
        {
            return null;
        }

        public override string PrintAllWords()
        {
            return null;
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
            
        }
        public override void RemoveComponent(int IndexOfComponent)
        {
            
        }
        public override IComponent GetChildComponent(int IndexOfComponent)
        {
            return null;
        }

        public CharComponent(string NewChar)
        {
            Value = NewChar;
        }
    }
}
