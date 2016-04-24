using System;
using System.Windows;
using System.Windows.Controls;

namespace Example3RoutedEvents
{
    // Для работы с маршрутизируемыми событиями, класс должен быть наследником UIElement
    class ExtraButton : Button
    {
        // Маршрутизируемое событие.

        public static RoutedEvent MyButtonClickEvent;

        // Статический конструктор, в котором регистрируется событие.
        static ExtraButton()
        {
            // Регистрация события с помощью EventManager.

            MyButtonClickEvent = EventManager.RegisterRoutedEvent(
                "MyButtonClick",            // 1 параметр: имя события.
                RoutingStrategy.Bubble,     // 2 параметр: тип маршрута. (Поднимающийся, туннельный, прямой)
                typeof(RoutedEventHandler), // 3 параметр: тип делегата, который будет задавать сигнатуру обработчика.
                typeof(ExtraButton));       // 4 параметр: класс-владелец события.
            
         
            #region Типы маршрутов
            // RoutingStrategy.Bubble - событие идет от самого последнего (вложенного элемента) до родительского верхнего уровня.
            // RoutingStrategy.Tunnel - событие идет от самого верхнего элемента (от родительского до дочернего)
            // RoutingStrategy.Direct - событие для одного элемента.
            #endregion
        }

        // Обертка для маршрутизируемого события.

        public event RoutedEventHandler MyButtonClick
        {
            add    { AddHandler(MyButtonClickEvent,    value); }
            remove { RemoveHandler(MyButtonClickEvent, value); }
        }

        // Переопределение метода, который срабатывает при нажатии на кнопку.
        protected override void OnClick()
        {
            base.OnClick();
            // Аргумент, который будет передан обработчику события.
            RoutedEventArgs args = new RoutedEventArgs(ExtraButton.MyButtonClickEvent, this);
         
          
            // Вызов события. 
            // Генерирует перенаправленное событие MyButtonClickEvent,
            // которое идентифицируется через указанный RoutedEventArgs
            RaiseEvent(args); 

            // Событие, которое должно быть вызвано, 
            // определяется по параметрам объекта типа RoutedEventArgs

        }
    }
}


