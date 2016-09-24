using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryGraphicsBuilder.CodeOfExtractingData
{
    public class Profession
    {
        private string professionName;
        public List<SalaryInfo> ListOfInfoAboutOffers;

        public string ProfessionName
        {
            get
            {
                return professionName;
            }

            set
            {
                professionName = value;
            }
        }

        public Profession(string NewProfessionName)
        {
            ProfessionName = NewProfessionName;
            ListOfInfoAboutOffers = new List<SalaryInfo>();
        }

        public Profession()
        {
            ProfessionName = string.Empty;
            ListOfInfoAboutOffers = new List<SalaryInfo>();
        }
    }
}
