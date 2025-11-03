using CommunityToolkit.Mvvm.ComponentModel;
using GameAchievementsApp.Models;
using GameAchievementsApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GameAchievementsApp.ViewModels
{
    public partial class MeuProgressoViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly AuthService _authService;

        public ObservableCollection<MeuProgressoItem> MeuProgresso { get; set; } = new();

        public MeuProgressoViewModel(DatabaseService databaseService, AuthService authService)
        {
            _databaseService = databaseService;
            _authService = authService;
        }

        public async Task LoadProgressoAsync()
        {
            var utilizadorAtual = _authService.UtilizadorAtual;
            if (utilizadorAtual == null) return; // Ninguém logado

            MeuProgresso.Clear();

            // Pede ao serviço de base de dados a lista de progresso já "mastigada"
            var listaDeProgresso = await _databaseService.GetMeuProgressoAsync(utilizadorAtual.Id);

            foreach (var item in listaDeProgresso)
            {
                MeuProgresso.Add(item);
            }
        }
    }
}