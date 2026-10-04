namespace PedidoApp.Vistas;

public partial class TamanioHeladoView : ContentView
{
	public TamanioHeladoView()
	{
		InitializeComponent();
	}
    public void Configurar(string medida)
    {
        MedidaLabel.Text = medida;
    }

}