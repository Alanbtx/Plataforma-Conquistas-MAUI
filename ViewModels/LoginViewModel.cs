// Importa as ferramentas do CommunityToolkit.Mvvm que instalámos
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameAchievementsApp.Services;
using GameAchievementsApp.Views;
using GameAchievementsApp.Models;
using Microsoft.Maui.Dispatching; // Corrigido: Removido o '?' ou espaço extra do final

namespace GameAchievementsApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly AuthService _authService;

        [ObservableProperty]
        private string _nomeUtilizador = string.Empty; // Boa prática: inicializar

        [ObservableProperty]
        private string _password = string.Empty; // Boa prática: inicializar

        public LoginViewModel(DatabaseService databaseService, AuthService authService)
        {
            _databaseService = databaseService;
            _authService = authService; // 3. GUARDA-O
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(NomeUtilizador) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Erro", "Por favor, preenche o nome de utilizador e a password.", "OK");
                return;
            }

            var utilizador = await _databaseService.GetUtilizadorPorNome(NomeUtilizador);

            if (utilizador != null && utilizador.Password == Password)
            {
                
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    _authService.EfetuarLogin(utilizador);

                    if (utilizador.Tipo == TipoUtilizador.Admin)
                    {
                        
                        await Shell.Current.GoToAsync(nameof(AdminDashboardPage));
                    }
                    else // Se for um Jogador
                    {
                        // Removemos o alerta e adicionamos a navegação real!
                        await Shell.Current.GoToAsync(nameof(PlayerDashboardPage));
                    }
                });
            }
            else
            {
                await Shell.Current.DisplayAlert("Erro", "Nome de utilizador ou password inválidos.", "OK");
            }
        }

        [RelayCommand]
        private async Task GoToRegister()
        {
            // Esta navegação é simples (não usa '//') e funciona bem.
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}