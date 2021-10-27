using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Wishlist.Desafio.Interfaces;
using Senai.Wishlist.Desafio.Repositories;

namespace Senai.Wishlist.Desafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VerbosController : ControllerBase
    {
        private IVerboRepository VerboRepository { get; set; }

        public VerbosController()
        {
            VerboRepository = new VerboRepository();
        }

        [HttpGet]
        //[Authorize]
        public IActionResult ListarVerbos()
        {
            try
            {
                return Ok(VerboRepository.ListarTodosVerbos());
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensagem = "Erro: " + ex
                });
            }
        }
    }
}