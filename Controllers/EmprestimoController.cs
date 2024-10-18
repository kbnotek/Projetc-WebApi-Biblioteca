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
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoR _emprestimoR;

        public EmprestimoController(EmprestimoR emprestimoR)
        {
            _emprestimoR = emprestimoR;
        }

        // GET: api/<EmprestimoController>
        [HttpGet]
        public ActionResult<List<Emprestimos>> GetAll()
        {
            try
            {
                var emprestimo = _emprestimoR.GetAll();

                if (emprestimo == null || !emprestimo.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum Emprestimo encontrado." });
                }

                var listasemUrl = emprestimo.Select(emprestimo => new Emprestimos
                {
                    Id = emprestimo.Id,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivros = emprestimo.FkLivros
                }).ToList();

                return Ok(listasemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar os empréstimos.", Detalhes = ex.Message });
            }
        }

        // GET api/<EmprestimoController>/5
        [HttpGet("{id}")]
        public ActionResult<Emprestimos> GetById(int id)
        {
            try
            {
                var emprestimo = _emprestimoR.GetById(id);

                if (emprestimo == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                var emprestimoSemUrl = new Emprestimos
                {
                    Id = emprestimo.Id,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivros = emprestimo.FkLivros
                };

                return Ok(emprestimoSemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar o empréstimo.", Detalhes = ex.Message });
            }
        }

        // POST api/<EmprestimoController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] EmprestimoDto novoEmprestimo)
        {
            try
            {
                var emprestimo = new Emprestimos
                {
                    DataEmprestimo = novoEmprestimo.DataEmprestimo,
                    DataDevolucao = novoEmprestimo.DataDevolucao,
                    FkMembro = novoEmprestimo.FkMembro,
                    FkLivros = novoEmprestimo.FkLivros
                };

                _emprestimoR.Add(emprestimo);

                var resultado = new
                {
                    Mensagem = "Emprestimo cadastrado com sucesso!",
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivros = emprestimo.FkLivros
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar o empréstimo.", Detalhes = ex.Message });
            }
        }

        // PUT api/<EmprestimoController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] Emprestimos emprestimoAtualizado)
        {
            try
            {
                var emprestimoExistente = _emprestimoR.GetById(id);

                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                emprestimoExistente.DataEmprestimo = emprestimoAtualizado.DataEmprestimo;
                emprestimoExistente.DataDevolucao = emprestimoAtualizado.DataDevolucao;
                emprestimoExistente.FkMembro = emprestimoAtualizado.FkMembro;
                emprestimoExistente.FkLivros = emprestimoAtualizado.FkLivros;

                _emprestimoR.Update(emprestimoExistente);

                var resultado = new
                {
                    Mensagem = "Emprestimo atualizado com sucesso!",
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataDevolucao = emprestimoExistente.DataDevolucao,
                    FkMembro = emprestimoExistente.FkMembro,
                    FkLivro = emprestimoExistente.FkLivros
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar o empréstimo.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<EmprestimoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var emprestimoExistente = _emprestimoR.GetById(id);

                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                _emprestimoR.Delete(id);

                var resultado = new
                {
                    Mensagem = "Emprestimo excluído com sucesso!",
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataDevolucao = emprestimoExistente.DataDevolucao,
                    FkMembro = emprestimoExistente.FkMembro,
                    FkLivro = emprestimoExistente.FkLivros
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir o empréstimo.", Detalhes = ex.Message });
            }
        }
    }
}
