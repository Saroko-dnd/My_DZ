using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace creating_of_matrix_for_labirint
{
    class Program
    {
        static void Main(string[] args)
        {
            List<fir_tree> our_forest = new List<fir_tree>();
            Random main_random = new Random();
            int cmp_number = main_random.Next(1,10);
            FileStream current_stream = new FileStream(@"forest.html", FileMode.OpenOrCreate, FileAccess.Write);
            XmlWriter current_writer = XmlWriter.Create(current_stream);
            
            current_writer.WriteStartElement("html");
            current_writer.WriteStartElement("body");
            current_writer.WriteStartElement("svg");
            current_writer.WriteAttributeString("height","1000");
            current_writer.WriteAttributeString("width", "1000");

            for (int counter = 0; counter < cmp_number; ++counter)
            {
                int y_for_rectangle = 0;
                int top_x_of_polygon = main_random.Next(1,1000);
                int top_y_of_polygon = main_random.Next(1,1000);
                int height_of_polygon = main_random.Next(1,1000);
                int width_of_polygon = main_random.Next(1,1000);
                int height_of_rectangle = main_random.Next(1,1000);
                int width_of_rectangle = width_of_polygon / 6;
                int cmp_number_2 = main_random.Next(1,6);
                for (int counter_2 = 0; counter_2 < cmp_number_2; ++counter_2)
                {
                    current_writer.WriteStartElement("polygon");
                    current_writer.WriteAttributeString("points", top_x_of_polygon.ToString() + "," +
                        (top_y_of_polygon + ((height_of_polygon / 2) * counter_2)).ToString() + " " +
                        (top_x_of_polygon - (width_of_polygon / 2)).ToString() + "," +
                        (top_y_of_polygon + (height_of_polygon + ((height_of_polygon / 2) * counter_2))).ToString() + " " +
                        (top_x_of_polygon + (width_of_polygon / 2)).ToString() + "," + 
                        (top_y_of_polygon + (height_of_polygon + ((height_of_polygon/2)*counter_2))).ToString());
                    current_writer.WriteAttributeString("style","fill:lime;stroke:purple;stroke-width:1");
                    current_writer.WriteEndElement();
                    y_for_rectangle = top_y_of_polygon + (height_of_polygon + ((height_of_polygon / 2) * counter_2));
                }
                current_writer.WriteStartElement("rect");
                current_writer.WriteAttributeString("x", (top_x_of_polygon - (width_of_polygon/6)).ToString());
                current_writer.WriteAttributeString("y", y_for_rectangle.ToString());
                current_writer.WriteAttributeString("width", width_of_rectangle.ToString());
                current_writer.WriteAttributeString("height", height_of_rectangle.ToString());
                current_writer.WriteAttributeString("style", "fill:rgb(153, 102, 51);stroke-width:3;stroke:rgb(0,0,0)");
                current_writer.WriteEndElement();

            }

            current_writer.WriteEndElement();
            current_writer.WriteEndElement();
            current_writer.WriteEndElement();
            current_writer.Close();
            current_stream.Close();
        }
    }
}
