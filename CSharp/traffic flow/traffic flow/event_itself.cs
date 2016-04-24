using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_flow
{
    class event_itself
    {
        static public delegate void MainEventHandler(object current_object, object current_event);
        static public event MainEventHandler event_was_created;
        public static Random main_random = new Random();
        //очередь событий
        public static List<MainEventHandler> list_of_all_events;
        static public void handle_of_all_events(object current_object, object current_event);
        static public void add_event_to_queue(MainEventHandler new_event)
        {
            list_of_all_events.Add(new_event);
        }
    }
}
