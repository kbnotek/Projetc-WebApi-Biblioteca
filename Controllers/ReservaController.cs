using biblioteca.Model;
using biblioteca.ORM;
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
    public class ReservaController : ControllerBase
    {
        private readonly ReservaR _reservaR;

        public ReservaController(ReservaR reservaR)
        {
            _reservaR = reservaR;
        }

        // GET: api/<ReservaController>
        [HttpGet]
        public ActionResult<List<Reservas>> GetAll()
        {
            try
            {
                var reserva = _reservaR.GetAll();

                if (reserva == null || !reserva.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum Reserva encontrado." });
                }

                var listasemUrl = reserva.Select(reserva => new Reservas
                {
                    Id = reserva.Id,
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro
                }).ToList();

                return Ok(listasemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar as reservas.", Detalhes = ex.Message });
            }
        }

        // GET api/<ReservaController>/5
        [HttpGet("{id}")]
        public ActionResult<Reservas> GetById(int id)
        {
            try
            {
                var reserva = _reservaR.GetById(id);

                if (reserva == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                var reservaSemUrl = new Reservas
                {
                    Id = reserva.Id,
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro
                };

                return Ok(reservaSemUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar a reserva.", Detalhes = ex.Message });
            }
        }

        // POST api/<ReservaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] ReservaDto novaReserva)
        {
            try
            {
                var reserva = new Reservas
                {
                    DataReserva = novaReserva.DataReserva,
                    FkMembro = novaReserva.FkMembro,
                    FkLivro = novaReserva.FkLivro
                };

                _reservaR.Add(reserva);

                var resultado = new
                {
                    Mensagem = "Reserva cadastrada com sucesso!",
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar a reserva.", Detalhes = ex.Message });
            }
        }

        // PUT api/<ReservaController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] Reservas reservaAtualizado)
        {
            try
            {
                var reservaExistente = _reservaR.GetById(id);

                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                reservaExistente.DataReserva = reservaAtualizado.DataReserva;
                reservaExistente.FkMembro = reservaAtualizado.FkMembro;
                reservaExistente.FkLivro = reservaAtualizado.FkLivro;

                _reservaR.Update(reservaExistente);

                var resultado = new
                {
                    Mensagem = "Reserva atualizada com sucesso!",
                    DataReserva = reservaExistente.DataReserva,
                    FkMembro = reservaExistente.FkMembro,
                    FkLivro = reservaExistente.FkLivro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar a reserva.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<ReservaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var reservaExistente = _reservaR.GetById(id);

                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                _reservaR.Delete(id);

                var resultado = new
                {
                    Mensagem = "Reserva excluída com sucesso!",
                    DataReserva = reservaExistente.DataReserva,
                    FkMembro = reservaExistente.FkMembro,
                    FkLivro = reservaExistente.FkLivro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir a reserva.", Detalhes = ex.Message });
            }
        }
    }
}
