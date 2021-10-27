using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Wishlist.Desafio.Domains;
using Senai.Wishlist.Desafio.Enums;
using Senai.Wishlist.Desafio.Interfaces;
using Senai.Wishlist.Desafio.Repositories;

namespace Senai.Wishlist.Desafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository UsuarioRepository { get; set; }
        public EmailController Email { get; set; }

        public UsuariosController()
        {
            UsuarioRepository = new UsuarioRepository();
            Email = new EmailController();
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(Usuarios usuario)
        {
            try
            {
                UsuarioRepository.CadastrarUsuario(usuario);

                Email.Enviar(usuario, ETiposEmail.AoCadastrarUsuario);

                return Ok();
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