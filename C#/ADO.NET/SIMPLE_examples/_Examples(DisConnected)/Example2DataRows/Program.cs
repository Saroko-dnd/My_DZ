using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2DataRows
{
    class Program
    {
        // ДЕМОНСТРАЦИЯ СВОЙСТВА  RowState

        static void Main(string[] args)
        {
            // Создать объект temp типа DataTable для тестирования.
            DataTable temp = new DataTable("Cars");
            //
            temp.Columns.Add(new DataColumn("TempColumn", typeof(int)));
           
           
            DataRow row = temp.NewRow();
            Console.WriteLine("После NewRow(): {0}", row.RowState);             // RowState = Detached (т.е. пока еще не является частью DataTable) .
                
            
            temp.Rows.Add(row);
            Console.WriteLine("После Rows.Add(): {0}", row.RowState);            // RowState = Added.
          
           
            row["TempColumn"] = 10;
            Console.WriteLine("После изменения : {0}", row.RowState);             // RowState = Added.
           
           
            temp.AcceptChanges();
            Console.WriteLine("После вызова AcceptChanges: {0}", row.RowState);   // RowState = Unchanged.
           
            
            row["TempColumn"] = 11;
            Console.WriteLine("После  assignment: {0}", row.RowState);            // RowState = Modified.
            
            
            temp.Rows[0].Delete();
            Console.WriteLine("После Delete: {0}", row.RowState);               // RowState = Deleted.


            Console.ReadKey();
        }
    }
}
