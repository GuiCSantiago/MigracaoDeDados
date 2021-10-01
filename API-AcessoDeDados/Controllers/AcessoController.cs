using API_AcessoDeDados.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MigracaoDeDados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_AcessoDeDados.Controllers
{
    [ApiController]
    [Route("cnpj")]
    public class AcessoController : ControllerBase
    {
        private readonly CnpjService _empresaService;

        public AcessoController(CnpjService service)
        {
            _empresaService = service; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("{cnpj:length(14)}")]
        public async Task<ActionResult> Get(string cnpj)
        {
            var empresa = await _empresaService.GetEmpresa(cnpj);
            if (empresa == null)
            {
                var socio = await _empresaService.GetSocio(cnpj);
                if (socio != null)
                    return Ok(socio);
            }
            else
            {
                return Ok(empresa);
            }
            return NotFound();
        }
    }
}
