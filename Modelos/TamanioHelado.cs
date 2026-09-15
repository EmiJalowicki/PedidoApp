using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Modelos
{
    internal class TamanioHelado
    {
        public int ID { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Orden {  get; set; }
        public decimal Precio { get; set; }
        public bool EstaActivo { get; set; }
    }
}
