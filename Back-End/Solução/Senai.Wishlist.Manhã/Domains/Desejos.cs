using System;
using System.Collections.Generic;

namespace Senai.Wishlist.Desafio.Domains
{
    public partial class Desejos
    {
        public int Id { get; set; }
        public int? IdVerbo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual Verbos IdVerboNavigation { get; set; }
    }
}
