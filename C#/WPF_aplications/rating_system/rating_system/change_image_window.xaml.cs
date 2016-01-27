using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Collections;

namespace rating_system
{
    /// <summary>
    /// Interaction logic for change_image_window.xaml
    /// </summary>
    public partial class change_image_window : Window
    {
        List<photo> all_images;
        ObservableCollection<great_assassin> current_assasins;
        int current_selected_index;
        MainWindow current_parent_window;

        public change_image_window(MainWindow parent_window, int selected_index, ObservableCollection<great_assassin> current_list_of_assasins)
        {
            InitializeComponent();
            current_parent_window = parent_window;
            current_assasins = current_list_of_assasins;
            current_selected_index = selected_index;
            load_images();
        }

        public void load_images()
        {
            List<string> resource_paths = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            var buffer = new ResourceManager(assembly.GetName().Name + ".g", assembly);
            try
            {
                var list = buffer.GetResourceSet(CultureInfo.CurrentCulture, true, true);
                foreach (DictionaryEntry item in list)
                {
                    resource_paths.Add((string)item.Key);
                }
            }
            finally
            {
                buffer.ReleaseAllResources();
            }

            all_images = new List<photo>();
            foreach (string current_path in resource_paths)
            {
                if (current_path.Contains(".jpg"))
                {
                    BitmapImage another_image = new BitmapImage(new Uri(@"/" + current_path, UriKind.Relative));
                    all_images.Add(new photo(another_image));
                }
            }

            faces_combo_box.ItemsSource = all_images;
            faces_combo_box.SelectedItem = faces_combo_box.Items[0];
        }

        private void change_image_ok_button_Click(object sender, RoutedEventArgs e)
        {
            if (faces_combo_box.SelectedItem == null)
                MessageBox.Show(my_resourses.texts.selected_null_picture, "", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            else
            {
                current_assasins[current_selected_index].Photo = ((photo)faces_combo_box.SelectedItem).picture;
                this.Close();
            }
        }
    }
}
