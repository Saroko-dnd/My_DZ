﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ShopAuto
{
    public partial class Form1 : Form
    {
        public void DataGridView1_SelectionChanged(object sender, EventArgs CurArgs)
        {
            // Update the labels to reflect changes to the selection.
            tMotorTableAdapter.Adapter.SelectCommand = new SqlCommand(@"SELECT * FROM tMotor WHERE IDD = 3");
            this._I__С_РАБОЧЕГО_PC__УЧЕБНОЕ__ИСОЗИ_434_436_438_2013_2014__МАТЕРИАЛЫ_ИСОЗИ_ISOZI_GZ_18_DBAUTO_MDFDataSet.TMotor.Clear();
            tMotorTableAdapter.Fill(this._I__С_РАБОЧЕГО_PC__УЧЕБНОЕ__ИСОЗИ_434_436_438_2013_2014__МАТЕРИАЛЫ_ИСОЗИ_ISOZI_GZ_18_DBAUTO_MDFDataSet.TMotor);
        }

        public Form1()
        {
            InitializeComponent();

            tAutoDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tAutoDataGridView.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_I__С_РАБОЧЕГО_PC__УЧЕБНОЕ__ИСОЗИ_434_436_438_2013_2014__МАТЕРИАЛЫ_ИСОЗИ_ISOZI_GZ_18_DBAUTO_MDFDataSet.TMotor' table. You can move, or remove it, as needed.
            this.tMotorTableAdapter.Fill(this._I__С_РАБОЧЕГО_PC__УЧЕБНОЕ__ИСОЗИ_434_436_438_2013_2014__МАТЕРИАЛЫ_ИСОЗИ_ISOZI_GZ_18_DBAUTO_MDFDataSet.TMotor);
            // TODO: This line of code loads data into the '_I__С_РАБОЧЕГО_PC__УЧЕБНОЕ__ИСОЗИ_434_436_438_2013_2014__МАТЕРИАЛЫ_ИСОЗИ_ISOZI_GZ_18_DBAUTO_MDFDataSet.TOwner' table. You can move, or remove it, as needed.
            this.tOwnerTableAdapter.Fill(this._I__С_РАБОЧЕГО_PC__УЧЕБНОЕ__ИСОЗИ_434_436_438_2013_2014__МАТЕРИАЛЫ_ИСОЗИ_ISOZI_GZ_18_DBAUTO_MDFDataSet.TOwner);
            // TODO: This line of code loads data into the '_I__С_РАБОЧЕГО_PC__УЧЕБНОЕ__ИСОЗИ_434_436_438_2013_2014__МАТЕРИАЛЫ_ИСОЗИ_ISOZI_GZ_18_DBAUTO_MDFDataSet.TAuto' table. You can move, or remove it, as needed.
            this.tAutoTableAdapter.Fill(this._I__С_РАБОЧЕГО_PC__УЧЕБНОЕ__ИСОЗИ_434_436_438_2013_2014__МАТЕРИАЛЫ_ИСОЗИ_ISOZI_GZ_18_DBAUTO_MDFDataSet.TAuto);
            
        }

    }
}
