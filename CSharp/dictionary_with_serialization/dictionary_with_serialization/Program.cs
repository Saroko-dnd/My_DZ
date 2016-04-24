using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.Schema;

namespace dictionary_with_serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializableDictionary<my_name_test_class, int> test_dictionary = new XmlSerializableDictionary<my_name_test_class, int>();
            test_dictionary.Add(new my_name_test_class("Nicholas", "Ryan"), 18);
            test_dictionary.Add(new my_name_test_class("Nicholas", "Ryan"), 43);
            test_dictionary.Add(new my_name_test_class("Nicholas", "Ryan"), 23);
            test_dictionary.Add(new my_name_test_class("Nicholas", "Ryan"), 89);
            XmlSerializer current_serializer = new XmlSerializer(typeof(XmlSerializableDictionary<my_name_test_class, int>));
            FileStream test_file_stream = new FileStream(@"dictionary_another_test.xml",FileMode.OpenOrCreate,FileAccess.Write);
            current_serializer.Serialize(test_file_stream, test_dictionary);
            test_file_stream.Close();
            FileStream test_file_stream_2 = new FileStream(@"dictionary_another_test.xml", FileMode.Open, FileAccess.Read);
            XmlSerializableDictionary<my_name_test_class, int> another_dictionary = (XmlSerializableDictionary<my_name_test_class, int>)current_serializer.
                Deserialize(test_file_stream_2);
            test_file_stream_2.Close();


            Console.ReadKey();
        }
    }
}
