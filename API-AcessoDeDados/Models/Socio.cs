using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoDeDados.Models
{
    public class Socio
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CnpjEmpresa { get; set; }

        public string RazaoNomeSocial { get; set; }

        public string IdentificadorSocio { get; set; }

        public string CnpjCpfSocio { get; set; }

        public Empresa Empresa { get; set; }
    }
}
