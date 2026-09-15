using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Modelos
{
    internal class BebidaTamanio
    {
        public int BebidaID { get; set; }
        public int TamanioBebidaID { get; set; }
        public decimal Precio {  get; set; }
        public bool EstaActivo { get; set; }
    }
    internal class BebidaConTamanio
    {
        public int BebidaID { get; set; }
        public string BebidaNombre { get; set; } = string.Empty;
        public int TamanioBebidaID { get; set; }
        public string Medida { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool EstaActivo { get; set; }
    }
}
