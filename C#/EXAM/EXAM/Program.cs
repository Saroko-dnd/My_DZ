using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{

    class Program
    {
        static void Main(string[] args)
        {
            structural_unit car_unit = new structural_unit("car");

            structural_unit shell_unit = new structural_unit("shell");
            shell_unit.all_items.Add(new item("bolt"), 10);
            shell_unit.all_items.Add(new item("list_of_steel"), 8);

            structural_unit wheel_unit = new structural_unit("wheel");
            wheel_unit.all_items.Add(new item("bolt"), 5);
            wheel_unit.all_items.Add(new item("tire"), 1);

            structural_unit engine_unit = new structural_unit("engine");
            engine_unit.all_items.Add(new item("bolt"), 12);
            engine_unit.all_items.Add(new item("piston"), 20);

            structural_unit door_unit = new structural_unit("door");
            door_unit.all_items.Add(new item("bolt"), 6);
            door_unit.all_items.Add(new item("door handle"), 1);
            door_unit.all_items.Add(new item("list_of_steel"), 2);

            shell_unit.all_structural_units.Add(door_unit, 4);
            car_unit.all_structural_units.Add(wheel_unit, 4);
            car_unit.all_structural_units.Add(engine_unit, 1);
            car_unit.all_structural_units.Add(shell_unit, 1);

            structural_unit.show_list_of_items(car_unit,1);

            Console.ReadKey();
    
        }
    }
}
