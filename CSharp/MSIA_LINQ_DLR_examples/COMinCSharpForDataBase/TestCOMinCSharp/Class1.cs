using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCOMinCSharp
{
    [Guid("FC56F704-AE09-4BC8-AB2B-B9D0E1E99239")]
    public interface DBCOM_Interface
    {
        [DispId(1)]
        void Init(string userid, string password);
        [DispId(2)]
        bool ExecuteSelectCommand(string selCommand);
        [DispId(3)]
        bool NextRow();
        [DispId(4)]
        void ExecuteNonSelectCommand(string insCommand);
        [DispId(5)]
        string GetColumnData(int pos);
    }

    // // Events interface Database_COMObjectEvents 
    [Guid("E3BD5578-4A90-4F98-9AFF-780CC2D91F25"),
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface DBCOM_Events
    {

    }

    [Guid("E2CC9A63-D7AD-470E-910A-70B91E993913"),
    ClassInterface(ClassInterfaceType.None),
    ComSourceInterfaces(typeof(DBCOM_Events))]
    public class DBCOM_Class : DBCOM_Interface
    {
        private SqlConnection myConnection = null;
        SqlDataReader myReader = null;

        public DBCOM_Class()
        {
        }

        public void Init(string userid, string password)
        {
            try
            {
                string myConnectString = "user id=" + userid + ";password=" + password +
                    ";Database=NorthWind;Server=SKYWALKER;Connect Timeout=30";
                myConnection = new SqlConnection(myConnectString);
                myConnection.Open();
                //MessageBox.Show("CONNECTED");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool ExecuteSelectCommand(string selCommand)
        {
            if (myReader != null)
                myReader.Close();

            SqlCommand myCommand = new SqlCommand(selCommand);
            myCommand.Connection = myConnection;
            myCommand.ExecuteNonQuery();
            myReader = myCommand.ExecuteReader();
            return true;
        }

        public bool NextRow()
        {
            if (!myReader.Read())
            {
                myReader.Close();
                return false;
            }
            return true;
        }

        public string GetColumnData(int pos)
        {
            Object obj = myReader.GetValue(pos);
            if (obj == null) return "";
            return obj.ToString();
        }

        public void ExecuteNonSelectCommand(string insCommand)
        {
            SqlCommand myCommand = new SqlCommand(insCommand, myConnection);
            int retRows = myCommand.ExecuteNonQuery();
        }

    }
}
