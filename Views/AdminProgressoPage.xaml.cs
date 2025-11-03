using GameAchievementsApp.ViewModels;

namespace GameAchievementsApp.Views;

public partial class AdminProgressoPage : ContentPage
{
    private readonly AdminProgressoViewModel _viewModel;

    public AdminProgressoPage(AdminProgressoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // Carrega o relatório sempre que a página aparecer
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadRelatorioAsync();
    }
}