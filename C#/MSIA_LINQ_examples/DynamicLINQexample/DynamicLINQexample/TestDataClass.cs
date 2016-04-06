using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLINQexample
{
    public class TestDataClass
    {
        private DateTime testDate;
        private int temperature;
        private double stress;
        private double deflection;

        public DateTime TestDate
        {
            get
            {
                return testDate;
            }

            set
            {
                testDate = value;
            }
        }

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

        public TestDataClass(DateTime NewTestDate, int NewTemperature, double NewStress, double NewDeflection)
        {
            TestDate = NewTestDate;
            Temperature = NewTemperature;
            stress = NewStress;
            deflection = NewDeflection;
        }
    }
}
