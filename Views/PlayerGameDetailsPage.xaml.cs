using GameAchievementsApp.ViewModels;

namespace GameAchievementsApp.Views;

public partial class PlayerGameDetailsPage : ContentPage
{
    private readonly PlayerGameDetailsViewModel _viewModel;

    public PlayerGameDetailsPage(PlayerGameDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // Sempre que a página aparecer, carrega as conquistas
    // com base no JogoId que o ViewModel recebeu.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadConquistasAsync();
    }
}