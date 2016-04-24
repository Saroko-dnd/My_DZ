using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_examples_linq
{
    public class DataClass
    {
        private string name;
        private int year;
        private string relatedCommand;
        private int weight;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }

        public string RelatedCommand
        {
            get
            {
                return relatedCommand;
            }

            set
            {
                relatedCommand = value;
            }
        }

        public int Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }

        public DataClass(string NewName, int NewYear,string NewCommand,int NewWeight)
        {
            Name = NewName;
            Year = NewYear;
            RelatedCommand = NewCommand;
            Weight = NewWeight;
        }
    }
}
