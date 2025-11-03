using GameAchievementsApp.ViewModels;

namespace GameAchievementsApp.Views;

public partial class MeuProgressoPage : ContentPage
{
    private readonly MeuProgressoViewModel _viewModel;

    public MeuProgressoPage(MeuProgressoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // Carrega o progresso sempre que a página aparecer
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadProgressoAsync();
    }
}