using System;
using System.Windows;

namespace Example2DependencyProp
{
   public class UserControl : FrameworkElement
    {
        public static DependencyProperty DataProperty;

      
        static UserControl()
        {
            // Объект указывает:
            // 1. значение по умолчанию
            // 2. службы используемые вместе со свойством зависимостей (привязка данных, анимации и т.д.)
            // FrameworkPropertyMetadataOptions.NotDataBindable - не поддерживает привязку данных
            // FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata((int)50);


            // параметр 1: Имя свойства.
            // параметр 2: Тип данных свойства.
            // параметр 3: Тип, которому принадлежит это свойство.
            // параметр 4: Метаданные
            // параметр 5: Метод для проверки корректности значения.
            DataProperty = DependencyProperty.Register( "Data", 
                                                        typeof(int), 
                                                        typeof(UserControl),
                                                        metadata,
                                                        new ValidateValueCallback(Validate));
        }

         public int Data
        {
            get
            {
                return (int)GetValue(DataProperty);
            }
            set
            {
                SetValue(DataProperty, value);
            }
        }


        //  метод ля проверки значений
        static bool Validate(object value)
        {
            if ((int)value <0)
                return false;
            return true;
        }

       
        
    }
}