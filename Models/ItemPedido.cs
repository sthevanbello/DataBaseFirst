using System;
using System.Collections.Generic;

#nullable disable

namespace DataBaseFirst.Models
{
    public partial class ItemPedido
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdPedido { get; set; }
        public int Quantidade { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; }
        public virtual Produto IdProdutoNavigation { get; set; }
    }
}
