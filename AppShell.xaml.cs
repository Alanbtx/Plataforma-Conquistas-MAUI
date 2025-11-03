using GameAchievementsApp.Views;

namespace GameAchievementsApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // ADICIONA ESTA LINHA PARA REGISTAR A ROTA
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(AdminDashboardPage), typeof(AdminDashboardPage));
        Routing.RegisterRoute(nameof(GameDetailsAdminPage), typeof(GameDetailsAdminPage));
        Routing.RegisterRoute(nameof(PlayerDashboardPage), typeof(PlayerDashboardPage));
        Routing.RegisterRoute(nameof(PlayerGameDetailsPage), typeof(PlayerGameDetailsPage));
        Routing.RegisterRoute(nameof(MeuProgressoPage), typeof(MeuProgressoPage));
        Routing.RegisterRoute(nameof(AdminProgressoPage), typeof(AdminProgressoPage));
    }
}