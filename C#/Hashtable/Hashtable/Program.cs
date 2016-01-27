using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtable
{
    class Program
    {
        static void Main(string[] args)
        {
            my_hashtable<int, string> my_hash = new my_hashtable<int, string>(100.0f);
            my_hash[1200] = "i am monster";
            my_hash[500] = "i am boy";
            my_hash[800] = "i am lyer";
            my_hash[1300] = "i am champion";
            int test_number_of_element = my_hash.count();
            my_hash.print_all_elements();
            my_hash.delete(1200);
            test_number_of_element = my_hash.count();
            test_number_of_element = my_hash.count();
            IEnumerator<string> new_num = my_hash.GetEnumerator();
            new_num.MoveNext();
            Console.WriteLine(new_num.Current);
            Console.WriteLine(new_num.Current);
        }
    }

    class my_hashtable<T, T2> : IEnumerable//Этот способ создания итератора подходит в первую очередь для foreeach
    {
        public Dictionary<T,T2> [] hash_array;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T2> GetEnumerator() 
        {
            foreach(Dictionary<T,T2> cur_dict in hash_array)
            {
                if (cur_dict.Count() != 0)
                {
                    foreach (KeyValuePair <T,T2> cur_pair in cur_dict)
                    {
                        yield return cur_pair.Value;
                    }
                }
            }
            
        }

        //public HashTable_enumerator 

        public void delete(T key_for_delete)
        {
            int buf_index = key_for_delete.GetHashCode() % hash_array.Length;
            if (hash_array[buf_index].Count() > 0)
            {
                if (hash_array[buf_index].ContainsKey(key_for_delete))
                {
                    hash_array[buf_index].Remove(key_for_delete);
                }
                else
                {
                    throw new System.ArgumentException("hashtable contains no such element");
                }
            }
            else
            {
                throw new System.ArgumentException("hashtable contains no such element");
            }
        }

        public int count()
        {
            int number_of_elements = 0;
            for (int counter = 0; counter < hash_array.Length; ++counter)
            {
                number_of_elements += hash_array[counter].Count();
            }
            return number_of_elements;
        }

        public void print_all_elements()
        {
            for (int counter = 0; counter < hash_array.Length; ++counter)
            {
                if (hash_array[counter].Count() != 0)
                {
                    foreach (KeyValuePair<T, T2> pair in hash_array[counter])
                    {
                        Console.Write("key " + pair.Key + " value " + pair.Value + "\n");
                    }
                }
            }
        }

        public my_hashtable(float size)
        {
            hash_array = new Dictionary<T,T2>[(int)(size * 2.6f)];
            for (int counter = 0; counter < hash_array.Length; ++counter)
            {
                hash_array[counter] = new Dictionary<T, T2>();
            }
        }
        public T2 this[T key]
        {
            get
            {
                int buf_index = key.GetHashCode() % hash_array.Length;
                if (hash_array[buf_index].ContainsKey(key))
                {
                    return hash_array[buf_index][key];
                }
                else
                {
                    throw new System.ArgumentException("hashtable contains no such element");
                }
            }
            set
            {
                int buf_index = key.GetHashCode() % hash_array.Length;
                hash_array[buf_index].Add(key,value);
            }
        }
    }
}
