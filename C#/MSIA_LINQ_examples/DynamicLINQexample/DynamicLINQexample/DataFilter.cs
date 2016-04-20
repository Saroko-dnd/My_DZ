using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DynamicLINQexample
{
    public class DataFilter
    {
        private string nameOfFilterTextBox = string.Empty;
        private string nameOfFilterComboBox = string.Empty;
        private string propertyName = string.Empty;
        private BinaryOperator currentBinaryOperator = null;
        private object CurrentConstant = null;

        public string PropertyName
        {
            get
            {
                return propertyName;
            }

            set
            {
                propertyName = value;
            }
        }

        public BinaryOperator CurrentBinaryOperator
        {
            get
            {
                return currentBinaryOperator;
            }

            set
            {
                currentBinaryOperator = value;
            }
        }

        public object Constant
        {
            get
            {
                return CurrentConstant;
            }

            set
            {
                CurrentConstant = value;
            }
        }

        public string NameOfFilterTextBox
        {
            get
            {
                return nameOfFilterTextBox;
            }

            set
            {
                nameOfFilterTextBox = value;
            }
        }

        public string NameOfFilterComboBox
        {
            get
            {
                return nameOfFilterComboBox;
            }

            set
            {
                nameOfFilterComboBox = value;
            }
        }

        public DataFilter (string NewPropertyName, BinaryOperator NewBinaryOperator, Object NewConstant, string NewNameOfFilterTextBox, string NewNameOfFilterComboBox)
        {
            PropertyName = NewPropertyName;
            CurrentBinaryOperator = NewBinaryOperator;
            Constant = NewConstant;
            NameOfFilterTextBox = NewNameOfFilterTextBox;
            NameOfFilterComboBox = NewNameOfFilterComboBox;
        }
    }
}
