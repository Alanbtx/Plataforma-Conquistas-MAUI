using CommunityToolkit.Mvvm.ComponentModel;
using GameAchievementsApp.Models;
using GameAchievementsApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GameAchievementsApp.ViewModels
{
    public partial class AdminProgressoViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<RelatorioProgressoItem> RelatorioProgresso { get; set; } = new();

        public AdminProgressoViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task LoadRelatorioAsync()
        {
            RelatorioProgresso.Clear();

            // Pede ao serviço de base de dados o relatório completo
            var relatorio = await _databaseService.GetRelatorioProgressoAsync();

            foreach (var item in relatorio)
            {
                RelatorioProgresso.Add(item);
            }
        }
    }
}