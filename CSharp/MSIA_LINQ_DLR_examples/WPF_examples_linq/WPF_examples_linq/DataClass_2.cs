using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_examples_linq
{
    public class DataClass_2
    {
        private string commandName;
        private int popularity;
        private int allStaff;

        public string CommandName
        {
            get
            {
                return commandName;
            }

            set
            {
                commandName = value;
            }
        }

        public int Popularity
        {
            get
            {
                return popularity;
            }

            set
            {
                popularity = value;
            }
        }

        public int AllStaff
        {
            get
            {
                return allStaff;
            }

            set
            {
                allStaff = value;
            }
        }

        public DataClass_2(string NewCommandName,int NewPopularity,int NewAllStaff)
        {
            CommandName = NewCommandName;
            Popularity = NewPopularity;
            AllStaff = NewAllStaff;
        }
    }
}
