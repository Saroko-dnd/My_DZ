using System;
using System.Collections.Generic;
using System.Collections;
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

namespace Binding_first_try
{

    public class font_weight_converter : IValueConverter
    {

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return FontWeights.Black;
            else
                return ((FontWeight)value);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return ((Int32)value).ToString();
            }
            catch
            {
                return "&&&&";
            }
        }
    }

    public class font_data : IValueConverter
    {

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return Int32.Parse((string)value);
            }
            catch
            {
                return 18;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return ((Int32)value).ToString();
            }
            catch
            {
                return "&&&&";
            }
        }
    }

    public class decor_converter : IValueConverter
    {
        static TextDecorationCollection[] current_list_of_decor = Application.Current.Resources["all_decor_2"] as TextDecorationCollection[];
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return current_list_of_decor[((int)value)];
            }
            catch
            {
                return null;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }
    }

    public class FontsWrapper
    {
        static ICollection<FontWeight> fontWeights;
        static ICollection<FontStyle> fontStyles;
        static ICollection<FontFamily> fontFamilies;

        public static ICollection<FontStyle> GetFontStyles()
        {
            return fontStyles ?? (fontStyles = new List<FontStyle>() { System.Windows.FontStyles.Italic, System.Windows.FontStyles.Normal, System.Windows.FontStyles.Oblique });//TODO:Get by reflection
        }

        public static ICollection<FontFamily> GetFontFamilies()
        {
            return fontFamilies ?? (fontFamilies = Fonts.SystemFontFamilies);
        }

        public static ICollection<FontWeight> GetFontWeights()
        {
            if (fontWeights == null)
                fontWeights = new List<FontWeight>();
            else
                return fontWeights;

            var type = typeof(FontWeights);
            foreach (var p in type.GetProperties())
            {
                fontWeights.Add((FontWeight)p.GetValue(null, null));
            }
            return fontWeights;
        }

        public static ICollection<FontWeight> FontWeights
        {
            get { return fontWeights ?? (fontWeights = GetFontWeights()); }
        }
        public static ICollection<FontStyle> FontStyles
        {
            get { return fontStyles ?? (fontStyles = GetFontStyles()); }
        }

        public static ICollection<FontFamily> FontFamilies
        {
            get { return fontFamilies ?? (fontFamilies = GetFontFamilies()); }
        }
    }
}
