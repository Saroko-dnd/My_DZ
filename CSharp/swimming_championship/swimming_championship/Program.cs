using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Threading;

namespace swimming_championship
{
    class Program
    {
        public static Random main_random = new Random();
        public static List<swimmer> all_swimmers = new List<swimmer>();
        public static bool there_is_a_winner = false;

        static void Main(string[] args)
        {
            int addition_number = 0;
            for (int counter = 0; counter < 10; ++ counter)
            {
                all_swimmers.Add(new swimmer(main_random.Next(30, 70), main_random.Next(5, 16), ((30 + (60*counter)) + 
                    addition_number)));
                addition_number += 5;
            }
            competition();
            //Console.ReadKey();
        }

        static void competition()
        {
            int rounds_counter = 0;
            while (!there_is_a_winner)
            {
                XmlSerializer serializer_for_list = new XmlSerializer(typeof(List<swimmer>));
                using (XmlWriter our_writer = XmlWriter.Create(@"competition.xml"))
                {
                    our_writer.WriteProcessingInstruction("xml-stylesheet",
                        "type=\"text/xsl\" href=\"USED-FILE.xslt\"");
                    foreach (swimmer current_swimmer in all_swimmers)
                    {
                        if (current_swimmer.current_position == 0 && rounds_counter > 0)
                            there_is_a_winner = true;
                        if (rounds_counter == current_swimmer.endurance)
                            current_swimmer.fatigue();
                        current_swimmer.move();
                    }
                    serializer_for_list.Serialize(our_writer, all_swimmers);
                    our_writer.Close();
                    ++rounds_counter;
                    //Thread.Sleep(2000);
                }
                if (!there_is_a_winner)
                    File.Delete(@"competition.xml");

            }

        }
    }
}
