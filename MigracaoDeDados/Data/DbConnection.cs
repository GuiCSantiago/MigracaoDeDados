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
        private MongoClient client = new MongoClient("mongodb://localhost:27017"); //Conectar no servidor

        private IMongoDatabase database;

        private IMongoCollection<Empresa> collectionEmpresa;

        private IMongoCollection<Socio> collectionSocio;

        private IMongoCollection<Inconsistencia> collectionInconsistencia;

        public DbConnection()
        {
            database = client.GetDatabase("MigracaoDeDados");
            collectionEmpresa = database.GetCollection<Empresa>("Empresas");
            collectionSocio = database.GetCollection<Socio>("Socios");
            collectionInconsistencia = database.GetCollection<Inconsistencia>("Inconsistencias");
        }

        public void InsertInconsistencia(Inconsistencia inconsistencia)
        {
            collectionInconsistencia.InsertOne(inconsistencia);
        }

        public void InsertAllEmpresa(List<Empresa> empresas)
        {
            if (empresas.Any())
                collectionEmpresa.InsertMany(empresas);
        }

        public void InsertAllSocio(List<Socio> socios)
        {
            if (socios.Any())
                collectionSocio.InsertMany(socios);
        }

        public void InsertAllInconsistencia(List<Inconsistencia> inconsistencias)
        {
            if (inconsistencias.Any())
                collectionInconsistencia.InsertMany(inconsistencias);
        }
    }
}
