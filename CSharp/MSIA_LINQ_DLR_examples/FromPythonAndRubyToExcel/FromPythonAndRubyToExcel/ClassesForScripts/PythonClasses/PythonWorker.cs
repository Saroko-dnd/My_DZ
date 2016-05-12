using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Runtime;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Windows;

namespace FromPythonAndRubyToExcel.ClassesForScripts.PythonClasses
{
    public class PythonWorker : IScriptWorker
    {
        private static PythonWorker SinglePythonWorker;
        private static object PythonWorkerGateObject = new object();
        private dynamic PythonScript;
        private dynamic LabObject;

        public static PythonWorker GetPythonWorker()
        {
            if (SinglePythonWorker == null)
            {
                lock (PythonWorkerGateObject)
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
            ScriptEngine PythonEngine = Python.CreateEngine();
            //Добавляем путь для поиска импортируемых в Python скрипт модулей
            if (PathToLibraries != null && PathToLibraries != string.Empty)
            {
                var paths = PythonEngine.GetSearchPaths();
                paths.Add(PathToLibraries);
                PythonEngine.SetSearchPaths(paths);
            }

            try
            {
                PythonScript = PythonEngine.ExecuteFile(FullNameOfScript);
            }
            catch (Exception CurrentException)
            {
                throw new Exception(MyResources.Texts.ScriptExecuteError + "\r\n\r\n" + MyResources.Texts.Details + " " + CurrentException.Message);
            }

            try
            {
                LabObject = PythonScript.GetNewLab();
            }
            catch (Exception CurrentException)
            {
                PythonScript = null;
                throw new Exception(MyResources.Texts.ExecuteMethodError + "\r\n\r\n" + MyResources.Texts.Details + " " + CurrentException.Message);
            }
        }

        private PythonWorker()
        {

        }
    }
}
