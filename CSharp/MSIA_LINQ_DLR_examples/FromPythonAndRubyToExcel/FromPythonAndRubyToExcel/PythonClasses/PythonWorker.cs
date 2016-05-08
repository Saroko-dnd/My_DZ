using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Runtime;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace FromPythonAndRubyToExcel.PythonClasses
{
    public class PythonWorker
    {
        private static PythonWorker SinglePythonWorker;
        private static object GateObject = new object();
        private dynamic PythonScript;
        private dynamic LabObject;

        public static PythonWorker GetPythonWorker()
        {
            if (SinglePythonWorker == null)
            {
                lock (GateObject)
                {
                    if (SinglePythonWorker == null)
                    {
                        SinglePythonWorker = new PythonWorker();
                    }
                }
            }
            return SinglePythonWorker;
        }

        public dynamic GetListOfTests()
        {
            return LabObject.GetTests();
        }

        private PythonWorker()
        {
            ScriptEngine PythonEngine = Python.CreateEngine();
            //Добавляем путь для поиска импортируемых в Python скрипт модулей
            var paths = PythonEngine.GetSearchPaths();
            paths.Add(@"C:\Program Files (x86)\IronPython 2.7\Lib");
            PythonEngine.SetSearchPaths(paths);

            PythonScript = PythonEngine.ExecuteFile("PythonLabScript.py");
            LabObject = PythonScript.GetNewLab();
        }
    }
}
