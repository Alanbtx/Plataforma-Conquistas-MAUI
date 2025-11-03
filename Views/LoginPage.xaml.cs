using GameAchievementsApp.ViewModels;

namespace GameAchievementsApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        // AQUI ligamos a View (a parte visual) ao seu ViewModel (a lógica).
        BindingContext = viewModel;
    }
}