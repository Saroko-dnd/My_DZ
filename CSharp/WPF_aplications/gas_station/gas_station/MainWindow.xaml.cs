using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xaml;

namespace gas_station
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int amount_of_goods_when_drop;

        StackPanel panel_for_drag_drop_operations;
        public bool drag_drop_operation = false;
        public bool drag_drop_operation_2 = false;
        public bool mouse_left_button_released = true;
        public bool mouse_in_listbox = false;
        public int index_of_children_to_destroy = -1;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public int price_for_one_liter_of_A_72 = 11000;
        public int price_for_one_liter_of_A_76 = 12000;
        public int price_for_one_liter_of_AI_91 = 11500;
        public int price_for_one_liter_of_AI_93 = 10000;
        public int price_for_one_liter_of_AI_95 = 10500;

        public int price_for_water = 3000;
        public int price_for_mineral_water = 3500;
        public int price_for_bun = 2500;
        public int price_for_juice = 10000;
        public int price_for_tea = 20000;
        public int price_for_coffee = 20000;

        public int number_of_trade_operations_with_goods = 0;

        public TreeViewItem gas_trade = new TreeViewItem();
        public TreeViewItem goods_trade = new TreeViewItem(); 

        public MainWindow()
        {
            InitializeComponent();

            gas_trade.Header = texts.gas_deals;
            goods_trade.Header = texts.goods_deals;

            gas_list_box.Items.Add(new Label() { Content = "А-72" });
            gas_list_box.Items.Add(new Label() { Content = "А-76 " });
            gas_list_box.Items.Add(new Label() { Content = "АИ-91" });
            gas_list_box.Items.Add(new Label() { Content = "АИ-93" });
            gas_list_box.Items.Add(new Label() { Content = "АИ-95" });

            load_labels_with_pictures_to_list_box(goods_list_box, "сдобная булочка",@"Images\Bun.jpg",100, true);
            load_labels_with_pictures_to_list_box(goods_list_box, "вода",@"Images\water.jpg",100, true);
            load_labels_with_pictures_to_list_box(goods_list_box, "минеральная вода", @"Images\mineral_water.jpg", 100, true);
            load_labels_with_pictures_to_list_box(goods_list_box, "сок", @"Images\juice.jpg", 100, true);
            load_labels_with_pictures_to_list_box(goods_list_box, "кофе", @"Images\coffee.jpg", 100, true);
            load_labels_with_pictures_to_list_box(goods_list_box, "чай", @"Images\tea.jpg", 100, true);

            list_of_sales_tree_view.Items.Add(gas_trade);
            list_of_sales_tree_view.Items.Add(goods_trade);

            take_button.Content = texts.take;
            remove_button.Content = texts.clean_up;
            second_buy_button.Content = texts.buy;

            get_price_button.Content = texts.get_price;
            gas_list_box.SelectedIndex = -1;

            second_get_price_button.Content = texts.get_price;
            available_services_label.Content = texts.available_services;
            purchases_list_box.AllowDrop = true;
            purchases_list_box.MouseMove += drop_list_box_event;
            purchases_list_box.MouseEnter += mouse_enter_list_box_event;
            purchases_list_box.MouseLeave += mouse_leave_list_box_event;

            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Tick += main_window_mouse_move;
            this.MouseMove += check_mouse_up;

            all_styles_here_label.Content = texts.choose_your_style;

        }

        private void get_price_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (gas_list_box.SelectedIndex)
                {
                    case 0:
                        fuel_price_label.Content = (price_for_one_liter_of_A_72 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    case 1:
                        fuel_price_label.Content = (price_for_one_liter_of_A_76 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    case 2:
                        fuel_price_label.Content = (price_for_one_liter_of_AI_91 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    case 3:
                        fuel_price_label.Content = (price_for_one_liter_of_AI_93 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    case 4:
                        fuel_price_label.Content = (price_for_one_liter_of_AI_95 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    default:
                        break;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show(texts.error_gas);
            }

        }

        private void first_buy_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string money_sum = "";
                switch (gas_list_box.SelectedIndex)
                {
                    case 0:
                        money_sum = (price_for_one_liter_of_A_72 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    case 1:
                        money_sum = (price_for_one_liter_of_A_76 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    case 2:
                        money_sum = (price_for_one_liter_of_AI_91 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    case 3:
                        money_sum = (price_for_one_liter_of_AI_93 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    case 4:
                        money_sum = (price_for_one_liter_of_AI_95 * Int32.Parse(amount_of_gas.Text)).
                            ToString() + " " + texts.of_money;
                        break;
                    default:
                        break;
                }
                TreeViewItem gas_trade_item = new TreeViewItem();
                gas_trade_item.Header = ((Label)gas_list_box.SelectedItem).Content.ToString() + " " +
                    (Int32.Parse(amount_of_gas.Text)).ToString() + " " + texts.of_liter + " " + money_sum;
                gas_trade.Items.Add(gas_trade_item);
                MessageBox.Show("Сделка завершена! В список продаж добавлен новый элемент.");
            }
            catch (System.FormatException)
            {
                MessageBox.Show(texts.error_gas);
            }
            
        }

        private void take_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string buf_for_sum_main = prices_for_all_goods_text_box.Content.ToString();
                string buf_for_sum = buf_for_sum_main.Replace(texts.of_money, "");
                switch (goods_list_box.SelectedIndex)
                {
                    case 0:
                        prices_for_all_goods_text_box.Content = ((price_for_bun * Int32.Parse(number_of_goods_text_box.Text)) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "сдобная булочка", @"Images\Bun.jpg", 100, false);
                        break;
                    case 1:
                        prices_for_all_goods_text_box.Content = ((price_for_water * Int32.Parse(number_of_goods_text_box.Text)) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "вода", @"Images\bottle_of_water.jpg", 100, false);
                        break;
                    case 2:
                        prices_for_all_goods_text_box.Content = ((price_for_mineral_water * Int32.Parse(number_of_goods_text_box.Text)) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "минеральная вода", @"Images\mineral_water.jpg", 100, false);
                        break;
                    case 3:
                        prices_for_all_goods_text_box.Content = ((price_for_juice * Int32.Parse(number_of_goods_text_box.Text)) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "сок", @"Images\juice.jpg", 100, false);
                        break;
                    case 4:
                        prices_for_all_goods_text_box.Content = ((price_for_coffee * Int32.Parse(number_of_goods_text_box.Text)) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "кофе", @"Images\coffee.jpg", 100, false);
                        break;
                    case 5:
                        prices_for_all_goods_text_box.Content = ((price_for_tea * Int32.Parse(number_of_goods_text_box.Text)) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "чай", @"Images\tea.jpg", 100, false);
                        break;
                    default:
                        break;
                }
                ((StackPanel)purchases_list_box.Items[purchases_list_box.Items.Count - 1]).Children.Add(new Label()
                {
                    FontSize = 20,
                    Content = Int32.Parse(number_of_goods_text_box.Text).ToString(),
                    Margin = new Thickness(0, 33, 0, 0)
                });
            }
            catch (System.FormatException)
            {
                MessageBox.Show(texts.error_goods);
            }

        }

        private void second_get_price_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (goods_list_box.SelectedIndex)
                {
                    case 0:
                        price_for_current_product.Content = (price_for_bun * Int32.Parse(number_of_goods_text_box.
                            Text)).ToString() + " " + texts.of_money;
                        break;
                    case 1:
                        price_for_current_product.Content = (price_for_water * Int32.Parse(number_of_goods_text_box.
                            Text)).ToString() + " " + texts.of_money;
                        break;
                    case 2:
                        price_for_current_product.Content = (price_for_mineral_water * Int32.
                            Parse(number_of_goods_text_box.Text)).ToString() + " " + texts.of_money;
                        break;
                    case 3:
                        price_for_current_product.Content = (price_for_juice * Int32.
                            Parse(number_of_goods_text_box.Text)).ToString() + " " + texts.of_money;
                        break;
                    case 4:
                        price_for_current_product.Content = (price_for_coffee * Int32.
                            Parse(number_of_goods_text_box.Text)).ToString() + " " + texts.of_money;
                        break;
                    case 5:
                        price_for_current_product.Content = (price_for_tea * Int32.
                            Parse(number_of_goods_text_box.Text)).ToString() + " " + texts.of_money;
                        break;
                    default:
                        break;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show(texts.error_goods);
            }
        }

        private void remove_button_Click(object sender, RoutedEventArgs e)
        {
            prices_for_all_goods_text_box.Content = "0 " + texts.of_money;
            purchases_list_box.Items.Clear();
        }

        private void second_buy_button_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem goods_trade_item = new TreeViewItem();
            ++number_of_trade_operations_with_goods;
            goods_trade_item.Header = number_of_trade_operations_with_goods.ToString();
            goods_trade_item.Items.Add(new TreeViewItem() { Header = prices_for_all_goods_text_box.Content.ToString()});
            for (int counter = 0; counter < purchases_list_box.Items.Count; ++counter )
            {
                TreeViewItem small_goods_trade_item = new TreeViewItem();
                small_goods_trade_item.Header = ((Label)(((StackPanel)purchases_list_box.Items[counter]).
                    Children[1])).Content.ToString() + " " + ((Label)(((StackPanel)purchases_list_box.Items[counter]).
                    Children[2])).Content.ToString();
                goods_trade_item.Items.Add(small_goods_trade_item);
            }
            goods_trade.Items.Add(goods_trade_item);
            MessageBox.Show("Сделка завершена! В список продаж добавлен новый элемент.");
        }

        private void load_picture_event(object sender, RoutedEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("D:\\users\\SarokoDZ_2\\C#\\WPF_aplications\\gas_station\\gas_station\\Bun.jpg"));
        }


        private void load_labels_with_pictures_to_list_box(ListBox current_list_box, string label_content, 
            string path_to_picture, int size_of_picture, bool mouse_event_must_be)
        {
            StackPanel panel_for_label_and_picture = new StackPanel();
            if (mouse_event_must_be)
                panel_for_label_and_picture.MouseDown += mouse_down_stack_panel_event;
            panel_for_label_and_picture.Orientation = Orientation.Horizontal;

            Image buf_for_current_image = new Image();
            buf_for_current_image.Width = size_of_picture;
            buf_for_current_image.Height = size_of_picture;
            buf_for_current_image.Source = new BitmapImage(new Uri(path_to_picture, UriKind.Relative));

            panel_for_label_and_picture.Children.Add(buf_for_current_image);
            panel_for_label_and_picture.Children.Add(new Label() { HorizontalAlignment = 
                System.Windows.HorizontalAlignment.Stretch, VerticalAlignment = System.Windows.
                VerticalAlignment.Stretch, Content = label_content,Margin =
                new Thickness(0, size_of_picture / 3, 0, 0), FontSize = size_of_picture/5});

            current_list_box.Items.Add(panel_for_label_and_picture);
        }

        private void mouse_down_stack_panel_event(object sender, EventArgs e)
        {

                Point mouse_pos = Mouse.GetPosition(pink_grid);
                //StackPanel current_panel = (StackPanel)sender;
                //DragDrop.DoDragDrop(current_panel, current_panel, DragDropEffects.Copy);
                panel_for_drag_drop_operations = new StackPanel()
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top
                };
                panel_for_drag_drop_operations.Orientation = Orientation.Horizontal;
                panel_for_drag_drop_operations.Children.Add(new Image()
                {
                    Opacity = 0.75,
                    Height = 100,
                    Width = 100,
                    Source =
                    ((Image)((StackPanel)sender).Children[0]).Source
                });
                panel_for_drag_drop_operations.Children.Add(new Label()
                {
                    Opacity = 0.75,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                    FontSize = 20,
                    Margin = new Thickness(0, 33, 0, 0),
                    Content = ((Label)((StackPanel)sender).Children[1]).Content
                });
                panel_for_drag_drop_operations.Opacity = 0.75;
                panel_for_drag_drop_operations.Margin = new Thickness(mouse_pos.X - 50, mouse_pos.Y - 25, 0, 0);
                pink_grid.Children.Add(panel_for_drag_drop_operations);
                drag_drop_operation = true;
                drag_drop_operation_2 = true;
                mouse_left_button_released = false;
                index_of_children_to_destroy = pink_grid.Children.Count - 1;
                dispatcherTimer.Start();
        }

        private void drop_list_box_event(object sender, EventArgs e)
        {
            Point mouse_pos = Mouse.GetPosition(pink_grid);
            if ((this.ActualWidth - mouse_pos.X) < third_column_in_store.ActualWidth) 
            if (Mouse.LeftButton == MouseButtonState.Released && drag_drop_operation_2)
            {
                if (index_of_children_to_destroy > 0)
                {
                    dispatcherTimer.Stop();
                    pink_grid.Children.RemoveAt(index_of_children_to_destroy);
                    index_of_children_to_destroy = -1;
                    drag_drop_operation = false;
                }
                new drop_window(goods_list_box, this) { Owner = this }.ShowDialog();
                drag_drop_operation_2 = false;
                string buf_for_sum_main = prices_for_all_goods_text_box.Content.ToString();
                string buf_for_sum = buf_for_sum_main.Replace(texts.of_money, "");
                switch (goods_list_box.SelectedIndex)
                {
                    case 0:
                        prices_for_all_goods_text_box.Content = ((price_for_bun * amount_of_goods_when_drop) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "сдобная булочка", @"Images\Bun.jpg", 100, false);
                        break;
                    case 1:
                        prices_for_all_goods_text_box.Content = ((price_for_water * amount_of_goods_when_drop) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "вода", @"Images\water.jpg", 100, false);
                        break;
                    case 2:
                        prices_for_all_goods_text_box.Content = ((price_for_mineral_water * amount_of_goods_when_drop) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "минеральная вода", @"Images\mineral_water.jpg", 100, false);
                        break;
                    case 3:
                        prices_for_all_goods_text_box.Content = ((price_for_juice * amount_of_goods_when_drop) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "сок", @"Images\juice.jpg", 100, false);
                        break;
                    case 4:
                        prices_for_all_goods_text_box.Content = ((price_for_coffee * amount_of_goods_when_drop) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "кофе", @"Images\coffee.jpg", 100, false);
                        break;
                    case 5:
                        prices_for_all_goods_text_box.Content = ((price_for_tea * amount_of_goods_when_drop) +
                            Int32.Parse(buf_for_sum)).ToString() + " " + texts.of_money;
                        load_labels_with_pictures_to_list_box(purchases_list_box, "чай", @"Images\tea.jpg", 100, false);
                        break;
                    default:
                        break;
                }
                ((StackPanel)purchases_list_box.Items[purchases_list_box.Items.Count - 1]).Children.Add(new Label()
                {
                    FontSize = 20,
                    Content = amount_of_goods_when_drop.ToString(),
                    Margin = new Thickness(0, 33, 0, 0)
                });
            }  
        }

        private void main_window_mouse_move(object sender, EventArgs e)
        {
            
            Point mouse_pos = Mouse.GetPosition(pink_grid);
            ((StackPanel)pink_grid.Children[index_of_children_to_destroy]).Margin = new Thickness(mouse_pos.X - 50,
                    mouse_pos.Y - 25, 0, 0);
        }

        private void mouse_enter_list_box_event(object sender, EventArgs e)
        {
            mouse_in_listbox = true;
        }

        private void mouse_leave_list_box_event(object sender, EventArgs e)
        {
            mouse_in_listbox = false;
        }

        private void check_mouse_up(object sender, EventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Released && drag_drop_operation && 
                index_of_children_to_destroy > 0)
            {
                drop_list_box_event(this, null);
                if (Mouse.LeftButton == MouseButtonState.Released && drag_drop_operation &&
                index_of_children_to_destroy > 0)
                {
                    dispatcherTimer.Stop();
                    pink_grid.Children.RemoveAt(index_of_children_to_destroy);
                    index_of_children_to_destroy = -1;
                    drag_drop_operation = false;
                    mouse_left_button_released = true;
                    drag_drop_operation_2 = false;
                }
            }
        }

        //event handler for combo_box with styles
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0:
                    this.Resources.MergedDictionaries.Clear();
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("scenes/first_scene.xaml", UriKind.Relative) });
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Resources/data_context.xaml", UriKind.Relative) });
                    break;
                case 1:
                    this.Resources.MergedDictionaries.Clear();
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("scenes/second_scene.xaml", UriKind.Relative) });
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Resources/data_context.xaml", UriKind.Relative) });
                break;
                default:
                break;
            }
        }

    }
}
