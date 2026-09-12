namespace PedidoApp;

public partial class TicketPage : ContentPage
{
	public TicketPage(MainPage mainPage)
	{
		InitializeComponent();
		BindingContext = mainPage;

    }
    //btn IMPRIMIR
    private void Imprimir_Clicked(object sender, EventArgs e)
    {
#if ANDROID
        PrintHelper.Print(Ticket);
#endif
    }

}