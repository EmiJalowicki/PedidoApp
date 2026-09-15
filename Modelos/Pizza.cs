using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Modelos
{
    internal class Pizza
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool EstaActivo { get; set; }
    }
}
