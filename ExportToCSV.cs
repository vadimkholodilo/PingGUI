using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace PingGUI
{
    static class ExportToCSV
    {
        public static bool Export(ObservableCollection<PingerResults> data, string FileName, string delimeter = ";")
        {
            string csv = "Дата" + delimeter + "время прохождения пакета" + delimeter + "Время жизни пакета";
            foreach (PingerResults obj in data)
            {
                csv += obj.DT + delimeter;
                csv += obj.RTT + delimeter;
                csv += obj.TTL + delimeter;
                csv += "\r\n";
            }
            File.AppendAllText(FileName, csv, Encoding.GetEncoding("windows-1251"));
            return true;
        }
    }
}