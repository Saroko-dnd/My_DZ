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
    public class car
    {
        public struct coordinates_2d
        {
            public double X;
            public double Y;
        }

        public crossroad direction_crossroad;
        public double distance_from_beginning = 0.0;
        public coordinates_2d coordinates;
        public bool arrived = false;

        public car(crossroad new_direction_crossroad, Point new_point)
        {
            direction_crossroad = new_direction_crossroad;
            coordinates.X = (double)new_point.X;
            coordinates.Y = (double)new_point.Y;
        }
    }
}
