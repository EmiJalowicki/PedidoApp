namespace PedidoApp.PopUps;
using CommunityToolkit.Maui.Views;

public partial class OpcionesPopUp : Popup
{
    private readonly Action _nuevoPedido;
	public OpcionesPopUp(Action nuevoPedido)
	{
		InitializeComponent();
        _nuevoPedido = nuevoPedido;

        HorizontalOptions = LayoutOptions.End;
        VerticalOptions = LayoutOptions.Start;
        Margin = 10;
	}
    private async void Menu_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
    private async void NuevoPedido_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
        _nuevoPedido();
    }

}