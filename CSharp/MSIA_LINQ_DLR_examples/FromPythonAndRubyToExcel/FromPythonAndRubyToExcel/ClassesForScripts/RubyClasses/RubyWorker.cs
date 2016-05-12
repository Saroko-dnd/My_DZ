using IronRuby;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromPythonAndRubyToExcel.ClassesForScripts.RubyClasses
{
    public class RubyWorker : IScriptWorker
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

        public dynamic GetListOfTests()
        {
            return LabObject.GetTests();
        }

        public dynamic GetDatesOfTests()
        {
            return LabObject.GetDatesOfTests();
        }

        public dynamic GetLabName()
        {
            return LabObject.GetLabName();
        }

        public dynamic GetValueSeparator()
        {
            return LabObject.GetValueSeparator();
        }

        public dynamic GetValueType()
        {
            return LabObject.GetValueType();
        }

        public void LoadScript(string FullNameOfScript, string PathToLibraries)
        {
            ScriptEngine RubyEngine = Ruby.CreateEngine();

            if (PathToLibraries != null && PathToLibraries != string.Empty)
            {
                var paths = RubyEngine.GetSearchPaths();
                paths.Add(PathToLibraries);
                RubyEngine.SetSearchPaths(paths);
            }

            try
            {
                RubyScript = RubyEngine.ExecuteFile(FullNameOfScript);
            }
            catch (Exception CurrentException)
            {
                throw new Exception(MyResources.Texts.ScriptExecuteError + "\r\n\r\n" + MyResources.Texts.Details + " " + CurrentException.Message);
            }

            try
            {
                LabObject = RubyScript.GetNewLab();
            }
            catch (Exception CurrentException)
            {
                RubyScript = null;
                throw new Exception(MyResources.Texts.ExecuteMethodError + "\r\n\r\n" + MyResources.Texts.Details + " " + CurrentException.Message);
            }
        }

        private RubyWorker()
        {

        }
    }
}
