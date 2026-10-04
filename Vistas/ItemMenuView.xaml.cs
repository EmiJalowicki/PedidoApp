namespace PedidoApp.Vistas;

public partial class ItemMenuView : ContentView
{
	public ItemMenuView()
	{
		InitializeComponent();
	}
	public void MostrarCategoria(string categoria)
	{
		switch (categoria)
		{
			case "Pizza": case "Empanada": case "Milanesa":
				MostrarFormularioSimple();
				break;

			case "Bebida":
				MostrarFormularioBebida();
				break;

			case "Helado":
				MostrarFormularioHelado();
				break;
		}
	}
    private void MostrarFormularioSimple()
    {
        FormularioSimple.IsVisible = true;
        FormularioBebida.IsVisible = false;
        FormularioHelado.IsVisible = false;
    }

    private void MostrarFormularioBebida()
    {
        FormularioSimple.IsVisible = false;
        FormularioBebida.IsVisible = true;
        FormularioHelado.IsVisible = false;

        TamaniosBebidaLayout.Children.Clear();

        AgregarTamanioPrueba("500 mL");
        AgregarTamanioPrueba("1 L");
        AgregarTamanioPrueba("2,25 L");
    }
    private void AgregarTamanioPrueba(string medida)
    {
        var tamanio = new TamanioBebidaView();

        tamanio.Configurar(medida);

        TamaniosBebidaLayout.Children.Add(tamanio);
    }

    private void MostrarFormularioHelado()
    {
        FormularioSimple.IsVisible = false;
        FormularioBebida.IsVisible = false;
        FormularioHelado.IsVisible = true;

        TamaniosHeladoLayout.Children.Clear();

        AgregarTamanioHelado("Tacita");
        AgregarTamanioHelado("Cucurucho");
        AgregarTamanioHelado("1/4 kg");
        AgregarTamanioHelado("1/2 kg");
        AgregarTamanioHelado("1 kg");
    }
    private void AgregarTamanioHelado(string medida)
    {
        var tamanio = new TamanioHeladoView();

        tamanio.Configurar(medida);

        TamaniosHeladoLayout.Children.Add(tamanio);
    }

}