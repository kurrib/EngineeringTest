using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO.Abstractions;

namespace EngineeringTest
{
    public class FileSystemProcessor
    {
        private readonly IFileSystem _filesystem;
        public FileSystemProcessor() : this (new FileSystem()) { }

        public FileSystemProcessor(IFileSystem fileSystem)
        {
            _filesystem = fileSystem;
        }

        public readonly string columnName = "Source IP";
        public void ProcessFiles(string path, string outputFileName)
        {
            var results = ReadValues(path, outputFileName);

            string[] paths = { path, outputFileName };
            string outputFilePath = Path.Combine(paths);

            WriteToCsv(results, outputFilePath);
        }
        public List<Result> ReadValues(string path, string outputFileName)
        {
            //var files = Directory.GetFiles(path);
            var files = _filesystem.Directory.GetFiles(path);

            if (files.Length == 0)
            {                
                Console.WriteLine("There are no files to read data"); 
            }
            var results = new List<Result>();

            foreach (var file in files)
            {
                var csvTable = new DataTable();

                using (var csvReader = new CsvReader(new StreamReader(_filesystem.File.OpenRead(file)), true))
                {
                    string rawFileName = Path.GetFileNameWithoutExtension(file);
                    var fileName = Regex.Replace(rawFileName, @"[\d-]", string.Empty);
                    var outputFileNameWithoutExt = Path.GetFileNameWithoutExtension(outputFileName);

                    if (fileName.ToUpper() == outputFileNameWithoutExt.ToUpper()) continue;

                    csvTable.Load(csvReader);                    

                    List<FileSystemModal> fileSystemModal = new List<FileSystemModal>();

                    for (int i = 0; i < csvTable.Rows.Count; i++)
                    {
                        fileSystemModal.Add(new FileSystemModal { SourceId = csvTable.Rows[i][columnName].ToString() });

                    }

                    var ips = fileSystemModal.Select(x => x.SourceId).Distinct().ToList();
                    foreach (var ip in ips)
                    {
                        var val = new Result(ip, fileName);
                        results.Add(val);
                    }
                }
            }

            return results;

        }

        public void WriteToCsv(List<Result> values, string filePath)
        {
            var output = values.OrderBy(x => x.SourceIP).ToList();
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(string.Join(", ", "SourceIp", "FileName"));

                foreach (var val in output)
                {
                    writer.WriteLine(string.Join(", ", val.SourceIP, val.Environment));
                }
            }

        }
    }
}
