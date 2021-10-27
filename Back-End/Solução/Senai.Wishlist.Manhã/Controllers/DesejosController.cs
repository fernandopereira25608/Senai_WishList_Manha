using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public class DesejosController : ControllerBase
    {
        private IDesejoRepository DesejoRepository { get; set; }
        private IUsuarioRepository UsuarioRepository { get; set; }
        private EmailController Email { get; set; }

        public DesejosController()
        {
            DesejoRepository = new DesejoRepository();
            UsuarioRepository = new UsuarioRepository();
            Email = new EmailController();
        }

        [HttpGet]
        //[Authorize]
        public IActionResult ListarDesejos()
        {
            try
            {
                return Ok(DesejoRepository.ListarTodosDesejos());
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensagem = "Erro: " + ex
                });
            }
        }

        [HttpGet("verbo/{verboNome}")]
        //[Authorize]
        public IActionResult ListarPorNomeVerbo(string verboNome)
        {
            try
            {
                return Ok(DesejoRepository.ListarDesejosPorVerbo(verboNome));
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensagem = "Erro: " + ex
                });
            }
        }

        [HttpGet("usuario/{usuarioNome}")]
        //[Authorize]
        public IActionResult ListarPorNomeUsuario(string usuarioNome)
        {
            try
            {
                return Ok(DesejoRepository.ListarDesejosPorNomeUsuario(usuarioNome));
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensagem = "Erro: " + ex
                });
            }
        }

        [HttpPost]
        //[Authorize]
        public IActionResult CadastrarDesejo(Desejos desejo)
        {
            try
            {
                int usuarioId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                desejo.IdUsuario = usuarioId; // Adicionando o usuário que cadastrou o desejo.
                desejo.DataCriacao = DateTime.Now; // Adionando a data de criação do desejo.

                DesejoRepository.CadastrarDesejo(desejo);

                Email.Enviar(UsuarioRepository.BuscarUsuarioPorId(usuarioId), ETiposEmail.AoCadastrarDesejo);
                
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

        [HttpPut]
        //[Authorize]
        public IActionResult AtualizarDesejo(Desejos novoDesejo)
        {
            try
            {
                int usuarioId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                Desejos desejoProcurado = DesejoRepository.BuscarDesejoPorId(novoDesejo.Id);

                if (desejoProcurado == null || desejoProcurado.IdUsuarioNavigation.Id != usuarioId)
                {
                    return BadRequest(new {
                        mensagem = "Desejo não encontrado, verifique se não está tentando apagar um desejo de outro usuário."
                    });
                }

                DesejoRepository.AtualizarDesejo(desejoProcurado, novoDesejo);
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

        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeletarDesejo(int id)
        {
            try
            {
                int usuarioId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                Desejos desejoProcurado = DesejoRepository.BuscarDesejoPorId(id);

                if (desejoProcurado == null || desejoProcurado.IdUsuarioNavigation.Id != usuarioId)
                {
                    return BadRequest(new
                    {
                        mensagem = "Desejo não encontrado, verifique se não está tentando apagar um desejo de outro usuário."
                    });
                }

                DesejoRepository.DeletarDesejo(desejoProcurado);
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