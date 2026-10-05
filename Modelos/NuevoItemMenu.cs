using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Modelos
{
    internal class NuevoItemMenu
    {
        public string Categoria { get; set; } = string.Empty;
        public string Nombre {  get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool EstaActivo { get; set; }
    }
}
