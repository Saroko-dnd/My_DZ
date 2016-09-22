using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryGraphicsBuilder.CodeOfExtractingData
{
    public class Profession
    {
        public string ProfessionName;
        public List<SalaryInfo> ListOfInfoAboutOffers;

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
