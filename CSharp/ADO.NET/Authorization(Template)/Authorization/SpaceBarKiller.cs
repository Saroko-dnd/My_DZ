using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authorization
{
    //Запрещает пользователю использовать пробелы в логине, пароле, имени и т.д.
    class SpaceBarKiller
    {
        public static void TextBoxTextChanged(object sender, EventArgs e)
        {
            (sender as TextBox).Text = (sender as TextBox).Text.Replace(" ", "");
        }

        public static void TextBoxKeyPress(object sender, KeyPressEventArgs e)
        { 
            if (e.KeyChar == (char)Keys.Space)
                e.Handled = true;
            else
                e.Handled = false; 
        }
    }
}
