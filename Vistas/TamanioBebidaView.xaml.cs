namespace PedidoApp.Vistas;

public partial class TamanioBebidaView : ContentView
{
    public TamanioBebidaView()
    {
        InitializeComponent();
    }

    public void Configurar(string medida)
    {
        MedidaLabel.Text = medida;
    }
}