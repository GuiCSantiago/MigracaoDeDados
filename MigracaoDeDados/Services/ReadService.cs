using MigracaoDeDados.Data;
using MigracaoDeDados.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MigracaoDeDados.Services
{
    class ReadService
    {
        private DbConnection connection;

        public List<Empresa> EmpresasList { get; } = new List<Empresa>();
        public List<Socio> SociosList { get; } = new List<Socio>();
        public List<Inconsistencia> InconsistenciasList { get; } = new List<Inconsistencia>();

        public void ReadAll(List<string> paths)
        {
            paths.ForEach(x => ReadAndParse(Directory.GetFiles(x)[0]));
        }

        private void ReadAndParse(string path)
        {
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        connection = new DbConnection();
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            DoParse(line);
                        }

                        connection.InsertAllEmpresa(EmpresasList);
                        connection.InsertAllSocio(SociosList);
                        connection.InsertAllInconsistencia(InconsistenciasList);
                    }
                }
            }   
        }

        private void DoParse(string line)
        {
            try
            {
                if (line.StartsWith("1"))
                {
                    var empresa = new Empresa();
                    empresa.Cnpj = line.Substring(3, 14).Trim();
                    empresa.RazaoSocial = line.Substring(18, 150).Trim();
                    empresa.IdMatrizFilial = line.Substring(17, 1).Trim();
                    empresa.NomeFantasia = line.Substring(168, 55).Trim();
                    empresa.SituacaoCadastral = line.Substring(223, 2).Trim();
                    empresa.CapitalSocial = Convert.ToDouble(line.Substring(891, 14).Trim());
                    empresa.DataSituacaoCadastral = DateTime.ParseExact(line.Substring(225, 8).Trim(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                    empresa.Cep = line.Substring(674, 8).Trim();

                    EmpresasList.Add(empresa);
                }
                else if (line.StartsWith("2"))
                {
                    var socio = new Socio();
                    socio.CnpjEmpresa = line.Substring(3, 14).Trim();
                    socio.CnpjCpfSocio = line.Substring(168, 14).Trim();
                    socio.RazaoNomeSocial = line.Substring(18, 150).Trim();
                    socio.IdentificadorSocio = line.Substring(17, 1).Trim();

                    SociosList.Add(socio);
                }
                else if (!line.StartsWith("0") && !line.StartsWith("6"))
                {
                    var inconsistencia = new Inconsistencia();
                    inconsistencia.Line = line;

                    InconsistenciasList.Add(inconsistencia);
                }
            } 
            catch (Exception)
            {
                var inconsistencia = new Inconsistencia();
                inconsistencia.Line = line;
            }
        }
    }
}
