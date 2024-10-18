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
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaR _categoriaR;

        public CategoriaController(CategoriaR categoriaR)
        {
            _categoriaR = categoriaR;
        }

        // GET: api/<CategoriaController>
        [HttpGet]
        public ActionResult<List<Categorias>> GetAll()
        {
            try
            {
                var categoria = _categoriaR.GetAll();

                if (categoria == null || !categoria.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum categoria encontrado." });
                }

                var listasemUrl = categoria.Select(endereco => new Categorias
                {
                    Id = endereco.Id,
                    Nome = endereco.Nome,
                    Categoria = endereco.Categoria
                }).ToList();

                return Ok(listasemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar as categorias.", Detalhes = ex.Message });
            }
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public ActionResult<Categorias> GetById(int id)
        {
            try
            {
                var categoria = _categoriaR.GetById(id);

                if (categoria == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrado." });
                }

                var categoriaSemUrl = new Categorias
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Categoria = categoria.Categoria,
                };

                return Ok(categoriaSemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar a categoria.", Detalhes = ex.Message });
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] CategoriaDto novaCategoria)
        {
            try
            {
                var categoria = new Categorias
                {
                    Nome = novaCategoria.Nome,
                    Categoria = novaCategoria.Categoria,
                };

                _categoriaR.Add(categoria);

                var resultado = new
                {
                    Mensagem = "Categoria cadastrada com sucesso!",
                    Nome = categoria.Nome,
                    Categoria = categoria.Categoria,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar a categoria.", Detalhes = ex.Message });
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] Categorias categoriaAtualizado)
        {
            try
            {
                var categoriaExistente = _categoriaR.GetById(id);

                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrado." });
                }

                categoriaExistente.Nome = categoriaAtualizado.Nome;
                categoriaExistente.Categoria = categoriaAtualizado.Categoria;

                _categoriaR.Update(categoriaExistente);

                var resultado = new
                {
                    Mensagem = "Categoria atualizada com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Categoria = categoriaExistente.Categoria,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar a categoria.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoriaExistente = _categoriaR.GetById(id);

                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrado." });
                }

                // Chama o método de exclusão que já inclui a verificação de referências
                _categoriaR.Delete(id);

                var resultado = new
                {
                    Mensagem = "Membro excluído com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Categoria = categoriaExistente.Categoria
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
