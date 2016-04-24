using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DynamicLINQexample
{
    public class TestDataClass
    {
        private int dayOfTest;
        private int monthOfTest;
        private int yearOfTest;
        private int temperature;
        private double stress;
        private double deflection;

        public int Temperature
        {
            get
            {
                return temperature;
            }

            set
            {
                temperature = value;
            }
        }

        public double Stress
        {
            get
            {
                return stress;
            }

            set
            {
                stress = value;
            }
        }

        public double Deflection
        {
            get
            {
                return deflection;
            }

            set
            {
                deflection = value;
            }
        }

        public int DayOfTest
        {
            get
            {
                return dayOfTest;
            }

            set
            {
                dayOfTest = value;
            }
        }

        public int MonthOfTest
        {
            get
            {
                return monthOfTest;
            }

            set
            {
                monthOfTest = value;
            }
        }

        public int YearOfTest
        {
            get
            {
                return yearOfTest;
            }

            set
            {
                yearOfTest = value;
            }
        }

        public TestDataClass(int NewDayOfTest, int NewMonthOfTest, int NewYearOfTest, int NewTemperature, double NewStress, double NewDeflection)
        {
            DayOfTest = NewDayOfTest;
            MonthOfTest = NewMonthOfTest;
            YearOfTest = NewYearOfTest;
            Temperature = NewTemperature;
            stress = NewStress;
            deflection = NewDeflection;
        }

        private static Random RandomGenerator = new Random();

        public static List<TestDataClass> GenerateRandomListOfData(int AmountOfData)
        {
            List<TestDataClass> NewRandomListOfData = new List<TestDataClass>();
            for (int counter = 0; counter < AmountOfData; ++counter)
            {
                NewRandomListOfData.Add(new TestDataClass(RandomGenerator.Next(1,28), RandomGenerator.Next(1,12), RandomGenerator.Next(2000, 2016), RandomGenerator.Next(-500, 100000),
                    RandomGenerator.Next(100000), RandomGenerator.Next(100000)));
            }
            return NewRandomListOfData;
        }

        public static void DataGridAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "DayOfTest")
            {
                e.Column.Header = MyResourses.Texts.TestDayLabel;
            }
            else if (e.PropertyName == "MonthOfTest")
            {
                e.Column.Header = MyResourses.Texts.TestMonthLabel;
            }
            else if (e.PropertyName == "YearOfTest")
            {
                e.Column.Header = MyResourses.Texts.TestYearLabel;
            }
            else if (e.PropertyName == "Temperature")
            {
                e.Column.Header = MyResourses.Texts.TemperatureColumn;
            }
            else if (e.PropertyName == "Stress")
            {
                e.Column.Header = MyResourses.Texts.StressColumn;
            }
            else if (e.PropertyName == "Deflection")
            {
                e.Column.Header = MyResourses.Texts.DeflectionColumn;
            }
        }
    }
}
