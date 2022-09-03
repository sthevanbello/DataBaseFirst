using System;
using System.Collections.Generic;

#nullable disable

namespace DataBaseFirst.Models
{
    public partial class Produto
    {
        public Produto()
        {
            ItemPedidos = new HashSet<ItemPedido>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public double Decimal { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<ItemPedido> ItemPedidos { get; set; }
    }
}
