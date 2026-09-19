namespace PedidoApp.PopUps;
using CommunityToolkit.Maui.Views;

public partial class OpcionesPopUp : Popup
{
	public OpcionesPopUp()
	{
		InitializeComponent();
        HorizontalOptions = LayoutOptions.End;
        VerticalOptions = LayoutOptions.Start;
        Margin = 10;
	}
    private async void Menu_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }

}