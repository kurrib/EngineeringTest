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
        public static readonly string FileFolderName = "Engineering Test Files";
        public static readonly string OutputFile = "Combined.csv";
        static void Main(string[] args)
        {           

            string rootPath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\" + FileFolderName;            
            var fileSystemProcessor = new FileSystemProcessor();

            fileSystemProcessor.ProcessFiles(rootPath, OutputFile);

            Console.WriteLine("File processing completed!");
            Console.ReadLine();
        }
       
    }
}
