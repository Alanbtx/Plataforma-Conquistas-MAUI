using CommunityToolkit.Mvvm.ComponentModel;
using GameAchievementsApp.Models;
using GameAchievementsApp.Services;
using GameAchievementsApp.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input; 
using GameAchievementsApp.Views; 

namespace GameAchievementsApp.ViewModels
{
    public partial class PlayerDashboardViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        // A lista de jogos que vamos mostrar na tela
        public ObservableCollection<Jogo> Jogos { get; set; } = new();

        // Propriedade para guardar o jogo selecionado
        [ObservableProperty]
        Jogo _selectedGame;

        public PlayerDashboardViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // Método para carregar os jogos (igual ao do Admin)
        public async Task LoadJogosAsync()
        {
            Jogos.Clear();
            var jogosDaDb = await _databaseService.GetJogos();
            foreach (var jogo in jogosDaDb)
            {
                Jogos.Add(jogo);
            }
        }

        // Método chamado automaticamente quando o jogador seleciona um jogo
        async partial void OnSelectedGameChanged(Jogo value)
        {
            if (value == null)
                return;

            // --- ALTERAÇÃO AQUI ---

            // 1. Removemos o alerta
            // await Shell.Current.DisplayAlert("Jogo Selecionado", $"Selecionaste: {value.Titulo}", "OK");

            // 2. Preparamos os parâmetros para enviar (o ID do jogo)
            var navigationParams = new Dictionary<string, object>
            {
                { "jogoId", value.Id }
            };

            // 3. Navegamos para a nova página de detalhes, passando o ID
            await Shell.Current.GoToAsync(nameof(PlayerGameDetailsPage), navigationParams);

            // --- FIM DA ALTERAÇÃO ---

            // Limpa a seleção
            SelectedGame = null;
        }
        [RelayCommand]
        private async Task GoToProgresso()
        {
            await Shell.Current.GoToAsync(nameof(MeuProgressoPage));
        }
    }
}