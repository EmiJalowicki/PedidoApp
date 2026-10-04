namespace PedidoApp.PopUps;
using CommunityToolkit.Maui.Views;

public partial class OpcionesPopUp : Popup
{
    private readonly Action _nuevoPedido;
    private readonly Action _abrirMenu;
    public OpcionesPopUp(Action nuevoPedido, Action abrirMenu)
	{
		InitializeComponent();
        _nuevoPedido = nuevoPedido;
        _abrirMenu = abrirMenu;

        HorizontalOptions = LayoutOptions.End;
        VerticalOptions = LayoutOptions.Start;
        Margin = 10;
	}
    private async void Menu_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
        _abrirMenu();
    }
    private async void NuevoPedido_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
        _nuevoPedido();
    }

}