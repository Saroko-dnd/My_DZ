﻿using System;
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
using System.Xml.Linq;
using System.Xml;
using System.Collections.ObjectModel;

namespace LinqForXml
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            CdDataGrid.AutoGeneratingColumn += ColumnNameAttribute.dgPrimaryGrid_AutoGeneratingColumn;
            ProducerDataGrid.AutoGeneratingColumn += ColumnNameAttribute.dgPrimaryGrid_AutoGeneratingColumn;
            CdDataGrid.ItemsSource = XmlDataLoader.CdList;
            ProducerDataGrid.ItemsSource = XmlDataLoader.ProducerList;

        }

        private void QueryListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    XmlDataLoader.ExecuteFirstQuery(QueryDataGrid);
                    break;
                case 1:
                    XmlDataLoader.ExecuteSecondQuery(QueryDataGrid);
                    break;
                case 2:
                    XmlDataLoader.ExecuteThirdQuery(QueryDataGrid);
                    break;
                case 3:
                    XmlDataLoader.ExecuteFourthQuery(QueryDataGrid);
                    break;
                case 4:
                    XmlDataLoader.ExecuteFifthQuery(QueryDataGrid);
                    break;
                case 5:
                    XmlDataLoader.ExecuteSixthQuery(QueryDataGrid);
                    break;
                case 6:
                    XmlDataLoader.ExecuteSeventhQuery(QueryDataGrid);
                    break;
                case 7:
                    XmlDataLoader.ExecuteEighthQuery(QueryDataGrid);
                    break;
                case 8:
                    XmlDataLoader.ExecuteNinthQuery(QueryDataGrid);
                    break;
                case 9:
                    XmlDataLoader.ExecuteTenthQuery(QueryDataGrid);
                    break;
                default:
                    break;
            }
        }
    }
}
