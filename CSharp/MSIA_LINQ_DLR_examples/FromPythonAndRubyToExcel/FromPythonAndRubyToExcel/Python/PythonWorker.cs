using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Runtime;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace FromPythonAndRubyToExcel.Python
{
    public class PythonWorker
    {
        private static PythonWorker SinglePythonWorker;
        private static object GateObject = new object();

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

        private PythonWorker()
        {           

        }
    }
}
