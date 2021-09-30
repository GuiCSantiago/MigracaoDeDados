using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MigracaoDeDados.Services
{
    class ReadService
    {
        public void Read(string path)
        {
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {

                }
            }
        }
        public void Parse()
        {

        }
    }
}
