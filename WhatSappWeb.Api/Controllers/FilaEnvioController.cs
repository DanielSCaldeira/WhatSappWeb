using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatSappDatabase.MongoDB.Base;
using WhatSappWeb.Core;
using WhatSappWeb.DTO;
using WhatSappWeb.Model;
using WhatSappWeb.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatSappWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilaEnvioController : BaseControllerBase
    {
        // GET: api/<FilaEnvio>
        [HttpGet]
        public List<FilaEnvioDTO> Get()
        {
            using (FilaEnvioService service = new FilaEnvioService())
            {
                var telefoneOrigem = "5561995757864";
                return service
                    .Listar(x => x.TelefoneOrigem == telefoneOrigem)
                    .Select(x => new FilaEnvioDTO(x))
                    .ToList();
            }


        }

        // GET api/<FilaEnvio>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FilaEnvio>
        [HttpPost]
        public void Post(List<FilaEnvioDTO> registro)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(Erro(ModelState));
            }

            using (FilaEnvioService service = new FilaEnvioService())
            {

                service.Inserir(registro);
            }

        }

        // PUT api/<FilaEnvio>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string registro)
        {
        }

        // DELETE api/<FilaEnvio>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
