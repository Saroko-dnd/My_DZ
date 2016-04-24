using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows;

namespace road_traffic
{
    public class road
    {
        public List<car> cars = new List<car>();
        public double max_speed_level;
        public int limit_for_cars;
        public crossroad start_crossroad;
        public crossroad end_crossroad;
        public double length;
        public double offset_x_with_max_speed;
        public double offset_y_with_max_speed;

        public void move_cars()
        {
            for (int counter = 0; counter < cars.Count; ++counter)
            {
                double current_speed_of_current_road = (double)(max_speed_level * Math.Exp(-1.0 * (cars.Count / limit_for_cars)));
                if (current_speed_of_current_road < 0.1)
                    current_speed_of_current_road = 0.1;
                cars[counter].distance_from_beginning += current_speed_of_current_road;
                if (cars[counter].distance_from_beginning >= length)
                {
                    cars[counter].distance_from_beginning = 0;
                    cars[counter].arrived = true;
                }
                if (cars[counter].coordinates.X > 720.0)
                {
                    MessageBox.Show("Alarm");
                    
                }
                if (cars[counter].direction_crossroad == start_crossroad)
                {
                    if (start_crossroad.coordinates.X > end_crossroad.coordinates.X)
                    {
                        cars[counter].coordinates.X += ((((double)Math.Abs(start_crossroad.coordinates.X - end_crossroad.
                            coordinates.X)) / length) * current_speed_of_current_road);
                    }
                    else
                    {
                        cars[counter].coordinates.X -= ((((double)Math.Abs(start_crossroad.coordinates.X - end_crossroad.
                            coordinates.X)) / length) * current_speed_of_current_road);
                    }
                    if (start_crossroad.coordinates.Y > end_crossroad.coordinates.Y)
                    {
                        cars[counter].coordinates.Y += ((((double)Math.Abs(start_crossroad.coordinates.Y - end_crossroad.
                            coordinates.Y)) / length) * current_speed_of_current_road);
                    }
                    else
                    {
                        cars[counter].coordinates.Y -= ((((double)Math.Abs(start_crossroad.coordinates.Y - end_crossroad.
                            coordinates.Y)) / length) * current_speed_of_current_road);
                    }
                }
                else
                {
                    if (start_crossroad.coordinates.X > end_crossroad.coordinates.X)
                    {
                        cars[counter].coordinates.X -= ((((double)Math.Abs(start_crossroad.coordinates.X - end_crossroad.
                            coordinates.X)) / length) * current_speed_of_current_road);
                    }
                    else
                    {
                        cars[counter].coordinates.X += ((((double)Math.Abs(start_crossroad.coordinates.X - end_crossroad.
                            coordinates.X)) / length) * current_speed_of_current_road);
                    }
                    if (start_crossroad.coordinates.Y > end_crossroad.coordinates.Y)
                    {
                        cars[counter].coordinates.Y -= ((((double)Math.Abs(start_crossroad.coordinates.Y - end_crossroad.
                            coordinates.Y)) / length) * current_speed_of_current_road);
                    }
                    else
                    {
                        cars[counter].coordinates.Y += ((((double)Math.Abs(start_crossroad.coordinates.Y - end_crossroad.
                            coordinates.Y)) / length) * current_speed_of_current_road);
                    }
                }
            }
        }

        public road(crossroad new_start_crossroad, crossroad new_end_crossroad, double new_max_speed_level, 
            int new_limit_for_cars)
        {
            start_crossroad = new_start_crossroad;
            end_crossroad = new_end_crossroad;
            max_speed_level = new_max_speed_level;
            limit_for_cars = new_limit_for_cars;

            length = Math.Sqrt((double)Math.Pow((double)Math.Abs(start_crossroad.coordinates.X - end_crossroad.
                coordinates.X), 2.0) + (double)Math.Pow((double)Math.Abs(start_crossroad.coordinates.
                Y - end_crossroad.coordinates.Y), 2.0));

            offset_x_with_max_speed = (((double)Math.Abs(start_crossroad.coordinates.X - end_crossroad.
                coordinates.X)) / length) * (double)max_speed_level;
            offset_y_with_max_speed = (((double)Math.Abs(start_crossroad.coordinates.Y - end_crossroad.
                coordinates.Y)) / length) * (double)max_speed_level;
        }
    }
}
