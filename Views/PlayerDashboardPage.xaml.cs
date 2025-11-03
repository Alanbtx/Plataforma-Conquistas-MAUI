using GameAchievementsApp.ViewModels;

namespace GameAchievementsApp.Views;

public partial class PlayerDashboardPage : ContentPage
{
    private readonly PlayerDashboardViewModel _viewModel;

    public PlayerDashboardPage(PlayerDashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // Sempre que a página aparecer, carregamos os jogos
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadJogosAsync();
    }
}