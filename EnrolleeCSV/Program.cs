using System.IO;
using static EnrolleeCSV.EnrolleeTransform;

namespace EnrolleeCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvLines = File.ReadLines(@"c:\test.txt");
            var et = new EnrolleeTransform();
            foreach (InsCoList list in et.Transform(csvLines))
            {
                File.WriteAllLines(list.FileName, list.CsvLines);
            }
        }
    }
}
