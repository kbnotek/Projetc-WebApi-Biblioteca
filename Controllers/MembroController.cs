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
    public class MembroController : ControllerBase
    {
        private readonly MembroR _membroR;

        public MembroController(MembroR membroR)
        {
            _membroR = membroR;
        }

        [HttpGet]
        public ActionResult<List<Membros>> GetAll()
        {
            try
            {
                var membro = _membroR.GetAll();

                if (membro == null || !membro.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum Membro encontrado." });
                }

                var listasemUrl = membro.Select(m => new Membros
                {
                    Id = m.Id,
                    Nome = m.Nome,
                    Email = m.Email,
                    Telefone = m.Telefone,
                    DataCadastro = m.DataCadastro,
                    TipoMembro = m.TipoMembro
                }).ToList();

                return Ok(listasemUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao recuperar os membros.", Detalhes = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Membros> GetById(int id)
        {
            try
            {
                var membro = _membroR.GetById(id);

                if (membro == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                var membroSemUrl = new Membros
                {
                    Id = membro.Id,
                    Nome = membro.Nome,
                    Email = membro.Email,
                    Telefone = membro.Telefone,
                    DataCadastro = membro.DataCadastro,
                    TipoMembro = membro.TipoMembro
                };

                return Ok(membroSemUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao recuperar o membro.", Detalhes = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<object> Post([FromForm] MembroDto novoMembro)
        {
            try
            {
                var membro = new Membros
                {
                    Nome = novoMembro.Nome,
                    Email = novoMembro.Email,
                    Telefone = novoMembro.Telefone,
                    DataCadastro = novoMembro.DataCadastro,
                    TipoMembro = novoMembro.TipoMembro
                };

                _membroR.Add(membro);

                var resultado = new
                {
                    Mensagem = "Membro cadastrado com sucesso!",
                    Nome = membro.Nome,
                    Email = membro.Email,
                    Telefone = membro.Telefone,
                    DataCadastro = membro.DataCadastro,
                    TipoMembro = membro.TipoMembro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar o membro.", Detalhes = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] Membros membroAtualizado)
        {
            try
            {
                var membroExistente = _membroR.GetById(id);

                if (membroExistente == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                membroExistente.Nome = membroAtualizado.Nome;
                membroExistente.Email = membroAtualizado.Email;
                membroExistente.Telefone = membroAtualizado.Telefone;
                membroExistente.DataCadastro = membroAtualizado.DataCadastro;
                membroExistente.TipoMembro = membroAtualizado.TipoMembro;

                _membroR.Update(membroExistente);

                var resultado = new
                {
                    Mensagem = "Membro atualizado com sucesso!",
                    Nome = membroExistente.Nome,
                    Email = membroExistente.Email,
                    Telefone = membroExistente.Telefone,
                    DataCadastro = membroExistente.DataCadastro,
                    TipoMembro = membroExistente.TipoMembro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar o membro.", Detalhes = ex.Message });
            }
        }

        [HttpDelete("{id}")]

        public ActionResult Delete(int id)
        {
            try
            {
                var membroExistente = _membroR.GetById(id);

                if (membroExistente == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                // Chama o método de exclusão que já inclui a verificação de referências
                _membroR.Delete(id);

                var resultado = new
                {
                    Mensagem = "Membro excluído com sucesso!",
                    Nome = membroExistente.Nome,
                    Email = membroExistente.Email,
                    Telefone = membroExistente.Telefone,
                    DataCadastro = membroExistente.DataCadastro,
                    TipoMembro = membroExistente.TipoMembro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao excluir o membro.", Detalhes = ex.Message });
            }
        }

    }
}
