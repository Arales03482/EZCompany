using System;
using System.IO;
using System.Runtime.InteropServices;

namespace EZCompanyMod.Helpers
{
    class ConsoleHelper
    {
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetStdHandle(UInt32 nStdHandle);

        [DllImport("kernel32.dll")]
        internal static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);

        [DllImport("kernel32.dll")]
        internal static extern int AllocConsole();

        [DllImport("kernel32.dll")]
        internal static extern int FreeConsole();

        internal static void CreateConsole()
        {
            AllocConsole();

            // stdout's handle seems to always be equal to 7
            IntPtr defaultStdout = new IntPtr(7);
            IntPtr currentStdout = GetStdHandle(STD_OUTPUT_HANDLE);

            if (currentStdout != defaultStdout)
                // reset stdout
                SetStdHandle(STD_OUTPUT_HANDLE, defaultStdout);

            // reopen stdout
            TextWriter writer = new StreamWriter(Console.OpenStandardOutput())
            { AutoFlush = true };
            Console.SetOut(writer);
        }

        internal static void DestroyConsole()
        {
            FreeConsole();
        }

        private const UInt32 STD_OUTPUT_HANDLE = 0xFFFFFFF5;
    }
}
