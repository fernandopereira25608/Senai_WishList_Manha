using System;
using System.Collections.Generic;

namespace Senai.Wishlist.Desafio.Domains
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Desejos = new HashSet<Desejos>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<Desejos> Desejos { get; set; }
    }
}
