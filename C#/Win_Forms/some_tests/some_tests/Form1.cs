using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace some_tests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test_class[,] test_matrix = new test_class[2,2];
            test_matrix[0, 0] = new test_class();
            test_matrix[0, 0].change_value(9);
            test_matrix[0, 0].current_value = 11;
            test_matrix[0, 0].current_value = 12;
        }
    }
}
