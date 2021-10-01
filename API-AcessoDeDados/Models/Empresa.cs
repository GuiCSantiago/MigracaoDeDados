using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoDeDados.Models
{
    public class Empresa
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public string IdMatrizFilial { get; set; }

        public string NomeFantasia { get; set; }

        public string SituacaoCadastral { get; set; }

        public double CapitalSocial { get; set; }

        public DateTime DataSituacaoCadastral { get; set; }

        public string Cep { get; set; }

        public List<Socio> SociosList { get; set; } = new List<Socio>();
    }
}
