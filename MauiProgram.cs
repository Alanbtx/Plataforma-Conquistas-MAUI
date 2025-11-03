using Microsoft.Extensions.Logging;
using GameAchievementsApp.Services;
using GameAchievementsApp.ViewModels;
using GameAchievementsApp.Views;

namespace GameAchievementsApp;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Registar o nosso serviço de Base de Dados
        builder.Services.AddSingleton<DatabaseService>();

        // Registar a nossa View e ViewModel de Login
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoginPage>();

        
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<RegisterPage>();

        builder.Services.AddTransient<AdminDashboardViewModel>();
        builder.Services.AddTransient<AdminDashboardPage>();

        builder.Services.AddTransient<GameDetailsAdminViewModel>();
        builder.Services.AddTransient<GameDetailsAdminPage>();

        builder.Services.AddTransient<PlayerDashboardViewModel>();
        builder.Services.AddTransient<PlayerDashboardPage>();

        builder.Services.AddTransient<PlayerGameDetailsViewModel>();
        builder.Services.AddTransient<PlayerGameDetailsPage>();

        builder.Services.AddSingleton<AuthService>();

        builder.Services.AddTransient<MeuProgressoViewModel>();
        builder.Services.AddTransient<MeuProgressoPage>();

        builder.Services.AddTransient<AdminProgressoViewModel>();
        builder.Services.AddTransient<AdminProgressoPage>();

        return builder.Build();
    }
}