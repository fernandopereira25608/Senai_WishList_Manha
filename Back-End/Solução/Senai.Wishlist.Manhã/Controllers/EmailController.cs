using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Wishlist.Desafio.Domains;
using Senai.Wishlist.Desafio.Enums;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Senai.Wishlist.Desafio.Controllers
{
    public class EmailController
    {
        static EmailDomain email = new EmailDomain();
        private static string mensagem { get; set; }
        private static string assunto { get; set; }

        static async Task EnviarEmail()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("arielpaixao10@gmail.com", "Ariel Paixão");
            var subject = assunto;
            var to = new EmailAddress(email.EmailDestinatario, email.NomeDestinatario);
            var plainTextContent = mensagem;
            var htmlContent = mensagem;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public void Enviar(Usuarios usuario, ETiposEmail tipoEmail)
        {
            // Utilizado para detectar o tipo de usuário.
            //switch (usuario.IdTipoUsuario)
            //{
            //    case 1:
            //        email.TipoUsuario = "Administrador";
            //        break;
            //    case 2:
            //        email.TipoUsuario = "Médico";
            //        break;
            //    case 3:
            //        email.TipoUsuario = "Paciente";
            //        break;
            //}

            if (tipoEmail == ETiposEmail.AoCadastrarDesejo)
            {
                mensagem = "Obrigado por cadastrar um desejo em nosso sistema!";
                assunto = "Obrigado por cadastrar um desejo";
            }
            else
            {
                mensagem = "Obrigado por se cadastrar em nosso sistema, a partir de agora você pode cadastrar e listar todos os seus desejos para sempre manter suas metas.";
                assunto = "Parabéns por se cadastrar em nosso sistema";
            }

            mensagem = mensagem + " Aplicação desenvolvida por: Ariel Paixão, GitHub: https://github.com/arielmn22, Cândida Paraizo, GitHub: https://github.com/crparaizo e Enzo Panebianco, GitHub: https://github.com/enzopanebianco";

            email.EmailDestinatario = usuario.Email;
            email.NomeDestinatario = usuario.Nome;

            EnviarEmail().Wait();
        }
    }
}