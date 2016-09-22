using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryGraphicsBuilder.DiagramCodeBehind
{
    public class DynamicKeyValuePair
    {
        public int MaxIntValue;
        public int MinIntValue;
        private string Key_ = string.Empty;

        public string Key
        {
            get { return Key_; }
            set { Key_ = value; }
        }
        private int Value_;

        public int Value
        {
            get { return Value_; }
            set { Value_ = value; }
        }

        public DynamicKeyValuePair(string NewKey, int NewValue, int NewMaxIntValue, int NewMinIntValue )
        {
            Key = NewKey;
            Value = NewValue;
            MaxIntValue = NewMaxIntValue;
            MinIntValue = NewMinIntValue;
        }
    }
}
