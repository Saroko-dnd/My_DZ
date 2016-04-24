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
using System.Drawing;
using System.IO;

namespace first_try_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int number_of_rectangle = 0;
        public int number_of_ellipse = 0;
        public bool we_draw_line = false;
        public double x_for_line;
        public double y_for_line;
        public double x_for_line_end;
        public double y_for_line_end;
        public Line drawing_line;
        public bool new_line_created = false;

        public MainWindow()
        {
            InitializeComponent();

            this.our_tab_control.SelectedIndex = 0;
            this.main_dock_panel.MouseLeftButtonUp += our_main_window_MouseLeftButtonUp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Обработчик клика для кнопки добавления прямоугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Rectangle buf_for_rectangle = new Rectangle() 
            { 
                Name = "first_rectangle_" + number_of_rectangle.ToString(),
                Margin = new Thickness(300, 100, 0, 0),
                Width = (double)Int32.Parse(this.width_text_box.Text),
                Height = (double)Int32.Parse(this.height_text_box.Text),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top
            };

            if (this.color_combo_box.Text == "Blue")
                {
                    buf_for_rectangle.Fill = Brushes.Blue;
                }
            if (this.color_combo_box.Text == "Red")
                {
                    buf_for_rectangle.Fill = Brushes.Red;
                }
            if (this.color_combo_box.Text == "Green")
                {
                    buf_for_rectangle.Fill = Brushes.Green;
                }
            buf_for_rectangle.MouseMove += mouse_on_rectangle;
            buf_for_rectangle.MouseLeftButtonDown += mouse_left_button_down_rectangle;
            work_field.Children.Add(buf_for_rectangle);
            ++number_of_rectangle;
        }
        /// <summary>
        /// Обработчик перемещения графического примитива
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mouse_on_rectangle(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && !we_draw_line)
            {
                Point point_of_mouse = Mouse.GetPosition(work_field);
                ((Rectangle)sender).Margin = new Thickness(point_of_mouse.X - ((Rectangle)sender).ActualWidth / 2,
                    point_of_mouse.Y - ((Rectangle)sender).ActualHeight / 2, 0, 0);
            }          
        }

        public void mouse_on_ellipse(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && !we_draw_line)
            {
                Point point_of_mouse = Mouse.GetPosition(work_field);
                ((Ellipse)sender).Margin = new Thickness(point_of_mouse.X - ((Ellipse)sender).ActualWidth/2,
                    point_of_mouse.Y - ((Ellipse)sender).ActualHeight/2, 0, 0);
            }
        }


        public void mouse_left_button_down_rectangle(object sender, RoutedEventArgs e)
        {
            if (this.CheckBox_remove_object.IsChecked == false)
            {
                if (this.color_combo_box.Text == "Blue")
                {
                    ((Rectangle)sender).Fill = Brushes.Blue;
                }
                if (this.color_combo_box.Text == "Red")
                {
                    ((Rectangle)sender).Fill = Brushes.Red;
                }
                if (this.color_combo_box.Text == "Green")
                {
                    ((Rectangle)sender).Fill = Brushes.Green;
                }
            }
            else
            {
                work_field.Children.Remove(((Rectangle)sender));
            }
        }

        public void mouse_left_button_down_ellipse(object sender, RoutedEventArgs e)
        {
            if (this.CheckBox_remove_object.IsChecked == false)
            {
                if (this.color_combo_box.Text == "Blue")
                {
                    ((Ellipse)sender).Fill = Brushes.Blue;
                }
                if (this.color_combo_box.Text == "Red")
                {
                    ((Ellipse)sender).Fill = Brushes.Red;
                }
                if (this.color_combo_box.Text == "Green")
                {
                    ((Ellipse)sender).Fill = Brushes.Green;
                }
            }
            else
            {
                work_field.Children.Remove(((Ellipse)sender));
            }
        }
        /// <summary>
        /// Обработчик клика для кнопки добавления эллипса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Ellipse buf_for_ellipse = new Ellipse()
            {
                Name = "first_ellipse_" + number_of_ellipse.ToString(),
                Margin = new Thickness(300, 100, 0, 0),
                Width = (double)Int32.Parse(this.width_text_box.Text),
                Height = (double)Int32.Parse(this.height_text_box.Text),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top
            };
            if (this.color_combo_box.Text == "Blue")
            {
                buf_for_ellipse.Fill = Brushes.Blue;
            }
            if (this.color_combo_box.Text == "Red")
            {
                buf_for_ellipse.Fill = Brushes.Red;
            }
            if (this.color_combo_box.Text == "Green")
            {
                buf_for_ellipse.Fill = Brushes.Green;
            }
            buf_for_ellipse.MouseMove += mouse_on_ellipse;
            buf_for_ellipse.MouseLeftButtonDown += mouse_left_button_down_ellipse;
            work_field.Children.Add(buf_for_ellipse);
            ++number_of_ellipse;
        }

        private void our_main_window_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouse_pos = Mouse.GetPosition(this);
            this.my_label.Content = "X " + mouse_pos.X.ToString() + "Y " + mouse_pos.Y.ToString();
            mouse_pos = Mouse.GetPosition(work_field);
            if (change_color_check_box.IsChecked == false && we_draw_line && mouse_pos.X > 0 && mouse_pos.Y > 0 && 
                Mouse.LeftButton == MouseButtonState.Pressed)
            {
                x_for_line_end = mouse_pos.X;
                y_for_line_end = mouse_pos.Y;
                if (!new_line_created)
                    work_field.Children.Remove(drawing_line);
                new_line_created = false;
                drawing_line = new Line();
                drawing_line.MouseLeftButtonDown += mouse_left_button_down_on_line;
                drawing_line.X1 = x_for_line;
                drawing_line.X2 = x_for_line_end;
                drawing_line.Y1 = y_for_line;
                drawing_line.Y2 = y_for_line_end;
                drawing_line.StrokeThickness = double.Parse(this.width_of_line.Text);
                if (this.color_combo_box_2.Text == "Blue")
                {
                    drawing_line.Stroke = Brushes.Blue;
                }
                if (this.color_combo_box_2.Text == "Red")
                {
                    drawing_line.Stroke = Brushes.Red;
                }
                if (this.color_combo_box_2.Text == "Green")
                {
                    drawing_line.Stroke = Brushes.Green;
                }
                work_field.Children.Add(drawing_line);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
 
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.our_tab_control.SelectedIndex == 1)
                we_draw_line = true;
            else
                we_draw_line = false;
        }

        private void work_field_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void work_field_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void our_main_window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouse_pos = Mouse.GetPosition(work_field);
            if (mouse_pos.X > 0 && mouse_pos.Y > 0 && change_color_check_box.IsChecked == false)
            {
                if (we_draw_line)
                {
                    x_for_line = mouse_pos.X;
                    y_for_line = mouse_pos.Y;
                    new_line_created = true;
                }
            }
        }

        private void our_main_window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
 
        }

        private void mouse_left_button_down_on_line(object sender, MouseButtonEventArgs e)
        {
            if (delete_line_check_box.IsChecked == true)
            {
                work_field.Children.Remove(((Line)sender));
            }
            else if (change_color_check_box.IsChecked == true)
            {
                if (this.color_combo_box_2.Text == "Blue")
                {
                    ((Line)sender).Stroke = Brushes.Blue;
                }
                if (this.color_combo_box_2.Text == "Red")
                {
                    ((Line)sender).Stroke = Brushes.Red;
                }
                if (this.color_combo_box_2.Text == "Green")
                {
                    ((Line)sender).Stroke = Brushes.Green;
                }
            }
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "new_image";
            dlg.DefaultExt = ".html";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                FileStream current_stream = new FileStream(filename, FileMode.CreateNew,FileAccess.Write);
                StreamWriter current_writer = new StreamWriter(current_stream);
                current_writer.WriteLine("<html>");
                current_writer.WriteLine("<body>");
                current_writer.WriteLine("<svg width=\"" + ((int)work_field.ActualWidth).ToString() + 
                    "\" height=\"" + ((int)work_field.ActualHeight).ToString() + "\">");

                foreach (UIElement current_UI_element in work_field.Children)
                {
                    if (current_UI_element is Rectangle)
                    {
                        Color buf_for_color = ((System.Windows.Media.SolidColorBrush)(((Rectangle)current_UI_element).Fill)).Color;
                        current_writer.WriteLine("<rect x=\"" + ((int)((Rectangle)current_UI_element).Margin.Left).ToString() +
                            "\" y=\"" + ((int)((Rectangle)current_UI_element).Margin.Top).ToString() + "\" width=\"" +
                            ((int)((Rectangle)current_UI_element).ActualWidth).ToString() + "\" height=\"" +
                            ((int)((Rectangle)current_UI_element).ActualHeight).ToString() + "\" style=\"fill:rgb(" +
                            buf_for_color.R.ToString() + "," + buf_for_color.G.ToString() + "," +
                            buf_for_color.B.ToString() + ")\"/>");
                    }
                    else if (current_UI_element is Ellipse)
                    {
                        Color buf_for_color = ((System.Windows.Media.SolidColorBrush)(((Ellipse)current_UI_element).Fill)).Color;
                        current_writer.WriteLine("<ellipse cx=\"" + ((int)(((Ellipse)current_UI_element).Margin.Left + 
                            ((Ellipse)current_UI_element).ActualWidth/2)).ToString() +
                            "\" cy=\"" + ((int)(((Ellipse)current_UI_element).Margin.Top + ((Ellipse)current_UI_element).ActualHeight/2)).
                            ToString() + "\" rx=\"" + 
                            ((int)((Ellipse)current_UI_element).ActualWidth/2).ToString() + "\" ry=\"" +
                            ((int)((Ellipse)current_UI_element).ActualHeight / 2).ToString() + "\" style=\"fill:rgb(" + 
                            buf_for_color.R.ToString() + "," + buf_for_color.G.ToString() + "," + 
                            buf_for_color.B.ToString() + ")\"/>");
                    }
                    else if (current_UI_element is Line)
                    {
                        Color buf_for_color = ((System.Windows.Media.SolidColorBrush)(((Line)current_UI_element).Stroke)).Color;
                        current_writer.WriteLine("<line x1=\"" + ((int)((Line)current_UI_element).X1).ToString() +
                            "\" y1=\"" + ((int)((Line)current_UI_element).Y1).ToString() + "\" x2=\"" +
                            ((int)((Line)current_UI_element).X2).ToString() + "\" y2=\"" +
                            ((int)((Line)current_UI_element).Y2).ToString() + "\" style=\"stroke:rgb(" +
                            buf_for_color.R.ToString() + "," + buf_for_color.G.ToString() + "," +
                            buf_for_color.B.ToString() + ");stroke-width:" + ((int)((Line)current_UI_element).StrokeThickness).ToString() +
                            "\"/>");
                    }
                }

                current_writer.WriteLine("</svg>");
                current_writer.WriteLine("</body>");
                current_writer.WriteLine("</html>");
                current_writer.Close();
                current_stream.Close();
            }
        }

    }
}
