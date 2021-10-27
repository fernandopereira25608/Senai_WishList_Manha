using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Wishlist.Desafio.Domains;
using Senai.Wishlist.Desafio.Interfaces;
using Senai.Wishlist.Desafio.Repositories;
using Senai.Wishlist.Desafio.ViewModels;

namespace Senai.Wishlist.Desafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository UsuarioRepository { get; set; }

        public LoginController()
        {
            UsuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult EfetuarLogin(LoginViewModel login)
        {
            try
            {

                Usuarios usuarioProcurado = UsuarioRepository.BuscarUsuarioPorEmailESenha(login);

                if (usuarioProcurado == null)
                {
                    return BadRequest(new
                    {
                        mensagem = "Erro: Usuário não encontrado, e-mail ou senha incorretos."
                    });
                }

                var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioProcurado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioProcurado.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuarioProcurado.Nome), // Adicionar o nome do usuário nas Claims
                    new Claim("nome", usuarioProcurado.Nome) // Adicionar o nome do usuário nas Claims
                    //new Claim(ClaimTypes.Role, usuarioProcurado.IdTipoUsuarioNavigation.Nome) // Adicionar Tipo de usuário
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("wishlist-chave-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Wishlist.WebApi",
                    audience: "Wishlist.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

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