using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Extension_Methods
{
    public static class new_methods
    {
        public static bool greater_than(this string current_string, string current_string_2)
        {
            if (current_string.Length > current_string_2.Length)
                return true;
            else
            {
                return false;
            }
        }
        public static bool less_than(this string current_string, string current_string_2)
        {
            if (current_string.Length < current_string_2.Length)
                return true;
            else
            {
                return false;
            }
        }
        public static string get_as_string<T>(this IEnumerable<T> current_storage)
        {
            string buf_string = "";
            foreach (T current_element in current_storage)
            {
                buf_string = buf_string + current_element.ToString();
            }
            return buf_string;
        }
        public static string get_as_json<T>(this IEnumerable<T> current_storage)
        {
            StringBuilder buf_string = new StringBuilder("[");
            foreach (T current_element in current_storage)
            {
                buf_string.Append(current_element.ToString());
                buf_string.Append(","); 
            }
            buf_string.Remove(buf_string.Length - 1,1);
            buf_string.Append("]"); 
            return buf_string.ToString();
        }
        public static string get_current_class_as_json(this object current_object)
        {
            StringBuilder buf_string = new StringBuilder("{");
            var all_fields = current_object.GetType().GetFields();
            foreach (var current_field in all_fields)
            {
                buf_string.Append("\"");
                buf_string.Append(current_field.Name);
                buf_string.Append("\":");
                try
                {
                    if ((string)current_field.GetValue(current_object) != null)
                    {
                        buf_string.Append("\"");
                        buf_string.Append(current_field.GetValue(current_object).ToString());
                        buf_string.Append("\"");
                    }
                }
                catch (InvalidCastException)
                {
                    buf_string.Append(new_methods.get_current_class_as_json_(current_field.GetValue(current_object)));
                }
                buf_string.Append(";");
            }
            buf_string.Remove(buf_string.Length - 1,1);
            buf_string.Append("}");
            return buf_string.ToString();
        }

        public static string get_current_class_as_json_<T>(IEnumerable<T> current_object)
        {
            StringBuilder buf_string = new StringBuilder("[");
            foreach (var current_value in current_object)
            {
                buf_string.Append(current_value.ToString());
                buf_string.Append(",");
            }
            buf_string.Remove(buf_string.Length - 1, 1);
            buf_string.Append("]");
            return buf_string.ToString();
        }
        public static string get_current_class_as_json_<T,T_2>(IDictionary<T,T_2> current_object)
        {
            StringBuilder buf_string = new StringBuilder("[");
            foreach (KeyValuePair<T,T_2> current_pair in current_object)
            {
                buf_string.Append("{");
                if (current_pair.Key is string == true)
                {
                    buf_string.Append("\"");
                    buf_string.Append(current_pair.Key.ToString());
                    buf_string.Append("\"");
                }
                else
                    buf_string.Append(new_methods.get_current_class_as_json_(current_pair.Key));
                if (current_pair.Value is string == true)
                {
                    buf_string.Append("\"");
                    buf_string.Append(current_pair.Value.ToString());
                    buf_string.Append("\"");
                }
                else
                    buf_string.Append(new_methods.get_current_class_as_json_(current_pair.Value));
            }
            buf_string.Append("}");
            return buf_string.ToString();
        }
        public static string get_current_class_as_json_(object current_object)
        {
            StringBuilder buf_string = new StringBuilder(current_object.ToString());
            return buf_string.ToString();
        }
    }
}
