using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{
    class structural_unit
    {
        /// <summary>
        /// хранилище для списка, который будет выведен на экран
        /// </summary>
        private static Dictionary<string, int> buf_list_of_items = new Dictionary<string, int>();
        /// <summary>
        /// выводит на экран список деталей
        /// </summary>
        /// <param name="current_unit"></param>
        /// <param name="number_of_units"></param>
        public static void show_list_of_items(structural_unit current_unit, int number_of_units)
        {
            adding_values_to_main_buffer(current_unit, number_of_units);
            Console.WriteLine(current_unit.name + ":\n\n");
            foreach (KeyValuePair<string, int> current_item in buf_list_of_items)
            {
                Console.WriteLine(current_item.Key + "  " + current_item.Value.ToString());
            }
            buf_list_of_items.Clear();
        }
        /// <summary>
        /// рекурсивно считает количество деталей у машиины и заносит данные в "buf_list_of_items"
        /// </summary>
        /// <param name="current_unit"></param>
        /// <param name="number_of_units"></param>
        private static void adding_values_to_main_buffer(structural_unit current_unit, int number_of_units)
        {
            foreach (KeyValuePair<item, int> current_item in current_unit.all_items)
            {
                int buf_number_of_all_items = 0;
                try
                {
                    buf_number_of_all_items = buf_list_of_items[current_item.Key.name];
                }
                catch (KeyNotFoundException)
                { }
                finally
                {
                    try
                    {
                        buf_number_of_all_items += current_item.Value * number_of_units;
                        buf_list_of_items.Add(current_item.Key.name, buf_number_of_all_items);
                    }
                    catch (ArgumentException)
                    {
                        buf_list_of_items.Remove(current_item.Key.name);
                        buf_list_of_items.Add(current_item.Key.name, buf_number_of_all_items);
                    }

                }
            }
            foreach (KeyValuePair<structural_unit, int> cur_unit in current_unit.all_structural_units)
            {
                adding_values_to_main_buffer(cur_unit.Key, cur_unit.Value);
            }
        }

        public string name;
        public Dictionary<item, int> all_items = new Dictionary<item, int>();
        public Dictionary<structural_unit,int> all_structural_units = new Dictionary<structural_unit,int>();

        public structural_unit(string new_name)
        {
            name = new_name;
        }
        public structural_unit(string new_name, Dictionary<item, int> new_items, Dictionary<structural_unit, int> new_units)
        {
            name = new_name;
            all_items = new_items;
            all_structural_units = new_units;
        }


    }
}
