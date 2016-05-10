using IronRuby;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromPythonAndRubyToExcel.ClassesForScripts.RubyClasses
{
    public class RubyWorker
    {
        private static RubyWorker SingleRubyWorker;
        private static object RubyWorkerGateObject = new object();
        private dynamic RubyScript;
        private dynamic LabObject;

        public static RubyWorker GetRubyWorker()
        {
            if (SingleRubyWorker == null)
            {
                lock (RubyWorkerGateObject)
                {
                    if (SingleRubyWorker == null)
                    {
                        SingleRubyWorker = new RubyWorker();
                    }
                }
            }
            return SingleRubyWorker;
        }

        private RubyWorker()
        {
            ScriptEngine RubyEngine = Ruby.CreateEngine();

            RubyScript = RubyEngine.ExecuteFile(@"..\..\..\RubyScripts\RubyLabScript.txt");
            LabObject = RubyScript.GetNewLab();
        }
    }
}
