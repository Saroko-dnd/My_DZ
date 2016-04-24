using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Используем технологию пространство ODBC
using System.Data.OleDb;

namespace ExampleODBC
{
    class Program
    {
        static void Main(string[] args)
        {
            // создаём объект для соединения с базой
            OleDbConnection connection = new OleDbConnection();
            try
            {
                // Прописываем строку соединения 
                // Указываем параметры необходимые для MS Access
                // Их два: имя драйвера (Driver) зарегистрированное в системе 
                // и путь к файлу базы данных (Dbq) 
                connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=med.mdb";


                // Открываем соединение 
                connection.Open();


                // Создаём объект для исполнения запроса
                // Указываем ему в качестве параметра запрос и объект открытого соединения
                OleDbCommand command = new OleDbCommand("SELECT * FROM Patients", connection);
                OleDbCommand command_2 = new OleDbCommand("SELECT COUNT(*) FROM Doctors", connection);
                OleDbCommand command_5 = new OleDbCommand("SELECT patientName,doctorName,visitDate FROM (Visits INNER JOIN Doctors ON Visits.visitDoctor=Doctors.doctorID) INNER JOIN Patients ON Patients.patientID=Visits.visitPatient", connection);
                OleDbCommand command_4 = new OleDbCommand("SELECT doctorName,visitCostValue FROM VisitCosts INNER JOIN Doctors ON VisitCosts.visitCostDoctorID=Doctors.doctorID", connection);
                OleDbCommand command_3 = new OleDbCommand("SELECT patientName,doctorName FROM (Visits INNER JOIN Doctors ON Visits.visitDoctor=Doctors.doctorID) INNER JOIN Patients ON Patients.patientID=Visits.visitPatient", connection);

                // Исполняем запрос и сохраняем ссылку на объект результата
                OleDbDataReader reader = command.ExecuteReader();
                // Проходимся по результатам работы запроса строка за строкой
                // Когда данные кончатся метод Read вернет false
                while (reader.Read() != false)
                {
                    // Так как в таблице три столбца в результате в строке будет тоже три столбца
                    // Для обращения к столбцу используется индексатор 
                    // Возможен доступ как по имени столбца так и по индексу
                    Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}", reader[0], reader[1], reader[2], reader[3]);
                    // Console.WriteLine("{0,-10} {1,-10} {2,-10}", reader[0], reader[1], reader[2]);
                }
                Console.WriteLine("\n\n{0}", reader.GetFieldType(0));
                Console.WriteLine("\n\n{0}", command_2.ExecuteScalar());
                reader.Close();

                OleDbDataReader reader_5 = command_5.ExecuteReader();
                while (reader_5.Read() != false)
                {
                    // Так как в таблице три столбца в результате в строке будет тоже три столбца
                    // Для обращения к столбцу используется индексатор 
                    // Возможен доступ как по имени столбца так и по индексу
                    Console.WriteLine("{0,-10} {1,-10} {2,-10}", reader_5[0], reader_5[1], reader_5[2]);
                    // Console.WriteLine("{0,-10} {1,-10} {2,-10}", reader[0], reader[1], reader[2]);
                }
                OleDbDataReader reader_4 = command_4.ExecuteReader();
                while (reader_4.Read() != false)
                {
                    // Так как в таблице три столбца в результате в строке будет тоже три столбца
                    // Для обращения к столбцу используется индексатор 
                    // Возможен доступ как по имени столбца так и по индексу
                    Console.WriteLine("{0,-10} {1,-10}", reader_4[0], reader_4[1]);
                    // Console.WriteLine("{0,-10} {1,-10} {2,-10}", reader[0], reader[1], reader[2]);
                }
                OleDbDataReader reader_3 = command_3.ExecuteReader();
                while (reader_3.Read() != false)
                {
                    // Так как в таблице три столбца в результате в строке будет тоже три столбца
                    // Для обращения к столбцу используется индексатор 
                    // Возможен доступ как по имени столбца так и по индексу
                    Console.WriteLine("{0,-10} {1,-10}", reader_3[0], reader_3[1]);
                    // Console.WriteLine("{0,-10} {1,-10} {2,-10}", reader[0], reader[1], reader[2]);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Закрываем соединение
                connection.Close();
            }

            Console.ReadKey();


        }
    }
}
