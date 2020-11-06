using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Autenticacao;
using WhatSappWeb.Model;
using WhatSappWeb.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatSappWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        // GET: api/<AutenticacaoController>
        [HttpGet]
        public ActionResult Get(int codigoVerificacao)
        {

            using (ClienteService service = new ClienteService())
            {

                var codigo = Convert.ToString(codigoVerificacao);

                var cliente = service.Buscar(x => x.CodigoVerificacao.Conteudo == codigo);

                if (cliente.CodigoVerificacao.Verificado)
                {
                    return Ok("Código já foi verificado!");
                }

                if (cliente == null)
                {
                    return BadRequest("Código invalido tente novamente!");
                }

                cliente.CodigoVerificacao.Verificado = true;
                service.Alterar(x => x.Id == cliente.Id, cliente);

                return Ok("Código validado com sucesso!");
            }
        }

        // GET api/<AutenticacaoController>/5
        [HttpGet]
        public ActionResult Get(string token)
        {

            //terminar o token gerado para empresa 
            //using (ClienteService service = new ClienteService())
            //{
            //    var cliente = service.Buscar(x => x.Token.TokenEmpresa.TokenRefrex.Conteudo == token);

            //    if (cliente == null)
            //    {
            //        return NotFound();
            //    }

            //    var dataExpiracao = DateTime.Parse(data);
            //    var dataAtual = DateTime.Now.AddHours(6);

            //    var tokenAtuall = new TokenCliente()
            //    {
            //        Cliente = { Id = cliente.Id, Email = cliente.Email },
            //        DataExpiracao = dataExpiracao,
            //        Empresa = { Descricao = descricao }
            //    };

            //    var tokenRefrexx = new TokenCliente()
            //    {
            //        Cliente = { Id = cliente.Id, Email = cliente.Email },
            //        DataExpiracao = dataExpiracao,
            //        Empresa = { Descricao = descricao }
            //    };

            //    var tokenAtual = Criptografia.CriptografarToken(tokenAtuall);
            //    var tokenRefrex = Criptografia.CriptografarToken(tokenRefrexx);

            //    cliente.Token.TokenEmpresa = new TokenEmpresa() { Conteudo = tokenAtual, DataExpiracao = dataAtual, Descricao = descricao };
            //    cliente.Token.TokenEmpresa.TokenRefrex = new TokenRefrex() { Conteudo = tokenRefrex, DataExpiracao = dataExpiracao, Descricao = descricao };

            //    service.Alterar(x => x.Id == cliente.Id, cliente);

            //    var resposta = new TokenDTO()
            //    {
            //        Token = {
            //               Conteudo = tokenAtual,
            //               DataExpiracao = dataAtual,
            //            },
            //        TokenRefrex = {
            //                Conteudo = tokenRefrex,
            //                DataExpiracao = dataExpiracao
            //            }
            //    };

            //    return Ok(resposta);
            //}
            //return "value";
            return Ok();
        }

        // POST api/<AutenticacaoController>
        [HttpPost]
        public ActionResult Post(ClienteLoginDTO registro)
        {
            try
            {

                if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

                using (ClienteService service = new ClienteService())
                {
                    var cliente = service.Buscar(x => x.Email == registro.Email && x.Senha == x.Senha);

                    if (cliente == null)
                    {
                        return BadRequest("Cliente não encontrado");
                    }

                    var data = DateTime.Now.AddHours(2);

                    var token = new TokenClienteDTO()
                    {
                        Cliente = { Id = cliente.Id, Email = cliente.Email },
                        DataExpiracao = data,
                    };

                    var tokenFinal = Criptografia.CriptografarToken(token);

                    cliente.Token = new TokenPessoa() { Conteudo = tokenFinal, DataExpiracao = data };

                    service.Alterar(x => x.Id == cliente.Id, cliente);

                    return Ok(tokenFinal);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<AutenticacaoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, string data, string descricao)
        {

            try
            {
                using (ClienteService service = new ClienteService())
                {
                    var cliente = service.Buscar(x => x.Id == id);

                    var dataExpiracao = DateTime.Parse(data);
                    var dataAtual = DateTime.Now.AddHours(6);

                    var tokenAtuall = new TokenClienteDTO()
                    {
                        Cliente = { Id = cliente.Id, Email = cliente.Email },
                        DataExpiracao = dataExpiracao,
                        Empresa = { Descricao = descricao }
                    };

                    var tokenRefrexx = new TokenClienteDTO()
                    {
                        Cliente = { Id = cliente.Id, Email = cliente.Email },
                        DataExpiracao = dataExpiracao,
                        Empresa = { Descricao = descricao }
                    };

                    var tokenAtual = Criptografia.CriptografarToken(tokenAtuall);
                    var tokenRefrex = Criptografia.CriptografarToken(tokenRefrexx);

                    cliente.Token.TokenEmpresa = new EmpresaPessoa() { Conteudo = tokenAtual, DataExpiracao = dataAtual, Descricao = descricao };
                    cliente.Token.TokenEmpresa.TokenRefrex = new TokenRefrex() { Conteudo = tokenRefrex, DataExpiracao = dataExpiracao, Descricao = descricao };

                    service.Alterar(x => x.Id == cliente.Id, cliente);

                    var resposta = new TokenDTO()
                    {
                        Token = {
                           Conteudo = tokenAtual,
                           DataExpiracao = dataAtual,
                        },
                        TokenRefrex = {
                            Conteudo = tokenRefrex,
                            DataExpiracao = dataExpiracao
                        }
                    };

                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        // DELETE api/<AutenticacaoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            using (ClienteService service = new ClienteService())
            {
                var cliente = service.Buscar(x => x.Id == id);

                if (cliente == null)
                {
                    return BadRequest("Cliente não encontrado");
                }

                cliente.Token = null;
                service.Alterar(x => x.Id == cliente.Id, cliente);

                return Ok("Deslogado com sucesso!");
            }
        }
    }
}
