using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleStoredProcedure
{
    class Program
    {
        static void Main(string[] args)
        {
      // ***  ПРОЦЕДУРА ПОЛУЧЕНИЯ ОПЛАТЫ ПО ИМЕНИ  *** //
            //CREATE PROCEDURE [dbo].[GetSalaryByName]
            //    @Name char(10),
            //    @Salary float output
            //AS
            //    SELECT @Salary = SALARY from Persons
            //       where Name=@Name
            //RETURN 0


            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True";

                connection.Open();


                // Указываем имя хранимой процедуры
                string cmdPr1 = @"GetSalaryByName";
                SqlCommand cmdProcedure = new SqlCommand(cmdPr1, connection);


                // Изменение типа на StoredProcedure (по умолчанию оператор SQL)
                cmdProcedure.CommandType = CommandType.StoredProcedure;

                // Входной параметр.
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Name";      // имя параметра
                param.SqlDbType = SqlDbType.Char;   // тип параметра (char)
                param.Size = 10;
                param.Value = "Ivan";               // значение параметра
                cmdProcedure.Parameters.Add(param); // добьавление параметра в список параметров команды

                // Выходной параметр.
                param = new SqlParameter();
                param.ParameterName = "@Salary";
                param.SqlDbType = SqlDbType.Float;
                param.Direction = ParameterDirection.Output; // направление действия каждого параметра
                cmdProcedure.Parameters.Add(param);

                // Выполнение хранимой процедуры.
                cmdProcedure.ExecuteNonQuery();
                // Возврат выходного параметра.
                Console.WriteLine("Salary = {0}", cmdProcedure.Parameters["@Salary"].Value.ToString());
               

                //CREATE PROCEDURE [dbo].[GetSalaries]
                //	@InSalary float
                //AS
                //	SELECT P.NAME, P.SUBJECT from Persons as P
                //	   where p.SALARY>=@InSalary


                // Указываем имя хранимой процедуры
                string cmdPr2 = @"GetSalaries";
                SqlCommand cmdProcedure2 = new SqlCommand(cmdPr2, connection);
                // Изменение типа на StoredProcedure (по умолчанию оператор SQL)
                cmdProcedure2.CommandType = CommandType.StoredProcedure;

                // Входной параметр.
                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@InSalary";      // имя параметра
                param2.SqlDbType = SqlDbType.Float;      // тип параметра 
                param2.Value = "250";                    // значение параметра
                cmdProcedure2.Parameters.Add(param2);     // добавление параметра в список параметров команды
                               
                // Выполнение хранимой процедуры.
                SqlDataReader readrer = cmdProcedure2.ExecuteReader();

                while (readrer.Read())
                {
                    String info = String.Format("{0} - {1}", readrer[0], readrer[1]);
                    Console.WriteLine(info);
                }
                readrer.Close();
               

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }

            Console.ReadKey();
        }
    }
}
