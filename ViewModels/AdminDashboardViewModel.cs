using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameAchievementsApp.Models;
using GameAchievementsApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic; // Precisamos disto para os parâmetros de navegação
using GameAchievementsApp.Views; // Precisamos disto para o nameof(GameDetailsAdminPage)

namespace GameAchievementsApp.ViewModels
{
    // A classe agora é 'partial' para que o [ObservableProperty]
    // possa gerar o código mágico para nós.
    public partial class AdminDashboardViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private string _novoJogoTitulo = string.Empty;

        [ObservableProperty]
        private string _novoJogoPlataforma = string.Empty;

        public ObservableCollection<Jogo> Jogos { get; set; } = new();

        [ObservableProperty]
        Jogo _selectedGame; // Nota: Numa app de produção, usaríamos Jogo?

        // Método chamado automaticamente quando SelectedGame muda
        async partial void OnSelectedGameChanged(Jogo value)
        {
            if (value == null)
                return;

            var navigationParams = new Dictionary<string, object>
            {
                { "jogoId", value.Id }
            };

            await Shell.Current.GoToAsync(nameof(GameDetailsAdminPage), navigationParams);

            SelectedGame = null;
        }

        // --- OS NOVOS COMANDOS ESTÃO AQUI ---

        // Comando para EDITAR um Jogo
        [RelayCommand]
        private async Task EditJogo(Jogo jogo)
        {
            if (jogo == null) return;
            // 1. PEDE O TÍTULO
            string novoTitulo = await Shell.Current.DisplayPromptAsync(
                "Editar Jogo",
                "Novo título do jogo:",
                "OK",
                "Cancelar",
                initialValue: jogo.Titulo);

            if (string.IsNullOrWhiteSpace(novoTitulo)) return;

            string novaPlataforma = await Shell.Current.DisplayPromptAsync(
                "Editar Jogo",
                $"Nova plataforma para {novoTitulo}:",
                "OK",
                "Cancelar",
                initialValue: jogo.Plataforma);

            if (string.IsNullOrWhiteSpace(novaPlataforma)) return;

            jogo.Titulo = novoTitulo;
            jogo.Plataforma = novaPlataforma;

            await _databaseService.UpdateJogoAsync(jogo);
        }

        // Comando para EXCLUIR um Jogo
        [RelayCommand]
        private async Task DeleteJogo(Jogo jogo)
        {
            if (jogo == null) return;

            bool resposta = await Shell.Current.DisplayAlert(
                "Confirmar Exclusão",
                $"Tens a certeza que queres excluir o jogo {jogo.Titulo}?",
                "Sim, Excluir",
                "Cancelar");

            if (resposta)
            {
                await _databaseService.DeleteJogoAsync(jogo);
                Jogos.Remove(jogo);
            }
        }

        [RelayCommand]
        private async Task GoToRelatorioProgresso()
        {
            await Shell.Current.GoToAsync(nameof(AdminProgressoPage));
        }

        // --- FIM DOS NOVOS COMANDOS ---

        public AdminDashboardViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        private async Task AddJogo()
        {
            if (string.IsNullOrWhiteSpace(NovoJogoTitulo) || string.IsNullOrWhiteSpace(NovoJogoPlataforma))
            {
                await Shell.Current.DisplayAlert("Erro", "Por favor, preenche o título e a plataforma do jogo.", "OK");
                return;
            }

            var novoJogo = new Jogo
            {
                Titulo = NovoJogoTitulo,
                Plataforma = NovoJogoPlataforma
            };

            await _databaseService.SaveJogo(novoJogo);
            Jogos.Add(novoJogo);
            NovoJogoTitulo = string.Empty;
            NovoJogoPlataforma = string.Empty;
        }

        public async Task LoadJogosAsync()
        {
            var jogosDaDb = await _databaseService.GetJogos();
            if (Jogos.Count > 0)
            {
                Jogos.Clear();
            }

            foreach (var jogo in jogosDaDb)
            {
                Jogos.Add(jogo);
            }
        }
    }
}