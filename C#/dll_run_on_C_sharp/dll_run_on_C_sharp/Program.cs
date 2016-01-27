using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime;


namespace dll_run_on_C_sharp
{
    static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);


        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
    }
    class Program
    {
        //вывод окошка средствами winapi через kernel32.dll и User32.dll
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        private delegate int show_message(IntPtr HWND, string text, string caption, uint flag);
        static void Main(string[] args)
        {
            IntPtr pDll = NativeMethods.LoadLibrary(@"User32.dll");
            IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "MessageBoxA");
            show_message show_current_method = (show_message)Marshal.GetDelegateForFunctionPointer(
                                                                        pAddressOfFunctionToCall,
                                                                        typeof(show_message));
            int theResult = show_current_method(default(System.IntPtr), @"Hello", @"World", 0x00000000);

            bool result = NativeMethods.FreeLibrary(pDll);

            Console.WriteLine(theResult);
            Console.ReadKey();
        }
    }
}
