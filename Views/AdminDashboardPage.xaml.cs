using GameAchievementsApp.ViewModels;

namespace GameAchievementsApp.Views;

public partial class AdminDashboardPage : ContentPage
{
    private readonly AdminDashboardViewModel _viewModel;

    public AdminDashboardPage(AdminDashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // Este método é executado sempre que a página aparece no ecrã.
    // É o local perfeito para carregar os dados.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadJogosAsync();
    }
}