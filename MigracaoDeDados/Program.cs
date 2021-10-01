using MigracaoDeDados.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MigracaoDeDados
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathDownload = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                   .Parent.FullName, "Archives");
            string pathExtract = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.FullName, "Extracted");

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                if (!Directory.Exists(pathDownload))
                    Directory.CreateDirectory(pathDownload);

                if (!Directory.Exists(pathExtract))
                    Directory.CreateDirectory(pathExtract);

                var url = "https://drive.google.com/uc?export=download&id=11JEE8WKSD9_FBAfGfiFq_z-ZtS1bmGeR";
                var downloadService = new DownloadService();
                var fileDownload = downloadService.Download(pathDownload, url);

                Console.WriteLine(fileDownload.Exists ? "Sucesso no download" : "Falha no download");

                var zipService = new ZipService();
                var nome = "arquivo-" + Directory.GetFiles(pathExtract).Length;
                var fileDecompressed = zipService.Extract(fileDownload.FullName, Path.Combine(pathExtract, nome));

                Console.WriteLine(fileDecompressed.Exists ? "Sucesso na extração" : "Falha na extração");

                var folders = new List<string>(Directory.GetDirectories(pathExtract));
                var readService = new ReadService();
                readService.ReadAll(folders);
                Console.WriteLine($"Tempo passado: {stopwatch.ElapsedTicks/TimeSpan.TicksPerSecond}");
                stopwatch.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Directory.Delete(pathDownload, true);
            Directory.Delete(pathExtract, true);           
        }
    }
}
