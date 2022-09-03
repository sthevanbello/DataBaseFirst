using System;
using System.Collections.Generic;

#nullable disable

namespace DataBaseFirst.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            ItemPedidos = new HashSet<ItemPedido>();
        }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime Data { get; set; }
        public int Status { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual ICollection<ItemPedido> ItemPedidos { get; set; }
    }
}
