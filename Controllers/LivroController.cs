using biblioteca.Model;
using biblioteca.ORM;
using biblioteca.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class LivroController : ControllerBase
    {
        private readonly LivroR _livroR;
        public LivroController(LivroR livroR)
        {
            _livroR = livroR;
        }
        // GET: api/<CategoriaController>
        [HttpGet]
        public ActionResult<List<Livros>> GetAll()
        {
            try
            {
                var livro = _livroR.GetAll();

                if (livro == null || !livro.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum Livro encontrado." });
                }

                var listasemUrl = livro.Select(livro => new Livros
                {
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    AnoPublicacao = livro.AnoPublicacao,
                    FkCategoria = livro.FkCategoria
                }).ToList();

                return Ok(listasemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar os livros.", Detalhes = ex.Message });
            }
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public ActionResult<Livros> GetById(int id)
        {
            try
            {
                var livro = _livroR.GetById(id);

                if (livro == null)
                {
                    return NotFound(new { Mensagem = "Livro não encontrado." });
                }

                var livroSemUrl = new Livros
                {
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    AnoPublicacao = livro.AnoPublicacao,
                    FkCategoria = livro.FkCategoria
                };

                return Ok(livroSemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar o livro.", Detalhes = ex.Message });
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] LivroDto novoLivro)
        {
            try
            {
                var livro = new Livros
                {
                    Titulo = novoLivro.Titulo,
                    Autor = novoLivro.Autor,
                    AnoPublicacao = novoLivro.AnoPublicacao,
                    FkCategoria = novoLivro.FkCategoria
                };

                _livroR.Add(livro);

                var resultado = new
                {
                    Mensagem = "Livro cadastrado com sucesso!",
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    AnoPublicacao = livro.AnoPublicacao,
                    FkCategoria = livro.FkCategoria
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar o livro.", Detalhes = ex.Message });
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] Livros livroAtualizado)
        {
            try
            {
                var livroExistente = _livroR.GetById(id);

                if (livroExistente == null)
                {
                    return NotFound(new { Mensagem = "livro não encontrado." });
                }

                livroExistente.Titulo = livroAtualizado.Titulo;
                livroExistente.Autor = livroAtualizado.Autor;
                livroExistente.AnoPublicacao = livroAtualizado.AnoPublicacao;
                livroExistente.FkCategoria = livroAtualizado.FkCategoria;

                _livroR.Update(livroExistente);

                var resultado = new
                {
                    Mensagem = "Livro atualizado com sucesso!",
                    Titulo = livroExistente.Titulo,
                    Autor = livroExistente.Autor,
                    AnoPublicacao = livroExistente.AnoPublicacao,
                    FkCategoria = livroExistente.FkCategoria
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar o livro.", Detalhes = ex.Message });
            }
        }


        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var livroExistente = _livroR.GetById(id);

                if (livroExistente == null)
                {
                    return NotFound(new { Mensagem = "Livro não encontrado." });
                }

                // Chama o método de exclusão que já inclui a verificação de referências
                _livroR.Delete(id);

                var resultado = new
                {
                    Mensagem = "Livro excluído com sucesso!",
                    Titulo = livroExistente.Titulo,
                    Autor = livroExistente.Autor,
                    AnoPublicacao = livroExistente.AnoPublicacao,
                    FkCategoria = livroExistente.FkCategoria

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
