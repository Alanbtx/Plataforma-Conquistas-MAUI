using CommunityToolkit.Mvvm.ComponentModel;
using GameAchievementsApp.Models;
using GameAchievementsApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GameAchievementsApp.ViewModels
{
    [QueryProperty(nameof(JogoId), "jogoId")]
    public partial class PlayerGameDetailsViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly AuthService _authService; // 1. ADICIONA O AUTHSERVICE

        [ObservableProperty]
        int _jogoId;

        public ObservableCollection<Conquista> Conquistas { get; set; } = new();

        [ObservableProperty]
        Conquista _selectedConquista;

        // 2. ATUALIZA O CONSTRUTOR
        public PlayerGameDetailsViewModel(DatabaseService databaseService, AuthService authService)
        {
            _databaseService = databaseService;
            _authService = authService; // 3. GUARDA-O
        }

        public async Task LoadConquistasAsync()
        {
            Conquistas.Clear();
            var conquistasDaDb = await _databaseService.GetConquistasPorJogoAsync(JogoId);
            foreach (var conquista in conquistasDaDb)
            {
                Conquistas.Add(conquista);
            }
        }

        // 4. SUBSTITUI O MÉTODO OnSelectedConquistaChanged POR ESTE
        async partial void OnSelectedConquistaChanged(Conquista value)
        {
            if (value == null)
                return;

            // Pega na conquista selecionada
            var conquistaSelecionada = value;
            // Pega no utilizador logado através do nosso novo serviço!
            var utilizadorAtual = _authService.UtilizadorAtual;

            // Verificação de segurança
            if (utilizadorAtual == null)
            {
                await Shell.Current.DisplayAlert("Erro", "Não foi possível identificar o utilizador.", "OK");
                return;
            }

            // Mostra ao jogador as opções para marcar o progresso
            string acao = await Shell.Current.DisplayActionSheet(
                $"Marcar progresso para: {conquistaSelecionada.Titulo}",
                "Cancelar",
                null,
                "Em Andamento", "Concluída");

            StatusConquista novoStatus;

            // Define o status com base na escolha do jogador
            switch (acao)
            {
                case "Em Andamento":
                    novoStatus = StatusConquista.EmAndamento;
                    break;
                case "Concluída":
                    novoStatus = StatusConquista.Concluido;
                    break;
                default:
                    // Se clicou em "Cancelar" ou fora da caixa, não faz nada
                    SelectedConquista = null;
                    return;
            }

            // Procura na base de dados se já existe um registo de progresso
            var progressoExistente = await _databaseService.GetProgressoAsync(utilizadorAtual.Id, conquistaSelecionada.Id);

            if (progressoExistente == null)
            {
                // Se não existe, cria um novo registo
                progressoExistente = new ProgressoConquista
                {
                    UtilizadorId = utilizadorAtual.Id,
                    ConquistaId = conquistaSelecionada.Id,
                    Status = novoStatus
                };
            }
            else
            {
                // Se já existe, apenas atualiza o status
                progressoExistente.Status = novoStatus;
            }

            // Guarda o registo (novo ou atualizado) na base de dados
            await _databaseService.SaveProgressoAsync(progressoExistente);

            await Shell.Current.DisplayAlert("Sucesso", "Progresso guardado!", "OK");

            SelectedConquista = null;
        }
    }
}