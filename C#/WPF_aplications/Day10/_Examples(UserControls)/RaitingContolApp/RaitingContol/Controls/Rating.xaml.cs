using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RatingContol.Controls
{
   
    public partial class Rating : UserControl
    {

        // Текущее значение рейтинга 
        public static readonly DependencyProperty ValueProperty;
        // Максимум
        public static readonly DependencyProperty MaximumProperty;
        // Минимум
        public static readonly DependencyProperty MinimumProperty;
        // Цвет выбранных элементов
        public static readonly DependencyProperty StarOnColorProperty;
        // Цвет невыбранных элементов
        public static readonly DependencyProperty StarOffColorProperty;

        public Rating()
        {
            InitializeComponent();

            this.Loaded += this.Rating_Loaded;
        }

        static Rating()
        {
            // регистрация свойств зависимостей
            ValueProperty = DependencyProperty.Register
               ("Value", 
                typeof(int), 
                typeof(Rating), 
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnValueChanged), 
                new CoerceValueCallback(CoerceValueValue)));
           
            MaximumProperty = DependencyProperty.Register
                ("Maximum", 
                typeof(int), 
                typeof(Rating),
                new FrameworkPropertyMetadata(5));
            
            MinimumProperty = DependencyProperty.Register
                ("Minimum", 
                typeof(int), 
                typeof(Rating), 
                new FrameworkPropertyMetadata(0));
            
            StarOnColorProperty = DependencyProperty.Register(
                "StarOnColor", 
                typeof(Brush), 
                typeof(Rating), 
                new FrameworkPropertyMetadata(Brushes.Yellow, new PropertyChangedCallback(OnStarOnColorChanged)));
            
            StarOffColorProperty = DependencyProperty.Register(
                "StarOffColor", 
                typeof(Brush), 
                typeof(Rating), 
                new FrameworkPropertyMetadata(Brushes.White, new PropertyChangedCallback(OnStarOffColorChanged)));
        }


        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }


        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }


        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }


        public Brush StarOnColor
        {
            get { return (Brush)GetValue(StarOnColorProperty); }
            set { SetValue(StarOnColorProperty, value); }
        }


        public Brush StarOffColor
        {
            get { return (Brush)GetValue(StarOffColorProperty); }
            set { SetValue(StarOffColorProperty, value); }
        }

        // Событие изменение рейтинга 
        public event EventHandler<RatingChangedEventArgs> RatingChanged;

        // Вызов события RatingChanged
        private void OnRatingChanged()
        {
            if (RatingChanged != null)
            {
                RatingChanged(this, new RatingChangedEventArgs(this.Value));
            }
        }

        // метод, выполняющийся при изменении занчения Value
        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Rating rating = obj as Rating;

            if (rating != null)
            {
                rating.OnRatingChanged();
            }
        }

        // Метод для корректировки значения Value
        private static object CoerceValueValue(DependencyObject obj, object value)
        {
            Rating rating = (Rating)obj;

            int current = (int)value;

            if (current < rating.Minimum)
            {
                current = rating.Minimum;
            }

            if (current > rating.Maximum)
            {
                current = rating.Maximum;
            }

            return current;
        }

        // метод, выполняющийся при изменении значения StarOffColor
        private static void OnStarOffColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Rating rating = obj as Rating;

            if (rating != null)
            {
                Brush offColor = (Brush)e.NewValue;

                foreach (Star star in rating.stackPanelStars.Children)
                {
                    star.OffColor = offColor;
                }
            }
        }
        // метод, выполняющийся при изменении значения StarOnColor
        private static void OnStarOnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Rating rating = obj as Rating;

            if (rating != null)
            {
                Brush onColor = (Brush)e.NewValue;

                foreach (Star star in rating.stackPanelStars.Children)
                {
                    star.OnColor = onColor;
                }
            }
        }

        // Загрузка 
        private void Rating_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeStars();
        }

        // События 
        private void star_MouseLeave(object sender, MouseEventArgs e)
        {
            Star star = (Star)sender;

            if (!star.IsOn)
            {
                int current = (int)star.Tag;
                int value;

                foreach (Star str in this.stackPanelStars.Children)
                {
                    DisableStateChange(str);

                    value = (int)str.Tag;

                    if (value < current && value > Value)
                    {
                        str.State = StarState.Off;
                    }

                    EnableStateChange(str);
                }
            }
        }

        private void star_MouseEnter(object sender, MouseEventArgs e)
        {
            Star star = (Star)sender;

            if (!star.IsOn)
            {
                int current = (int)star.Tag;
                int value;

                foreach (Star str in this.stackPanelStars.Children)
                {
                    DisableStateChange(str);

                    value = (int)str.Tag;

                    if (value < current)
                    {
                        str.State = StarState.On;
                    }

                    EnableStateChange(str);
                }
            }
        }

        private void star_StateChanged(object sender, StarStateChangedEventArgs e)
        {
            Star star = (Star)sender;

            int current = (int)star.Tag;

            bool reset = (current < Value);

            int value;

            foreach (Star str in this.stackPanelStars.Children)
            {
                value = (int)str.Tag;

                DisableStateChange(str);

                if (value < current)
                {
                    str.State = StarState.On;
                }
                else if (value > current)
                {
                    str.State = StarState.Off;
                }
                else if (value == current && reset)
                {
                    str.State = StarState.On;
                }

                EnableStateChange(str);
            }

            this.Value = current;
        }


        // Инициализация (заполнение stackPanelStars объектами класса Star)
        private void InitializeStars()
        {
            this.stackPanelStars.Children.Clear();

            int value = 1;

            for (int i = 0; i < this.Maximum; i++)
            {
                Star star = new Star();
                star.OnColor = this.StarOnColor;
                star.OffColor = this.StarOffColor;
                star.Tag = value;
                
                // подписка на события 
                star.StateChanged += star_StateChanged;
                star.MouseEnter += star_MouseEnter;
                star.MouseLeave += star_MouseLeave;

                value++;

                this.stackPanelStars.Children.Insert(i, star);
            }
        }

        // подписка на события 
        private void EnableStateChange(Star str)
        {
            str.StateChanged += this.star_StateChanged;
        }
        // отписка от событий 
        private void DisableStateChange(Star str)
        {
            str.StateChanged -= this.star_StateChanged;
        }

        

    }

     
    // 
    public class RatingChangedEventArgs
    {
        public int Value { get; private set; }

        public RatingChangedEventArgs(int value)
            : base()
        {
            this.Value = value;
        }
    }
}
