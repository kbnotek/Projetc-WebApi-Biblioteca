using biblioteca.Model;
using biblioteca.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioR _funcionarioR;

        public FuncionarioController(FuncionarioR funcionarioR)
        {
            _funcionarioR = funcionarioR;
        }

        // GET: api/<FuncionarioController>
        [HttpGet]
        public ActionResult<List<Funcionarios>> GetAll()
        {
            try
            {
                var funcionario = _funcionarioR.GetAll();

                if (funcionario == null || !funcionario.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum Funcionario encontrado." });
                }

                var listasemUrl = funcionario.Select(funcionario => new Funcionarios
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo
                }).ToList();

                return Ok(listasemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar os funcionários.", Detalhes = ex.Message });
            }
        }

        // GET api/<FuncionarioController>/5
        [HttpGet("{id}")]
        public ActionResult<Funcionarios> GetById(int id)
        {
            try
            {
                var funcionario = _funcionarioR.GetById(id);

                if (funcionario == null)
                {
                    return NotFound(new { Mensagem = "Funcionario não encontrado." });
                }

                var funcionarioSemUrl = new Funcionarios
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo
                };

                return Ok(funcionarioSemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar o funcionário.", Detalhes = ex.Message });
            }
        }

        // POST api/<FuncionarioController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] FuncionarioDto novoFuncionario)
        {
            try
            {
                var funcionario = new Funcionarios
                {
                    Nome = novoFuncionario.Nome,
                    Email = novoFuncionario.Email,
                    Telefone = novoFuncionario.Telefone,
                    Cargo = novoFuncionario.Cargo
                };

                _funcionarioR.Add(funcionario);

                var resultado = new
                {
                    Mensagem = "Funcionario cadastrado com sucesso!",
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar o funcionário.", Detalhes = ex.Message });
            }
        }

        // PUT api/<FuncionarioController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] Funcionarios funcionarioAtualizado)
        {
            try
            {
                var funcionarioExistente = _funcionarioR.GetById(id);

                if (funcionarioExistente == null)
                {
                    return NotFound(new { Mensagem = "Funcionario não encontrado." });
                }

                funcionarioExistente.Nome = funcionarioAtualizado.Nome;
                funcionarioExistente.Email = funcionarioAtualizado.Email;
                funcionarioExistente.Telefone = funcionarioAtualizado.Telefone;
                funcionarioExistente.Cargo = funcionarioAtualizado.Cargo;

                _funcionarioR.Update(funcionarioExistente);

                var resultado = new
                {
                    Mensagem = "Funcionario atualizado com sucesso!",
                    Nome = funcionarioExistente.Nome,
                    Email = funcionarioExistente.Email,
                    Telefone = funcionarioExistente.Telefone,
                    Cargo = funcionarioExistente.Cargo
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar o funcionário.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var funcionarioExistente = _funcionarioR.GetById(id);

                if (funcionarioExistente == null)
                {
                    return NotFound(new { Mensagem = "Funcionario não encontrado." });
                }

                _funcionarioR.Delete(id);

                var resultado = new
                {
                    Mensagem = "Funcionario excluído com sucesso!",
                    Nome = funcionarioExistente.Nome,
                    Email = funcionarioExistente.Email,
                    Telefone = funcionarioExistente.Telefone,
                    Cargo = funcionarioExistente.Cargo
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir o funcionário.", Detalhes = ex.Message });
            }
        }
    }
}
