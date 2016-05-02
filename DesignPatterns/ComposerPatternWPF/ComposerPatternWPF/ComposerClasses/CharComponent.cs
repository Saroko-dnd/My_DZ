using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СomposerPattern.ComposerClasses
{
    public class CharComponent : AbstractComponent
    {
        char value;

        public char Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public override string DeleteAllWords(int WordLength)
        {
            return Value.ToString();
        }

        public override string ChangeAllWords()
        {
            return Value.ToString();
        }

        public override void Parse(string NewString)
        {
            Value = NewString.ToCharArray()[0];
        }

        public override string TextToString()
        {
            return Value.ToString();
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
    }
}
