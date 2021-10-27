using Senai.Wishlist.Desafio.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Wishlist.Desafio.Interfaces
{
    public interface IVerboRepository
    {
        /// <summary>
        /// Lista todos os verbos cadastrados no sistema.
        /// </summary>
        /// <returns>Retorna uma List<Verbos></returns>
        List<Verbos> ListarTodosVerbos();
    }
}
