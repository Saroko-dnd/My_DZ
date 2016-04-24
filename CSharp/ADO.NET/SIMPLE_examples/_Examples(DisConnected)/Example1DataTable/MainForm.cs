using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example1DataTable
{
    public partial class MainForm : Form
    {
        DataSet dataSet;

        public MainForm()
        {
            InitializeComponent();
           
        }

            private void Form1_Load(object sender, EventArgs e)
            {
                // Приязка к таблицам в качестве источника BindingSource
                dgwDrivers.DataSource = bsDrivers;
                dgwCars.DataSource = bsCars;

                // BindingSource - предназначен для упрощения процесса 
                // привязки элементов управления к источнику данных


                // Создание DataSet с именем AutoPark
                dataSet = new DataSet("AutoPark");
                // Формирование заполнение dataSet  из  XML-файла
                dataSet.ReadXml("Registration.xml");
                dataSet.ReadXmlSchema("Registration.xsd");




                // присваивание dataSet объекту BindingSource
                bsDrivers.DataSource = dataSet;
                // указываем таблицу для свойства DataMember
                bsDrivers.DataMember = "Drivers";

                // присваивание dataSet объекту BindingSource
                bsCars.DataSource = bsDrivers;
                // указываем таблицу для свойства DataMember
                bsCars.DataMember = "FK_DriversCars";

            }

    }
}
