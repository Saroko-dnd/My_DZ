using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;

namespace WebParsing
{
    public class WordAndCount
    {
        public static void AutoGeneratingColumnForDataGrid(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "Word")
            {
                e.Column.Header = MyResourses.Texts.Word;
            }
            if (headername == "Count")
            {
                e.Column.Header = MyResourses.Texts.Count;
            }
        }

        private string word = string.Empty;
        private int count = 0;
        public string Word
        {
            get
            {
                return word;
            }

            set
            {
                word = value;
            }
        }
        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }

        public WordAndCount(string NewWord, int CountForWord)
        {
            Word = NewWord;
            Count = CountForWord;
        }
    }
}
