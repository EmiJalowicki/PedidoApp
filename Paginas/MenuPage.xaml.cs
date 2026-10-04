namespace PedidoApp.Paginas;

public partial class MenuPage : ContentPage
{
    public MenuPage()
    {
        InitializeComponent();
    }
    //Botones
    private async void AgregarMenu_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync(
            "Agregar",
            "Acá vamos a agregar un elemento al menú.",
            "Aceptar");
    }

    private void PizzaHeader_Tapped(object sender, TappedEventArgs e)
    {
        PizzaContenido.IsVisible = !PizzaContenido.IsVisible;
        PizzaFlecha.Text = PizzaContenido.IsVisible ? "▼" : "▶";
    }

    private void EmpanadaHeader_Tapped(object sender, TappedEventArgs e)
    {
        EmpanadaContenido.IsVisible = !EmpanadaContenido.IsVisible;
        EmpanadaFlecha.Text = EmpanadaContenido.IsVisible ? "▼" : "▶";
    }

    private void MilanesaHeader_Tapped(object sender, TappedEventArgs e)
    {
        MilanesaContenido.IsVisible = !MilanesaContenido.IsVisible;
        MilanesaFlecha.Text = MilanesaContenido.IsVisible ? "▼" : "▶";
    }

    private void BebidaHeader_Tapped(object sender, TappedEventArgs e)
    {
        BebidaContenido.IsVisible = !BebidaContenido.IsVisible;
        BebidaFlecha.Text = BebidaContenido.IsVisible ? "▼" : "▶";
    }

    private void HeladoHeader_Tapped(object sender, TappedEventArgs e)
    {
        HeladoContenido.IsVisible = !HeladoContenido.IsVisible;
        HeladoFlecha.Text = HeladoContenido.IsVisible ? "▼" : "▶";
    }
}