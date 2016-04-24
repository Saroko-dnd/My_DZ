using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace strings_of_file_class
{
    class Program
    {
        static void Main(string[] args)
        {

            strings_of_file.do_something current_do_something = delegate(string first, string second)
            {
                return String.Compare(first, second, true);
            };

            string test_1 = "gggg kkkk jjj";
            int test_1_res = test_1.IndexOf(' ', 7);
            strings_of_file test_object = new strings_of_file("test.txt", current_do_something);
            test_object.load_pointers_to_strings();
            test_object.load_key_for_file(2);
            List<string> test_list_of_strings = new List<string>();
            test_list_of_strings = test_object.get_string(2, "романов");
            foreach (string current_string in test_list_of_strings)
            {
                Console.WriteLine(current_string);
            }
            Console.ReadKey();
            /*string first_string = test_object[3];
            string second_string = test_object[4];
            string third_string = test_object[5];
            foreach (string current_string in test_object)
            {
                Console.WriteLine(current_string);
            }           
            string third_string2 = test_object[5];*/
        }
    }

    class strings_of_file_enumerator : IEnumerator
    {
        int current_position = -1;
        strings_of_file current_storage;
        string current_valur;
        public bool MoveNext()
        {
            ++current_position;
            try
            {
                current_valur = current_storage[current_position];
                return true;
            }
            catch (ArgumentException current_exeption)
            {
                return false;
            }
        }

        public void Reset()
        {
            current_position = -1;
        }

        public strings_of_file_enumerator(strings_of_file new_storage)
        {
            current_storage = new_storage;
        }

        object IEnumerator.Current
        {
            get
            {
                return current_valur;
            }
        }

        public string Current
        {
            get
            {
                return current_valur;
            }
        }
    }

    //for UTF-8
    class strings_of_file : IEnumerable
    {
        /// <summary>
        /// кодировка открываемого файла
        /// </summary>
        Encoding encoding_of_file = Encoding.UTF8;
        FileStream current_file_stream;
        /// <summary>
        /// показывает найдена строка которую мы ищем в файле или нет (значение меняется в бинарном поиске
        /// binary_search в зависимости от его успешности)
        /// </summary>
        bool not_found = false;
        /// <summary>
        ///список который содержит номера (в байтах) окончаний строк
        /// </summary>
        List<int> strings_in_byte = new List<int>();
        /// <summary>
        ///экземпляр класса возвращающего одну строку из файла по ее индексу в strings_in_byte
        /// </summary>
        current_string_of_file create_string_from_index;
        /// <summary>
        ///буфер для чтения файла
        /// </summary>
        byte[] buf_array = new byte[64000];
        /// <summary>
        ///счетчик байтов при подсчете длины строки файла
        /// </summary>
        int buf_int_for_strings_check_points = 0;
        /// <summary>
        ///количество колонок в файле
        /// </summary>
        const int  number_of_columns = 3;
        /// <summary>
        ///список отсортированных ключей для каждой из колонок
        /// </summary>
        List<List<int>> all_keys = new List<List<int>>(number_of_columns);
        /// <summary>
        ///шаблон для функции сравнения строк
        /// </summary>
        public delegate int do_something(string first, string second);
        /// <summary>
        ///текущий метод сравнения строк (выбирается пользователем класса)
        /// </summary>
        do_something cmp_strings_function;

        //*************************************************************************************************
        /// <summary>
        ///class current_string_of_file возращает распарсенную строку по индексу из strings_in_byte
        /// </summary>
        public class current_string_of_file
        {
            /// <summary>
            ///кодировка открываемого файла
            /// </summary>
            Encoding encoding_of_file = Encoding.UTF8;
            /// <summary>
            ///список который содержит номера (в байтах) окончаний строк
            /// </summary>
            List<int> strings_in_byte;
            FileStream current_file_stream;
            /// <summary>
            ///количество байт у заголовка файла, которые необходимо пропустить при чтении из файла
            /// </summary>
            int BOM_bytes = 3;
            /// <summary>
            ///загружает одну строку из файла по индексу в strings_in_byte
            /// </summary>
            public string load_string_from_file(int key)
            {
                try
                {
                    if (key >= strings_in_byte.Count())
                    {
                        throw new my_exception("key out of size - returned empty string");
                    }
                    string buf_string, buf_string_for_return;
                    if (key == 0)
                    {
                        byte[] small_buf_array = new byte[strings_in_byte[0]];
                        current_file_stream.Seek(BOM_bytes, SeekOrigin.Begin);
                        current_file_stream.Read(small_buf_array, 0, strings_in_byte[0]);
                        buf_string = encoding_of_file.GetString(small_buf_array);
                    }
                    else
                    {
                        byte[] small_buf_array = new byte[strings_in_byte[key] - strings_in_byte[key - 1]];
                        current_file_stream.Seek(strings_in_byte[key - 1] + BOM_bytes, SeekOrigin.Begin);
                        current_file_stream.Read(small_buf_array, 0, (strings_in_byte[key] -
                        strings_in_byte[key - 1]));
                        buf_string = encoding_of_file.GetString(small_buf_array);
                    }
                    buf_string_for_return = buf_string.Trim(new char[] { '\r', '\n' });
                    return buf_string_for_return;
                }
                catch (my_exception current_exception)
                {
                    Console.WriteLine(current_exception.current_exception);
                    return "";
                }
            }
            public List<string> this[int number_of_string]//свойство возвращаюшее распарсенную строку по индексу в strings_in_byte
            {
                get
                {
                    try
                    {
                    //если запрошенный номер строки корректен (строка с таким номером есть в файле)
                    //то загружаем эту строку из файла и возвращаем List из строк, каждая из которых 
                    //будет содержать значение одной из колонок
                            if (number_of_string >= 0 && number_of_string < strings_in_byte.Count())
                            {
                                List<string> string_parsed = new List<string>(
                                load_string_from_file(number_of_string).Split(new char[] { ' ' }, 3));
                                return string_parsed;
                            }
                            //иначе кидаем исключение
                            else
                            {
                                throw new my_exception("file contains no such string - returned empty strings");
                            }
                    }
                    catch (my_exception current_exeption)
                    {
                        Console.WriteLine(current_exeption.current_exception);
                        return new List<string> {"","",""};
                    }

                }
            }
            //конструктор current_string_of_file принимающий текущий файловый поток и таблицу окончаний строк(List<int>)
            public current_string_of_file (List<int> new_list_of_bytes, FileStream new_file_stream)
            {
                strings_in_byte = new_list_of_bytes;
                current_file_stream = new_file_stream;
            }
        }
        //class current_string_of_file (END)***********************************************************************

        public strings_of_file_enumerator GetEnumerator()
        {
            return new strings_of_file_enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        /// <summary>
        /// возвращает значение string найденное по индексу ключа и номеру колонки в файле
        /// </summary>
        string found_string(int key_index,int number_of_column)
        {
            int index_of_string_in_file = all_keys[number_of_column - 1][key_index];
            return create_string_from_index[index_of_string_in_file][number_of_column - 1];
        }

        /// <summary>
        /// бинарный поиск по ключам
        /// </summary>
        int binary_search(int number_of_column,string string_for_search)
        {
            bool first_time = true;
            //индексы границ для бинарного поиска строки 
            int buf_index = 0, border_index = -1, buf_last_position = -1;
            while (string_for_search != found_string(buf_index, number_of_column))
            {
                //если первый раз - просто попадаем на середину массива ключей
                if (first_time)
                {
                    buf_index = (all_keys[number_of_column - 1].Count() / 2) - 1;
                    first_time = false;
                }
                else
                {
                    //иначе указываем на середину между buf_index и border_index
                    if (buf_index == border_index)
                    {
                        not_found = true;
                        break;
                    }
                    if (Math.Abs(buf_index - border_index) == 1)
                        buf_index = border_index;
                    else if (buf_index > border_index)
                        buf_index = ((buf_index - border_index) / 2) + border_index;
                    else if (buf_index < border_index)
                        buf_index = ((border_index - buf_index) / 2) + buf_index;
                }
                //в зависимости от результата функции сравнения cmp_strings_function выбираем border_index для следующей 
                //операции бинарного поиска
                if (cmp_strings_function(found_string(buf_index, number_of_column), string_for_search) > 0)
                {
                    if (buf_last_position > 0 && buf_last_position < buf_index)
                        border_index = buf_last_position;
                    else
                        border_index = 0;
                }
                else if (cmp_strings_function(found_string(buf_index, number_of_column), string_for_search) < 0)
                {
                    if (buf_last_position < strings_in_byte.Count() && buf_last_position > buf_index)
                        border_index = buf_last_position;
                    else
                        border_index = strings_in_byte.Count() - 1;
                }
                else
                    break;
                buf_last_position = buf_index;
            }
            return buf_index;
        }

        /// <summary>
        /// заполняет таблицу позиций конца строк (strings_in_byte) в файле 
        /// </summary>
        public void load_pointers_to_strings()
        {
            int max_size_of_one_string = 1024;
            int current_size_of_string = 0;
            buf_int_for_strings_check_points = 0;
            int count_read_bytes = 1;
            try
            {
                //while количество прочитанных байтов больше 0
                while (count_read_bytes != 0)
                {
                    if (strings_in_byte.Count() > 0)//если читаем не с начала файла
                    {
                        current_file_stream.Seek(strings_in_byte[strings_in_byte.Count() - 1], SeekOrigin.Begin);
                    }
                    else//если читаем с начала файла
                    {
                        current_file_stream.Seek(3, SeekOrigin.Begin);
                    }
                    count_read_bytes = current_file_stream.Read(buf_array, 0, 64000);
                    //заносим в цикле значения окончаний строк (в байтах) от начала файла
                    for (int counter = 0; counter < count_read_bytes; ++counter)
                    {
                        if (buf_array[counter] == 10)
                        {
                            strings_in_byte.Add(buf_int_for_strings_check_points);
                            current_size_of_string = 0;
                        }
                        else
                        {
                            ++current_size_of_string;
                            if (current_size_of_string > max_size_of_one_string)
                            {
                                throw new my_exception("Warning: too long string - byte list incomplete");
                            }
                        }
                        ++buf_int_for_strings_check_points;
                    }
                    //если прочитанный обьем данных меньше буфера значит мы достигли конца файла - завершаем цикл
                    if (count_read_bytes < 64000)
                    {
                        break;
                    }
                }
            }
            catch (my_exception current_exception)
            {
                Console.WriteLine(current_exception.current_exception);
            }

            //создаем обьект класса, который будет возвращать строку по индексу в strings_in_byte
            create_string_from_index = new current_string_of_file(strings_in_byte, current_file_stream);
        }

        /// <summary>
        /// создает отсортированные по cmp_strings_function ключи для колонки number_of_key 
        /// </summary>
        public void load_key_for_file(int number_of_key)
        {
            current_string_of_file array_of_strings = new current_string_of_file(strings_in_byte, current_file_stream);
            bool was_insert = false;
            for (int counter = 0; counter < strings_in_byte.Count(); ++counter)
            {
                    if (all_keys[number_of_key - 1].Count() == 0)
                    {
                        all_keys[number_of_key - 1].Add(counter);
                    }
                    else
                    {
                        for (int counter_2 = 0; counter_2 < all_keys[number_of_key - 1].Count(); ++counter_2)
                        {
                            //если cmp_strings_function после сравнения двух строк возращает значение больше 0  
                            if (cmp_strings_function(found_string(counter_2, number_of_key)
                                , create_string_from_index[counter][number_of_key - 1]) > 0)
                            {
                                //вставляем индекс новой строки на место индекса текущей строки
                                all_keys[number_of_key - 1].Insert(counter_2, counter);
                                was_insert = true;
                                break;
                            }
                        }
                        //если функция сравнения ни разу не возвращает значения больше 0
                        if (!was_insert)
                        {
                            //добавляем индекс новой строки в конец списка ключей
                            all_keys[number_of_key - 1].Add(counter);
                        }
                        was_insert = false;
                    }
            }
        }

        /// <summary>
        ///get_string возвращает массив из строк содержащих в колонке number_of_column строку string_for_search 
        /// </summary>
        public List<string> get_string(int number_of_column, string string_for_search)
        {
            List<string> buf_list_of_strings = new List<string>();
            int buf_index = -1;
            try
            {
                //если номер колонки корректен
                if (number_of_column >= 1 && number_of_column <= 3)
                {
                    //то ведем двоичный поиск строки в файле содержащее строку string_for_search в колонке number_of_column
                    buf_index = binary_search(number_of_column, string_for_search);
                    //если строка string_for_search в колонке number_of_column отсутствует в файле
                    if (not_found)
                    {
                        not_found = false;
                        throw new my_exception("database does not contain such string - returned empty list");
                    }
                    //если строка string_for_search найдена то мы находим все строки файла сверху и снизу от найденной строки
                    //в файле (которые содержат строку string_for_search в колонке number_of_column)
                    else
                    {
                        buf_list_of_strings.Add(create_string_from_index.load_string_from_file(all_keys[number_of_column - 1][buf_index]));
                        int copy_of_index = buf_index;
                        while (buf_index > 0)
                        {
                            --buf_index;
                            if (string_for_search != found_string(buf_index, number_of_column))
                                break;
                            buf_list_of_strings.Add(create_string_from_index.load_string_from_file(all_keys[number_of_column - 1][buf_index]));
                        }
                        buf_index = copy_of_index;
                        while (buf_index < (all_keys[number_of_column - 1].Count() - 1))
                        {
                            ++buf_index;
                            if (string_for_search != found_string(buf_index, number_of_column))
                                break;
                            buf_list_of_strings.Add(create_string_from_index.load_string_from_file(all_keys[number_of_column - 1][buf_index]));
                        }
                        return buf_list_of_strings;
                    }
                }
                //исключение на случай если колонка задана неверно
                else
                    throw new my_exception("in database no such column - returned empty List");   
            }
            catch (my_exception current_exception)
            {
                Console.WriteLine(current_exception.current_exception);
                return buf_list_of_strings;
            }
               
        }

        public string this[int number_of_string]
        {
                get
                {
                    try 
                    {
                        current_file_stream.Flush();
                        if (number_of_string >= 0 && number_of_string < strings_in_byte.Count())
                        {
                            if (number_of_string == 0)
                            {
                                byte[] small_buf_array = new byte[strings_in_byte[0] - 4];
                                current_file_stream.Seek(3, SeekOrigin.Begin);
                                current_file_stream.Read(small_buf_array, 0, strings_in_byte[0] - 4);
                                return encoding_of_file.GetString(small_buf_array);
                            }
                            else
                            {
                                byte[] small_buf_array = new byte[strings_in_byte[number_of_string] -
                                    strings_in_byte[number_of_string - 1] - 2];
                                current_file_stream.Seek(strings_in_byte[number_of_string - 1] + 1, SeekOrigin.Begin);
                                current_file_stream.Read(small_buf_array, 0, (strings_in_byte[number_of_string] -
                                    strings_in_byte[number_of_string - 1] - 2));
                                return encoding_of_file.GetString(small_buf_array);
                            }
                        }
                        else
                        {
                            int count_read_bytes = 1;
                            while (number_of_string >= strings_in_byte.Count() && count_read_bytes != 0)
                            {
                                if (strings_in_byte.Count() > 0)
                                {
                                    current_file_stream.Seek(strings_in_byte[strings_in_byte.Count() - 1], SeekOrigin.Begin);
                                }
                                else
                                {
                                    current_file_stream.Seek(0, SeekOrigin.Begin);
                                }
                                count_read_bytes = current_file_stream.Read(buf_array, 0, 64000);
                                for (int counter = 0; counter < count_read_bytes; ++counter)
                                {
                                    if (buf_array[counter] == 10)
                                    {
                                        strings_in_byte.Add(buf_int_for_strings_check_points);
                                    }
                                    ++buf_int_for_strings_check_points;
                                }
                            }
                            if (number_of_string < strings_in_byte.Count() && number_of_string >= 0)
                            {
                                byte[] small_buf_array = null;
                                if (number_of_string > 0)
                                {
                                    small_buf_array = new byte[strings_in_byte[number_of_string] -
                                        strings_in_byte[number_of_string - 1] - 2];
                                }
                                else
                                {
                                    small_buf_array = new byte[strings_in_byte[number_of_string] - 4];
                                }
                                if (number_of_string > 0)
                                {
                                    current_file_stream.Seek(strings_in_byte[number_of_string - 1] + 1, SeekOrigin.Begin);
                                    current_file_stream.Read(small_buf_array, 0, (strings_in_byte[number_of_string] -
                                        strings_in_byte[number_of_string - 1] - 2));
                                }
                                else
                                {
                                    current_file_stream.Seek(3, SeekOrigin.Begin);
                                    current_file_stream.Read(small_buf_array, 0, strings_in_byte[number_of_string] - 4);
                                }
                                return encoding_of_file.GetString(small_buf_array);
                            }
                            else
                            {
                                throw new my_exception("strings_of_file contains no such element");
                            }
                        }
                    }
                    catch (my_exception current_exception)
                    {
                        Console.WriteLine(current_exception.current_exception);
                        return "";
                    }    
                }      
        }

        public strings_of_file (string new_path,do_something new_cmp_function)
        {
            try
            {
                if (new_path.Substring(new_path.Length - 4) != ".txt")
                {
                    throw new my_exception("Error! Invalid format of file");
                }
            }
            catch (my_exception current_exception)
            {
                Console.WriteLine(current_exception.current_exception);
                Console.ReadKey();
                Environment.Exit(0);
            }

            cmp_strings_function = new_cmp_function;
            current_file_stream = new FileStream(new_path, FileMode.Open, FileAccess.Read);
            all_keys.Add(new List<int>());
            all_keys.Add(new List<int>());
            all_keys.Add(new List<int>());
        }
    }
}
