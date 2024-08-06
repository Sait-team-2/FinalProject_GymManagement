using Microsoft.Extensions.Logging;
using GymManagement.Data;
using SQLite;
using System.IO;

namespace GymManagement
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GymManagement.db");

            // Registering ClientService
            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ClientService>(s, dbPath));

            // Registering Equipment Service
            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<EquipmentService>(s, dbPath));

            // Registering Booking Service
            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<BookingService>(s, dbPath));

            return builder.Build();
        }
    }
}
