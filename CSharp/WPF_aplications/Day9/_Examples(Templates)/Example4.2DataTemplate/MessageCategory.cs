using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Example6
{
    public class CategoryMessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MainDataTemplate { get; set; }
        public DataTemplate OtherDataTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Message mes = (Message)item;

            switch (mes.Category)
            {
                case 1:
                    return MainDataTemplate;
                case 2:
                    return OtherDataTemplate;

                default:
                    return null;
            }
        }
    }
}
