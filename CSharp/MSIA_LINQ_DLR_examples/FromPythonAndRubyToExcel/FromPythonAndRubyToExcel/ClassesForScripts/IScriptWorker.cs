using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromPythonAndRubyToExcel.ClassesForScripts
{
    public interface IScriptWorker
    {
        dynamic GetListOfTests();

        dynamic GetDatesOfTests();

        dynamic GetLabName();

        dynamic GetValueSeparator();

        dynamic GetValueType();

        void LoadScript(string FullNameOfScript);
    }
}
