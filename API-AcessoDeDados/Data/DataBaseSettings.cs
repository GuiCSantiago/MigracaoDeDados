using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigracaoDeDados.Data
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string EmpresasCollectionName { get; set; }

        public string SociosCollectionName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string EmpresasCollectionName { get; set; }

        string SociosCollectionName { get; set; }
    }
}
