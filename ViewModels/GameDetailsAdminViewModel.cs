using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameAchievementsApp.Models;
using GameAchievementsApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GameAchievementsApp.ViewModels
{
    // [QueryProperty] é a magia que permite ao .NET MAUI passar dados
    // para este ViewModel durante a navegação.
    // Estamos a dizer: "Quando alguém navegar para cá e enviar um parâmetro
    // chamado 'jogoId', por favor, guarda o valor desse parâmetro na nossa
    // propriedade 'JogoId' ".
    [QueryProperty(nameof(JogoId), "jogoId")]
    public partial class GameDetailsAdminViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        // Propriedade que vai receber o ID do jogo vindo da navegação
        [ObservableProperty]
        int _jogoId;

        // Propriedades para os campos de texto da nova conquista
        [ObservableProperty]
        private string _novoTituloConquista;

        [ObservableProperty]
        private string _novaDescricaoConquista;

        // A nossa lista de conquistas que será exibida na tela
        public ObservableCollection<Conquista> Conquistas { get; set; } = new();

        public GameDetailsAdminViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // Este método será chamado pela View (no OnAppearing)
        // para carregar a lista de conquistas
        public async Task LoadConquistasAsync()
        {
            // Limpa a lista antes de carregar, para evitar duplicados
            Conquistas.Clear();

            // Pede à base de dados todas as conquistas deste JogoId
            var conquistasDaDb = await _databaseService.GetConquistasPorJogoAsync(JogoId);

            foreach (var conquista in conquistasDaDb)
            {
                Conquistas.Add(conquista);
            }
        }

        [RelayCommand]
        private async Task AddConquista()
        {
            if (string.IsNullOrWhiteSpace(NovoTituloConquista) || string.IsNullOrWhiteSpace(NovaDescricaoConquista))
            {
                await Shell.Current.DisplayAlert("Erro", "Preenche o título e a descrição da conquista.", "OK");
                return;
            }

            var novaConquista = new Conquista
            {
                JogoId = this.JogoId, // Ligamos a conquista ao JogoId que recebemos!
                Titulo = NovoTituloConquista,
                Descricao = NovaDescricaoConquista
            };

            await _databaseService.SaveConquistaAsync(novaConquista);

            // Adiciona à lista visível (a ObservableCollection atualiza a UI)
            Conquistas.Add(novaConquista);

            // Limpa os campos
            NovoTituloConquista = string.Empty;
            NovaDescricaoConquista = string.Empty;
        }

        // ... (depois do método AddConquista)

        // Comando para EDITAR uma Conquista
        [RelayCommand]
        private async Task EditConquista(Conquista conquista)
        {
            if (conquista == null) return;

            string novoTitulo = await Shell.Current.DisplayPromptAsync(
                "Editar Conquista",
                "Novo título da conquista:",
                "OK",
                "Cancelar",
                initialValue: conquista.Titulo);

            if (string.IsNullOrWhiteSpace(novoTitulo)) return;

            string novaDescricao = await Shell.Current.DisplayPromptAsync(
                "Editar Conquista",
                $"Nova descrição para {novoTitulo}:",
                "OK",
                "Cancelar",
                initialValue: conquista.Descricao);

            if (string.IsNullOrWhiteSpace(novaDescricao)) return;

            // 1. Atualiza o objeto na memória
            conquista.Titulo = novoTitulo;
            conquista.Descricao = novaDescricao;

            // 2. Atualiza o objeto na base de dados
            await _databaseService.UpdateConquistaAsync(conquista);

            // A UI vai atualizar-se sozinha graças ao ObservableObject!
        }

        // Comando para EXCLUIR uma Conquista
        [RelayCommand]
        private async Task DeleteConquista(Conquista conquista)
        {
            if (conquista == null) return;

            bool resposta = await Shell.Current.DisplayAlert(
                "Confirmar Exclusão",
                $"Tens a certeza que queres excluir a conquista {conquista.Titulo}?",
                "Sim, Excluir",
                "Cancelar");

            if (resposta)
            {
                await _databaseService.DeleteConquistaAsync(conquista);
                Conquistas.Remove(conquista); // Remove da lista visível
            }
        }
    }
}