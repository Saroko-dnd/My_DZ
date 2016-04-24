using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace xml_test
{
    class Car
    {
        public string town;
        public string model;
        public int year;
        public string color;
        public int speed;
        public void write_to_file_in_XML(string full_name_of_file)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement buf_element = doc.CreateElement("choosed_car");
            XmlElement buf_element_2 = doc.CreateElement(null,"town",null);
            buf_element_2.InnerText = town;
            XmlElement buf_element_3 = doc.CreateElement(null,"model",null);
            buf_element_3.InnerText = model;
            XmlElement buf_element_4 = doc.CreateElement(null,"color",null);
            buf_element_4.InnerText = color;
            buf_element.SetAttribute("year",null,year.ToString());
            buf_element.SetAttribute("speed",null,speed.ToString());
            buf_element.AppendChild(buf_element_2);
            buf_element.AppendChild(buf_element_3);
            buf_element.AppendChild(buf_element_4);
            doc.AppendChild(buf_element);
            doc.Save(full_name_of_file);
        }
        public Car (XmlNode current_node)
        {
            XmlAttributeCollection current_attributes = current_node.Attributes;
            XmlNodeList current_nodes = current_node.ChildNodes;
            foreach (XmlAttribute cur_attr in current_attributes)
            {
                if (cur_attr.Name == "Year")
                {
                    year = int.Parse(cur_attr.Value);
                }
            }
            foreach (XmlNode cur_node in current_nodes)
            {
                if (cur_node.Name == "Model")
                {
                    model = cur_node.InnerText;
                }
                else if (cur_node.Name == "Color")
                {
                    color = cur_node.InnerText;
                }
                else if (cur_node.Name == "Speed")
                {
                    speed = int.Parse(cur_node.InnerText);
                }
                else if (cur_node.Name == "Manufactured")
                {
                    town = cur_node.InnerText;
                }
                else if (cur_node.Name == "Year")
                {
                    year = int.Parse(cur_node.InnerText);
                }
            }
        }

    }
    class Program
    {
        public static Random main_random = new Random();
        public static List<Car> our_cars = new List<Car>();
        static void displayNode(XmlNode node, int level = 0)
        {
            if (node.Name == "Car")
            {
                our_cars.Add(new Car(node));
            }
            if (node.NodeType == XmlNodeType.Element)
            {
                Console.Write(new String(' ', level) + node.Name);
                foreach (XmlAttribute a in node.Attributes)
                {
                    Console.Write(" {0}='{1}'", a.Name, a.Value);
                }
                Console.WriteLine();
            }
            if (node.NodeType == XmlNodeType.Text)
            {
                Console.WriteLine(new String(' ', level) + '*' + node.Value + '*');
            }


            foreach (XmlNode child in node.ChildNodes)
            {
                displayNode(child, level + 1);
            }
        }


        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"d:\users\SarokoDZ_2\C#\xml_test\Cars.xml");
            XmlNode root = doc.ChildNodes[1];
            displayNode(root);
            our_cars[0].write_to_file_in_XML(@"d:\users\SarokoDZ_2\C#\xml_test\my_car.xml");

            List<apprentice> list_of_students = new List<apprentice>(); 
            List<string> buf_things = new List<string>();
            buf_things.Add("pen");
            buf_things.Add("notebook");
            buf_things.Add("trap");
            List<string> buf_things_2 = new List<string>();
            buf_things_2.Add("pen");
            buf_things_2.Add("notebook");
            buf_things_2.Add("apple");

            for (int counter = 1; counter < 11; ++counter)
            {
                int age_buf = main_random.Next(10,19);
                int GPA_buf = main_random.Next(11);
                string buf_name = "apprentice" + counter;
                if (counter % 2 == 0)
                    list_of_students.Add(new apprentice(buf_name, age_buf, GPA_buf, buf_things));
                else
                    list_of_students.Add(new apprentice(buf_name, age_buf, GPA_buf, buf_things_2));
            }

            XmlSerializer my_xml_serializer = new XmlSerializer(typeof(apprentice));
            BinaryFormatter my_bin_formatter = new BinaryFormatter();
            int count = 1;
            foreach (apprentice current_apprentice in list_of_students)
            {
                FileStream file_stream_first = new FileStream(@"D:\users\SarokoDZ_2\C#\xml_test\" + @"apprentice_" + count.ToString() + @".xml",
                    FileMode.OpenOrCreate, FileAccess.ReadWrite);
                my_xml_serializer.Serialize(file_stream_first, current_apprentice);
                ++count;
                file_stream_first.Close();
            }
            count = 1;
            foreach (apprentice current_apprentice in list_of_students)
            {
                FileStream file_stream_first_2 = new FileStream(@"D:\users\SarokoDZ_2\C#\xml_test\" + @"apprentice_" + count.ToString() + @".bin",
                    FileMode.OpenOrCreate, FileAccess.ReadWrite);
                my_bin_formatter.Serialize(file_stream_first_2, current_apprentice);
                ++count;
                file_stream_first_2.Close();
            }
            Console.WriteLine("**********************************************");
            FileStream file_stream_second = new FileStream(@"D:\users\SarokoDZ_2\C#\xml_test\apprentice_1.xml",
                FileMode.OpenOrCreate, FileAccess.ReadWrite);
            apprentice the_chosen = (apprentice)my_xml_serializer.Deserialize(file_stream_second);
            file_stream_second.Close();
            Console.WriteLine("**********************************************");
            FileStream file_stream_second_2 = new FileStream(@"D:\users\SarokoDZ_2\C#\xml_test\apprentice_1.bin",
                FileMode.OpenOrCreate, FileAccess.ReadWrite);
            apprentice the_chosen_2 = (apprentice)my_bin_formatter.Deserialize(file_stream_second_2);
            file_stream_second_2.Close();
            Console.WriteLine("**********************************************");
            Console.ReadKey();




            /*        XmlNodeList nodes = doc.GetElementsByTagName("Car");
                    foreach (XmlNode node in nodes)
                    {
                        Console.WriteLine("{0} {1}", node["Manufactured"].InnerText,
                        node["Model"].InnerText);
                    }*/
        }
    }

}
