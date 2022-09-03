﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataBaseFirst.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
