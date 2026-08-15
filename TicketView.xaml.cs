using System.Collections.ObjectModel;
namespace PedidoApp;

public partial class TicketView : ContentView
{
	public TicketView()
	{
		InitializeComponent();
		FechaLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
	}

}