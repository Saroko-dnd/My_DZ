using System;
using System.Configuration;

namespace ExampleSQLServer3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Объект ConnectionStringSettings представляет собой отдельную строку подключения в разделе строк подключения 
            // конфигурационного файла
            ConnectionStringSettings setting = new ConnectionStringSettings()
            {
                Name = "MyConnectionString2",     //имя строки подключения в конфигурационном файле
                ConnectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True"
            };

            Configuration config;  // Объект Config представляет конфигурационный файл
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Console.WriteLine(config.FilePath);
            // Объект ConfigurationManager предоставляет доступ к файлам конфигурации
            config.ConnectionStrings.ConnectionStrings.Add(setting);
            config.Save();  // СОХРАНЕНИЕ СТРОКИ ПОДКЛЮЧЕНИЯ В ФАЙЛЕ

            Console.WriteLine("Строка подключения записана в конфигурационный файл.");

            // Получение строки подключения.
            Console.WriteLine("Строка подключения MyConnectionString2");
            Console.WriteLine(ConfigurationManager.ConnectionStrings["MyConnectionString2"].ConnectionString);
            Console.WriteLine("Строка подключения DefaultConnection");
            Console.WriteLine(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


            Console.ReadKey();

        }

       
    }
}
