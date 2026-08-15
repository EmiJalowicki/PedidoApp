using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace PedidoApp
{
    public class PedidoItem : INotifyPropertyChanged
    {
        private string cantidad = "";
        private string consumible = "";
        private string precio = "";
        public string Cantidad
        {
            get => cantidad;
            set
            {
                if (cantidad != value)
                {
                    cantidad = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SubtotalFormateado));
                }
            }
        }
        public string Consumible
        {
            get => consumible;
            set
            {
                if (consumible != value)
                {
                    consumible = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Subtotal));
                    OnPropertyChanged(nameof(SubtotalFormateado));
                }
            }
        }
        public string Precio
        {
            get => precio;
            set
            {
                if (precio != value)
                {
                    precio = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Subtotal));
                }
            }
        }
        public decimal Subtotal
        {
            get
            {
                int.TryParse(Cantidad, out int cantidad);
                decimal.TryParse(Precio, out decimal precio);

                return cantidad * precio;
            }
        }
        public string SubtotalFormateado
        {
            get
            {
                return Subtotal.ToString(
                    "C2",
                    new System.Globalization.CultureInfo("es-AR"));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(
            [CallerMemberName] string? nombre = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nombre));
        }

    }
}
