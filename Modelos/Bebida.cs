using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Modelos
{
    internal class Bebida
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool EsAlcoholica { get; set; }
    }
}
