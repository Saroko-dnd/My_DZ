using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace file_system
{
    class Program
    {
        public static DirectoryInfo info = new System.IO.DirectoryInfo(@"e:\iteducation\drawing\test");
        public static List<FileSystemInfo> list_of_all_files_and_directoryes = new List<FileSystemInfo>(info.
            GetFileSystemInfos());

        public static void show_all_catalogs_and_files(DirectoryInfo current_directory, int indent=0)
        {
            List<DirectoryInfo> list_of_all_directories = new List<DirectoryInfo>(current_directory.
                GetDirectories());
            List<FileInfo> list_of_all_files = new List<FileInfo>(current_directory.GetFiles());
            foreach (DirectoryInfo current_dir in list_of_all_directories)
            {
                for (int counter = 0; counter < indent; ++counter)
                    Console.Write(" ");
                Console.WriteLine("|");
                for (int counter = 0; counter < indent; ++counter)
                    Console.Write(" ");
                Console.Write("_____");
                Console.WriteLine(current_dir.Name + "\n");
                show_all_catalogs_and_files(current_dir, indent + 5);
            }

            string current_path = current_directory.FullName + @"\" + @"our_temp_file.txt";

            foreach (FileInfo current_file in list_of_all_files)
            {
                for (int counter = 0; counter < indent; ++counter)
                    Console.Write(" ");
                Console.WriteLine("|");
                for (int counter = 0; counter < indent; ++counter)
                    Console.Write(" ");
                Console.Write("_____");
                if (current_file.Extension == ".txt")
                {
                    FileStream current_stream = new FileStream(current_path, FileMode.CreateNew,
                        FileAccess.ReadWrite);
                    StreamWriter current_writer = new StreamWriter(current_stream);
                    FileStream current_stream_2 = new FileStream(current_file.FullName, FileMode.Open,
                        FileAccess.ReadWrite);
                    StreamReader current_reader_2 = new StreamReader(current_stream_2);
                    while (true)
                    {
                        string buf_string = current_reader_2.ReadLine();
                        if (buf_string == null)
                            break;
                        string second_buf_string = buf_string.Replace("this", "subject");
                        current_writer.WriteLine(second_buf_string);
                    }
                    current_writer.Close();
                    current_stream.Close();
                    current_reader_2.Close();
                    current_stream_2.Close();
                    File.Delete(current_file.FullName);
                    File.Move(current_path, current_file.FullName);
                }
                Console.WriteLine(current_file.Name + "\n");
            }         
        }

        static void Main(string[] args)
        {
            show_all_catalogs_and_files(info);
            Console.ReadKey();
        }

    }
}
