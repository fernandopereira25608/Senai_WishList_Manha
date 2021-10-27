using Senai.Wishlist.Desafio.Domains;
using Senai.Wishlist.Desafio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Wishlist.Desafio.Interfaces
{
    public interface IDesejoRepository
    {
        /// <summary>
        /// Lista todos os desejos cadastrados no sistema.
        /// </summary>
        /// <returns>List<Desejos></returns>
        List<DesejosViewModel> ListarTodosDesejos();

        /// <summary>
        /// Lista os desejos que contêm o verbo passado por parâmetro.
        /// </summary>
        /// <param name="verboNome">Nome do verbo à ser listado</param>
        /// <returns>List<Desejos></returns>
        List<DesejosViewModel> ListarDesejosPorVerbo(string verboNome);

        /// <summary>
        /// Lista os desejos relacionados à um usuário.
        /// </summary>
        /// <param name="usuarioNome">Nome do usuário</param>
        /// <returns>List<Desejos></returns>
        List<DesejosViewModel> ListarDesejosPorNomeUsuario(string usuarioNome);

        /// <summary>
        /// Cadastra um desejo.
        /// </summary>
        void CadastrarDesejo(Desejos desejo);

        /// <summary>
        /// Atualiza um desejo.
        /// </summary>
        /// <param name="desejo">Um Desejos Object</param>
        void AtualizarDesejo(Desejos desejoCadastrado, Desejos desejoNovo);

        /// <summary>
        /// Deleta um desejo do banco de dados
        /// </summary>
        /// <param name="id">Id do desejo a ser deletado.</param>
        void DeletarDesejo(Desejos desejo);

        /// <summary>
        /// Busca um desejo pelo seu Id.
        /// </summary>
        /// <param name="id">Id do desejo</param>
        /// <returns>Retona um 'Desejos' object</returns>
        Desejos BuscarDesejoPorId(int id);

        /// <summary>
        /// Transforma a List<Desejos> em List<DesejosViewModel>
        /// </summary>
        /// <param name="desejos">Lista de desejos</param>
        /// <returns>Retorna uma List<DesejosViewModel></returns>
        List<DesejosViewModel> TranformarEmDesejosViewModel(List<Desejos> desejos);
    }
}
