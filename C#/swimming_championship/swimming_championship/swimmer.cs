using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swimming_championship
{
    public class swimmer
    {
        public int y_coordinate;
        public int average_speed;
        public int current_position = 0;
        public int endurance;
        public bool reached_the_end_of_the_strip = false;

        public void fatigue()
        {
            average_speed -= 10;
        }

        public void move()
        {
            if (!reached_the_end_of_the_strip)
            {
                current_position += Program.main_random.Next(average_speed - 10, average_speed + 20);
                if (current_position >= 1000)
                {
                    current_position -= ((current_position - 1000) * 2);
                    reached_the_end_of_the_strip = true;
                }
            }
            else
            {
                current_position -= Program.main_random.Next(average_speed - 10, average_speed + 20);
                if (current_position < 0)
                    current_position = 0;
            }
        }

        public swimmer()
        { 

        }

        public swimmer(int new_average_speed, int new_endurance, int new_number)
        {
            average_speed = new_average_speed;
            endurance = new_endurance;
            y_coordinate = new_number;
        }

    }
}
