using GameAchievementsApp.ViewModels;

namespace GameAchievementsApp.Views;

public partial class GameDetailsAdminPage : ContentPage
{
    // Guardamos uma referência ao ViewModel
    private readonly GameDetailsAdminViewModel _viewModel;

    public GameDetailsAdminPage(GameDetailsAdminViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // Como no Dashboard, usamos o OnAppearing
    // para carregar os dados sempre que a página for exibida.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Diz ao ViewModel para carregar as conquistas
        // com base no JogoId que ele recebeu da navegação.
        await _viewModel.LoadConquistasAsync();
    }
}