using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace road_traffic
{
    public class crossroad
    {
        public Point coordinates;
        public List<road> list_of_roads = new List<road>();
        bool first_time = true;
        public void check_roads(int number_of_cars)
        {
            for (int counter = 0; counter < list_of_roads.Count; ++counter)
            {
                list_of_roads[counter].move_cars();
                if (number_of_cars < 250 && Program.main_random.Next(0, 11) <= 3)
                {
                    if (list_of_roads[counter].start_crossroad == this)
                        list_of_roads[counter].cars.Add(new car(list_of_roads[counter].end_crossroad, coordinates));
                    else
                        list_of_roads[counter].cars.Add(new car(list_of_roads[counter].start_crossroad, coordinates));
                }
            }
            for (int counter = 0; counter < list_of_roads.Count; ++counter)
            {
                List<car> cars_for_remove = new List<car>();
                for (int counter_2 = 0; counter_2 < list_of_roads[counter].cars.Count; ++counter_2)
                {
                    if (list_of_roads[counter].cars[counter_2].arrived && (list_of_roads[counter].cars[counter_2].
                        direction_crossroad == this))
                    {
                        cars_for_remove.Add(list_of_roads[counter].cars[counter_2]);
                        int buf_for_index_of_road = Program.main_random.Next(0, list_of_roads.Count);
                        if (buf_for_index_of_road != counter)
                        {
                            list_of_roads[counter].cars[counter_2].arrived = false;
                            if (list_of_roads[buf_for_index_of_road].start_crossroad == this)
                            {
                                list_of_roads[counter].cars[counter_2].direction_crossroad = 
                                    list_of_roads[buf_for_index_of_road].end_crossroad;
                            }
                            else
                            {
                                list_of_roads[counter].cars[counter_2].direction_crossroad = 
                                    list_of_roads[buf_for_index_of_road].start_crossroad;
                            }
                            list_of_roads[buf_for_index_of_road].cars.Add(list_of_roads[counter].cars[counter_2]);
                            list_of_roads[buf_for_index_of_road].cars[list_of_roads[buf_for_index_of_road].cars.Count - 1].
                                coordinates.X = (double)coordinates.X;
                            list_of_roads[buf_for_index_of_road].cars[list_of_roads[buf_for_index_of_road].cars.Count - 1].
                                coordinates.Y = (double)coordinates.Y;
                        }
                    }
                }
                foreach (car current_car in cars_for_remove)
                {
                    list_of_roads[counter].cars.Remove(current_car);
                }
            }
        }

        public crossroad(int new_x, int new_y)
        {
            coordinates = new Point(new_x, new_y);
        }
    }
}
