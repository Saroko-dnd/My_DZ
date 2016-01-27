using System;
using System.Windows;

namespace ExampleDependencyProp
{
   public class UserControl : FrameworkElement
    {
        // 1. Создание свойства зависимостей.
        // Идентификатор свойства зависимости - поле представляющее свойство зависимости.
        public static DependencyProperty DataProperty;

        // 2. регистрация свойства зависимостей
        static UserControl()
        {
            // параметр 1: Имя свойства.
            // параметр 2: Тип данных свойства.
            // параметр 3: Тип, которому принадлежит это свойство.
            DataProperty = DependencyProperty.Register("Data", typeof(int), typeof(UserControl));
        }

        // 3. Упаковка свойства зависимостей в традиционное свойство.
        // Методы SetValue и GetValue унаследованы от класса DependencyObject

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


        // для автоматического ввода DependencyProperty
        // вводим propdp и нажимаем Tab

       
        
    }
}