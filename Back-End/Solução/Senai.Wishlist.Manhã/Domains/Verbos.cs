using System;
using System.Collections.Generic;

namespace Senai.Wishlist.Desafio.Domains
{
    public partial class Verbos
    {
        public Verbos()
        {
            Desejos = new HashSet<Desejos>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Desejos> Desejos { get; set; }
    }
}
