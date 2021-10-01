using MigracaoDeDados.Data;
using MigracaoDeDados.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AcessoDeDados.Services
{
    public class CnpjService
    {
        private DbConnection dbConnection;

        private readonly IMongoCollection<Empresa> _collectionEmpresa;

        private readonly IMongoCollection<Socio> _collectionSocio;

        public CnpjService(IDatabaseSettings settings)
        {
            dbConnection = new DbConnection(settings);
            _collectionEmpresa = dbConnection.Database.GetCollection<Empresa>(settings.EmpresasCollectionName);
            _collectionSocio = dbConnection.Database.GetCollection<Socio>(settings.SociosCollectionName);
        }

        public async Task<Empresa> GetEmpresa(string cnpj)
        {
            var empresa = await _collectionEmpresa.Find(e => e.Cnpj.Equals(cnpj)).FirstOrDefaultAsync();
            if (empresa == null)
                return null;

            empresa.SociosList = await _collectionSocio.Find(socio => socio.CnpjEmpresa.Equals(empresa.Cnpj)).Limit(10).ToListAsync();
            return empresa;
        }

        public async Task<Socio> GetSocio(string cnpj)
        {
            var socio = await _collectionSocio.Find(s => s.CnpjCpfSocio.Equals(cnpj)).FirstOrDefaultAsync();
            if (socio == null)
                return null;

            socio.Empresa = await _collectionEmpresa.Find(e => e.Cnpj.Equals(socio.CnpjEmpresa)).FirstOrDefaultAsync();
            return socio;
        }
    }
}
