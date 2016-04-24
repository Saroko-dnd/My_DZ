using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace road_traffic
{
    public partial class main_window : Form
    {
        public int counter_of_cars = 0;
        public List<crossroad> list_of_crossroads = new List<crossroad>();
        public List<road> list_of_roads = new List<road>();
        SolidBrush brush = new SolidBrush(Color.DodgerBlue);
        SolidBrush brush_for_cars = new SolidBrush(Color.Crimson);
        Pen pen_for_roads = new Pen(Color.DarkGreen,3);
        public int limit_for_roads = 10;

        public main_window()
        {
            InitializeComponent();
 
            list_of_crossroads.Add(new crossroad(50, 50));
            list_of_crossroads.Add(new crossroad(250, 50));
            list_of_crossroads.Add(new crossroad(500, 50));
            list_of_crossroads.Add(new crossroad(700, 50));
            list_of_crossroads.Add(new crossroad(50, 600));
            list_of_crossroads.Add(new crossroad(250, 600));
            list_of_crossroads.Add(new crossroad(500, 600));
            list_of_crossroads.Add(new crossroad(700, 600));
            list_of_crossroads.Add(new crossroad(250, 300));
            list_of_crossroads.Add(new crossroad(500, 300));
            list_of_crossroads.Add(new crossroad(50, 300));
            list_of_crossroads.Add(new crossroad(700, 300));

            #region creation of roads
            list_of_crossroads[0].list_of_roads.Add(new road(list_of_crossroads[0], list_of_crossroads[1],
                6.0, limit_for_roads));
            list_of_crossroads[0].list_of_roads.Add(new road(list_of_crossroads[0], list_of_crossroads[10],
                6.0, limit_for_roads));

            list_of_crossroads[1].list_of_roads.Add(list_of_crossroads[0].list_of_roads[0]);
            list_of_crossroads[1].list_of_roads.Add(new road(list_of_crossroads[1], list_of_crossroads[2],
                6.0, limit_for_roads));
            list_of_crossroads[1].list_of_roads.Add(new road(list_of_crossroads[1], list_of_crossroads[8],
                6.0, limit_for_roads));

            list_of_crossroads[2].list_of_roads.Add(list_of_crossroads[1].list_of_roads[1]);
            list_of_crossroads[2].list_of_roads.Add(new road(list_of_crossroads[2], list_of_crossroads[3],
                6.0, limit_for_roads));
            list_of_crossroads[2].list_of_roads.Add(new road(list_of_crossroads[2], list_of_crossroads[9],
                6.0, limit_for_roads));

            list_of_crossroads[3].list_of_roads.Add(list_of_crossroads[2].list_of_roads[1]);
            list_of_crossroads[3].list_of_roads.Add(new road(list_of_crossroads[3], list_of_crossroads[11],
                6.0, limit_for_roads));

            list_of_crossroads[10].list_of_roads.Add(list_of_crossroads[0].list_of_roads[1]);
            list_of_crossroads[10].list_of_roads.Add(new road(list_of_crossroads[10], list_of_crossroads[8],
                6.0, limit_for_roads));
            list_of_crossroads[10].list_of_roads.Add(new road(list_of_crossroads[10], list_of_crossroads[4],
                6.0, limit_for_roads));

            list_of_crossroads[8].list_of_roads.Add(list_of_crossroads[1].list_of_roads[2]);
            list_of_crossroads[8].list_of_roads.Add(list_of_crossroads[10].list_of_roads[1]);
            list_of_crossroads[8].list_of_roads.Add(new road(list_of_crossroads[8], list_of_crossroads[9],
                6.0, limit_for_roads));
            list_of_crossroads[8].list_of_roads.Add(new road(list_of_crossroads[8], list_of_crossroads[5],
                6.0, limit_for_roads));

            list_of_crossroads[9].list_of_roads.Add(list_of_crossroads[2].list_of_roads[2]);
            list_of_crossroads[9].list_of_roads.Add(list_of_crossroads[8].list_of_roads[2]);
            list_of_crossroads[9].list_of_roads.Add(new road(list_of_crossroads[9], list_of_crossroads[11],
                6.0, limit_for_roads));
            list_of_crossroads[9].list_of_roads.Add(new road(list_of_crossroads[9], list_of_crossroads[6],
                6.0, limit_for_roads));

            list_of_crossroads[11].list_of_roads.Add(list_of_crossroads[3].list_of_roads[1]);
            list_of_crossroads[11].list_of_roads.Add(list_of_crossroads[9].list_of_roads[2]);
            list_of_crossroads[11].list_of_roads.Add(new road(list_of_crossroads[11], list_of_crossroads[7],
                6.0, limit_for_roads));

            list_of_crossroads[4].list_of_roads.Add(list_of_crossroads[10].list_of_roads[2]);
            list_of_crossroads[4].list_of_roads.Add(new road(list_of_crossroads[4], list_of_crossroads[5],
                6.0, limit_for_roads));

            list_of_crossroads[5].list_of_roads.Add(list_of_crossroads[4].list_of_roads[1]);
            list_of_crossroads[5].list_of_roads.Add(list_of_crossroads[8].list_of_roads[3]);
            list_of_crossroads[5].list_of_roads.Add(new road(list_of_crossroads[5], list_of_crossroads[6],
                6.0, limit_for_roads));

            list_of_crossroads[6].list_of_roads.Add(list_of_crossroads[9].list_of_roads[3]);
            list_of_crossroads[6].list_of_roads.Add(list_of_crossroads[5].list_of_roads[2]);
            list_of_crossroads[6].list_of_roads.Add(new road(list_of_crossroads[6], list_of_crossroads[7],
                6.0, limit_for_roads));

            list_of_crossroads[7].list_of_roads.Add(list_of_crossroads[11].list_of_roads[2]);
            list_of_crossroads[7].list_of_roads.Add(list_of_crossroads[6].list_of_roads[2]);
            #endregion
            this.Paint += main_window_paint;
        }

        public void main_window_paint(object sender, PaintEventArgs e)
        {
            foreach (crossroad current_crossroad in list_of_crossroads)
            {
                current_crossroad.check_roads(counter_of_cars);
            }
            counter_of_cars = 0;
            foreach (crossroad current_crossroad in list_of_crossroads)
            {
                e.Graphics.FillEllipse(brush, current_crossroad.coordinates.X, current_crossroad.coordinates.Y, 15, 
                    15);
                foreach (road current_road in current_crossroad.list_of_roads)
                {
                    e.Graphics.DrawLine(pen_for_roads, current_road.start_crossroad.coordinates.X + 7,current_road.
                        start_crossroad.coordinates.Y + 7, current_road.end_crossroad.coordinates.X + 7, current_road.
                        end_crossroad.coordinates.Y + 7);
                    foreach (car current_car in current_road.cars)
                    {
                        e.Graphics.FillEllipse(brush_for_cars, (int)current_car.coordinates.X, (int)current_car.coordinates.Y, 15,
                            15);
                        ++counter_of_cars;
                    }
                }
            }
            if (counter_of_cars > 5000)
            {
                MessageBox.Show("Машин больше 5000! Программа завершена.");
                this.Close();
            }
        }

        private void main_window_Paint(object sender, PaintEventArgs e)
        {
            //Rectangle myRectangle = new Rectangle(100, 50, 80, 40);
            //Point[] myPointArray = { new Point(0, 0), new Point(50, 30), new Point(30, 60) };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void main_window_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }
    }
}
