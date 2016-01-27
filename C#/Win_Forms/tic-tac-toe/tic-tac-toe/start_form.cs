using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tic_tac_toe
{
    public partial class start_form : Form
    {
        public string player_char;
        public string computer_char;
        public start_form()
        {
            InitializeComponent();
        }

        private void zero_button_Click(object sender, EventArgs e)
        {
            player_char = "0";
            computer_char = "X";
            this.Hide();
        }

        private void x_button_Click(object sender, EventArgs e)
        {
            player_char = "X";
            computer_char = "0";
            this.Hide();
        }
    }
}
