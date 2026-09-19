using CommunityToolkit.Maui.Extensions;
using PedidoApp.Modelos;
using PedidoApp.PopUps;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace PedidoApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<PedidoItem> Items { get; set; }

        public string NomCliente { get; set; }
        public string DirCliente { get; set; }
        public string TelCliente { get; set; }

        public decimal Total
        {
            get
            {
                return Items.Sum(item =>
                {
                    int cantidad = item.Cantidad;
                    decimal.TryParse(item.Precio, out decimal precio);
                    return cantidad * precio;
                });
            }
        }
        public string TotalFormateado => Total.ToString("C2", new System.Globalization.CultureInfo("es-AR"));

        public MainPage()
        {
            InitializeComponent();
            Items = new ObservableCollection<PedidoItem>();
            var itemInicio = new PedidoItem();
            itemInicio.PropertyChanged += Item_PropertyChanged;

            Items.Add(itemInicio);
            BindingContext = this;

        }
        //Acciones
        private void DisminuirCantidad_Clicked(object sender, EventArgs e)
        {
            if (sender is Button boton &&
                boton.BindingContext is PedidoItem item)
            {
                int cantidad = item.Cantidad;
                if (cantidad > 0)
                    cantidad--;

                item.Cantidad = cantidad;
            }
        }
        private void AumentarCantidad_Clicked(object sender, EventArgs e)
        {
            if (sender is Button boton &&
                boton.BindingContext is PedidoItem item)
            {
                item.Cantidad++;
            }
        }

        private async void AgregarItem_Clicked(object sender, EventArgs e)
        {
            var item = new PedidoItem();
            item.PropertyChanged += Item_PropertyChanged;

            Items.Add(item);
            await Task.Delay(50);

            Pedidos.ScrollTo(
                item,
                position: ScrollToPosition.End,
                animate: true);
        }
        private void EliminarItem_Clicked(object sender, EventArgs e)
        {
            if (sender is Button boton &&
                boton.BindingContext is PedidoItem item)
            {
                Items.Remove(item);
                ActualizarTotal();
            }
        }
        private void Opciones_Clicked(object sender, EventArgs e)
        {
            this.ShowPopup(new OpcionesPopUp());
        }

        //Cambios y calculos
        private void ActualizarTotal()
        {
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(TotalFormateado));
        }
        private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PedidoItem.Precio) ||
                e.PropertyName == nameof(PedidoItem.Cantidad))
            {
                ActualizarTotal();
            }
        }
        //Evento CrearTicket
        private async void CrearTicket_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TicketPage(this));
        }

    }
}
