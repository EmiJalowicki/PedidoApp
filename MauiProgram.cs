using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using PedidoApp.DataBase;

namespace PedidoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Aptos.ttf", "Aptos");
                });
            builder.Services.AddSingleton<InicializarDB>(); //Verifica si existe DB

#if DEBUG
            builder.Logging.AddDebug();
#endif
            MauiApp app = builder.Build();

            InicializarDB inicializador =
                app.Services.GetRequiredService<InicializarDB>();

            inicializador.Inicializar();

            return app;
        }
    }
}
