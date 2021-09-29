using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EngineeringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string rootPath = @"C:\Users\kurribh\source\repos\EngineeringTest\Engineering Test Files";
            string outputFileName = "Combined.csv";
            var fileSystemProcessor = new FileSystemProcessor();

            fileSystemProcessor.ProcessFiles(rootPath, outputFileName);

            Console.WriteLine("File processing completed!");
            Console.ReadLine();
        }
       
    }
}
