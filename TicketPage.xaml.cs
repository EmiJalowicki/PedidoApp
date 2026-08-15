namespace PedidoApp;

public partial class TicketPage : ContentPage
{
	public TicketPage(MainPage mainPage)
	{
		InitializeComponent();
		BindingContext = mainPage;

    }
}