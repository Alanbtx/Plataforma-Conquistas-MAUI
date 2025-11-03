// Adiciona estes dois "usings"
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace GameAchievementsApp.Models
{
    [Table("jogos")]
    // Faz o Jogo herdar de ObservableObject
    public partial class Jogo : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Esta linha está correta como estava

        // --- A CORREÇÃO ESTÁ AQUI ---
        [ObservableProperty]
        [property: Unique] // Mudamos de [Unique] para [property: Unique]
        private string _titulo = string.Empty; // Adicionamos a inicialização

        [ObservableProperty]
        private string _plataforma = string.Empty; // Adicionamos a inicialização
    }
}