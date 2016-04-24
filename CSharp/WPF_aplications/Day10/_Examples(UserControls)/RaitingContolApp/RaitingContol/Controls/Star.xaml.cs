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
    
    public partial class Star : UserControl
    {
        public static readonly DependencyProperty OnColorProperty;
        
        public static readonly DependencyProperty OffColorProperty;
               
        public static readonly DependencyProperty StateProperty;

        public Star()
        {
            InitializeComponent();
        }

        static Star()
        {
            StateProperty = DependencyProperty.Register
                ("State", 
                typeof(StarState), 
                typeof(Star), 
                new FrameworkPropertyMetadata(StarState.Off, new PropertyChangedCallback(OnStateChanged)));
            
            OnColorProperty = DependencyProperty.Register
               ("Color", 
                typeof(Brush), 
                typeof(Star), 
                new FrameworkPropertyMetadata(Brushes.Yellow, new PropertyChangedCallback(OnStarOnColorChanged)));
            
            OffColorProperty = DependencyProperty.Register
                ("OffColor", 
                typeof(Brush), 
                typeof(Star), 
                new FrameworkPropertyMetadata(Brushes.White, new PropertyChangedCallback(OnStarOffColorChanged),
                new CoerceValueCallback(CoerceOnStarOffColor)));
        }

      
        public event EventHandler<StarStateChangedEventArgs> StateChanged;
             

        
        public Brush OnColor
        {
            get { return (Brush)GetValue(OnColorProperty); }
            set { SetValue(OnColorProperty, value); }
        }

         public Brush OffColor
        {
            get { return (Brush)GetValue(OffColorProperty); }
            set { SetValue(OffColorProperty, value); }
        }

        public StarState State
        {
            get { return (StarState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public bool IsOn
        {
            get { return (this.State == StarState.On); }
        }

       
        private Brush StarFill
        {
            get { return this.pathFill.Fill; }
            set { this.pathFill.Fill = value; }
        }

             

        private void OnGridMouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsOn)
            {
                this.StarFill = OnColor;
            }
        }

        private void OnGridMouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsOn)
            {
                this.StarFill = OffColor;
            }
        }

        private void OnGridMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                State = (State == StarState.On) ? StarState.Off : StarState.On;
            }
        }

        private void OnStateChanged(StarStateChangedEventArgs e)
        {
            if (StateChanged != null)
            {
                StateChanged(this, e);
            }
        }

        private static void OnStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Star star = obj as Star;

            if (star != null)
            {
                StarState newState = (StarState)e.NewValue;

                star.StarFill = (newState == StarState.On) ? star.OnColor : star.OffColor;

                star.OnStateChanged(new StarStateChangedEventArgs(star.State));
            }
        }

        private static void OnStarOnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Star star = obj as Star;
            if (star != null && star.IsOn)
            {
                star.StarFill = star.OnColor;
            }
        }

        private static void OnStarOffColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Star star = obj as Star;
            if (star != null && !star.IsOn)
            {
                star.StarFill = star.OffColor;
            }
        }

        private static object CoerceOnStarOffColor(DependencyObject obj, object value)
        {
            Star star = obj as Star;

            if (star != null)
            {
                Brush brush = (Brush)value;
                if (brush is SolidColorBrush)
                {
                    SolidColorBrush solid = (SolidColorBrush)brush;

                    if (solid.Color == Colors.Transparent)
                    {
                        return Brushes.White;
                    }
                }

                return brush;
            }

            return Brushes.White;
        }

    }

   
    public class StarStateChangedEventArgs : EventArgs
    {
        // Получение состояния перед изменением 
        public StarState OldState { get; private set; }

       
        // Получение состояния после изменения 
        public StarState NewState { get; private set; }

        public StarStateChangedEventArgs(StarState current)
        {
            if (current == StarState.On)
            {
                this.OldState = StarState.Off;
                this.NewState = StarState.On;
            }
            else
            {
                this.OldState = StarState.On;
                this.NewState = StarState.Off;
            }
        }
    }

    public enum StarState
    {
        Off = 0,
        On = 1
    }
}
