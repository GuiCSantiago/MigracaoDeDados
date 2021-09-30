using System;
using System.IO;
using System.IO.Compression;

namespace MigracaoDeDados.Services
{
    class ZipService
    {
        public FileInfo Extract(string pathZip, string pathExtract)
        {
            try
            {
                ZipFile.ExtractToDirectory(pathZip, pathExtract);
            }
            catch(Exception)
            {
                return null;
            }

            FileInfo fileInfo = new FileInfo(Directory.GetFiles(pathExtract)[0]);
            return fileInfo;
        }
    }
}
