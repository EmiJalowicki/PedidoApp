namespace PedidoApp.Paginas;

public partial class AgregarMenuPage : ContentPage
{
	public AgregarMenuPage()
	{
		InitializeComponent();

        CategoriaPicker.ItemsSource = new List<string>
        {
            "Pizza",
            "Empanada",
            "Milanesa",
            "Bebida",
            "Helado"
        };
        CategoriaPicker.SelectedIndexChanged += CategoriaPicker_SelectedIndexChanged;

    }
    private void CategoriaPicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (CategoriaPicker.SelectedItem is not string categoria)
        {
            ItemMenu.IsVisible = false;
            return;
        }

        ItemMenu.IsVisible = true;

        ItemMenu.MostrarCategoria(categoria);
    }

}