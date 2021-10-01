using MigracaoDeDados.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoDeDados.Data
{
    public class DbConnection
    {
        private MongoClient client; 

        public IMongoDatabase Database { get; set; }

        public DbConnection(IDatabaseSettings settings)
        {
            client = new MongoClient(settings.ConnectionString); 
            Database = client.GetDatabase(settings.DatabaseName);
        }
    }
}
