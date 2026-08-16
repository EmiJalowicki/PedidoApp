using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Globalization;

namespace PedidoApp
{
    public partial class MainPage : ContentPage
    {
        private CancellationTokenSource? _debounce;
        public ObservableCollection<PedidoItem> Items { get; set; }
        public decimal Total
        {
            get
            {
                return Items.Sum(item =>
                {
                    int.TryParse(item.Cantidad, out int cantidad);
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
        private void AgregarItem_Clicked(object sender, EventArgs e)
        {
            var item = new PedidoItem();
            item.PropertyChanged += Item_PropertyChanged;

            Items.Add(item);
        }
        private void EliminarItem_Clicked(object sender, EventArgs e)
        {
            if (sender is Button boton &&
                boton.BindingContext is PedidoItem item)
            {
                Items.Remove(item);
                OnPropertyChanged(nameof(Total));
            }
        }
        //Cambios y calculos
        private async void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PedidoItem.Precio))
            {
                _debounce?.Cancel();
                _debounce = new CancellationTokenSource();
                try
                {
                    await Task.Delay(300, _debounce.Token);

                    if (sender is PedidoItem item &&
                    (string.IsNullOrWhiteSpace(item.Cantidad) || item.Cantidad == "0"))
                    {
                        item.Cantidad = "1";
                    }
                    OnPropertyChanged(nameof(Total));
                    OnPropertyChanged(nameof(TotalFormateado));
                }
                catch(TaskCanceledException){}
            }else if (e.PropertyName == nameof(PedidoItem.Cantidad)){
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(TotalFormateado));
            }
        }
        //Evento CrearTicket
        private async void CrearTicket_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TicketPage(this));
        }

    }
}
