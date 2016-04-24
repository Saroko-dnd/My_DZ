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
using System.Windows.Shapes;

namespace gas_station
{
    /// <summary>
    /// Interaction logic for drop_window.xaml
    /// </summary>
    public partial class drop_window : Window
    {
        MainWindow parent_window;

        public drop_window(ListBox current_list_box, MainWindow current_parent_window)
        {
            InitializeComponent();

            text_on_label.Text = texts.how_many_goods + " \"" + ((Label)((StackPanel)current_list_box.
                SelectedItem).Children[1]).Content.ToString() + "\" " + texts.you_want_buy;

            parent_window = current_parent_window;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                parent_window.amount_of_goods_when_drop = Int32.Parse(number_of_goods_text_block.Text);
                this.Close();
            }
            catch (System.FormatException)
            {
                MessageBox.Show(texts.error_goods);
            }
        }
    }
}
