using System;
using System.IO;

namespace CodeGeneration
{
    public static class Logger
    {
        public static string Now { get; set; }

        static Logger()
        {
            Now = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        }

        public static void Log(params string[] message)
        {
            File.AppendAllLines(
                $@"C:\Users\burak\Documents\Projects\CityPop\Assets\Code Generation\Code Generation~\Debug\{Now}.log",
                message);
        }
    }
}