using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using RegistryEditProgram.MyResourses;
using System.ComponentModel;
using System.Windows.Controls;

namespace RegistryEditProgram
{
    public class AutoRunProgram
    {

        private string programName;
        public string ProgramName
        {
            get
            {
                return programName;
            }

            set
            {
                programName = value;
            }
        }

        private bool runWhenOSstart;
        public bool RunWhenOSstart
        {
            get
            {
                return runWhenOSstart;
            }

            set
            {
                runWhenOSstart = value;
            }
        }

        public AutoRunProgram(string NewProgramName, bool NewCheckForAutorun)
        {
            ProgramName = NewProgramName;
            RunWhenOSstart = NewCheckForAutorun;
        }

        public static void dataGridAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ProgramName")
            {
                e.Column.Header = MyResourses.FirstTabTexts.ProgramName;
            }
            else if (e.PropertyName == "RunWhenOSstart")
            {
                e.Column.Header = MyResourses.FirstTabTexts.AutorunThisProgram;
            }
        }
    }
}
