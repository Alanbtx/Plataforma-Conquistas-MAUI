using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameAchievementsApp.Models;
using GameAchievementsApp.Services;
using GameAchievementsApp.Views; // Importante para a navegação
using System.Threading.Tasks;

namespace GameAchievementsApp.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        // Este é o nosso código secreto. Numa aplicação real, ele não estaria
        // aqui de forma tão visível, mas para o nosso projeto é perfeito.
        private const string CodigoSecretoAdmin = "DEVGUGU_ADMIN_2025";

        [ObservableProperty]
        private string _nomeUtilizador;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _confirmarPassword;

        [ObservableProperty]
        private string _codigoAdmin; // Propriedade para o campo opcional

        public RegisterViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        private async Task Register()
        {
            // --- PASSO 1: Validações ---
            if (string.IsNullOrWhiteSpace(NomeUtilizador) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmarPassword))
            {
                await Shell.Current.DisplayAlert("Erro", "Todos os campos, exceto o código de admin, são obrigatórios.", "OK");
                return;
            }

            if (Password != ConfirmarPassword)
            {
                await Shell.Current.DisplayAlert("Erro", "As passwords não coincidem.", "OK");
                return;
            }

            var utilizadorExistente = await _databaseService.GetUtilizadorPorNome(NomeUtilizador);
            if (utilizadorExistente != null)
            {
                await Shell.Current.DisplayAlert("Erro", "Este nome de utilizador já existe. Por favor, escolhe outro.", "OK");
                return;
            }

            // --- PASSO 2: Criar o Utilizador ---
            var novoUtilizador = new Utilizador
            {
                NomeUtilizador = this.NomeUtilizador,
                Password = this.Password // Numa app real, a password seria encriptada!
            };

            // --- PASSO 3: Verificar o Código Secreto ---
            if (!string.IsNullOrWhiteSpace(CodigoAdmin) && CodigoAdmin == CodigoSecretoAdmin)
            {
                novoUtilizador.Tipo = TipoUtilizador.Admin;
                await Shell.Current.DisplayAlert("Sucesso", "Conta de Administrador criada com sucesso!", "OK");
            }
            else
            {
                novoUtilizador.Tipo = TipoUtilizador.Jogador;
                await Shell.Current.DisplayAlert("Sucesso", "Conta de Jogador criada com sucesso!", "OK");
            }

            // --- PASSO 4: Guardar na Base de Dados e Voltar ---
            await _databaseService.SaveUtilizador(novoUtilizador);

            // Navega de volta para a página anterior (Login)
            await Shell.Current.GoToAsync("..");
        }
    }
}