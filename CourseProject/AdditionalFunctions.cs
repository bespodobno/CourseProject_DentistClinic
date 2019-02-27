using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace CourseProject
{
     public static class AdditionalFunctions
    {
        static string logfile = "log.txt";

        public static bool IsDouble(string text)
        {
            double doubleNumber;

            return double.TryParse(text, out doubleNumber);           
        }

        public static bool IsDateTime(string txtDate)
        {
            DateTime tempDate;
            return DateTime.TryParse(txtDate, out tempDate);
        }

        public static bool IsTimeSpan(string txtTime)
        {
            TimeSpan time;
            return TimeSpan.TryParse(txtTime, out time);
        }

        public static void LogMessageToFile(string msg)

        {
            msg = string.Format("{0:G}: {1}{2}", DateTime.Now, msg, Environment.NewLine);

            File.AppendAllText(logfile, msg);

        }
        /*
    class Log
    {
        static string logfile = "log.txt";
        public static void Save(string text)
        {
            if (!File.Exists(logfile))
            {
                File.CreateText(logfile).Close();
            }
            File.AppendAllText(logfile,DateTime.Now.ToString() + " " + text + Environment.NewLine);
        }
    }*/

    }
}
