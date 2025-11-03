// Adiciona estes dois "usings"
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace GameAchievementsApp.Models
{
    [Table("conquistas")]
    // Faz a Conquista herdar de ObservableObject
    public partial class Conquista : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int JogoId { get; set; }

        // Atributo "ObservableProperty" para o Título
        [ObservableProperty]
        private string _titulo = string.Empty;

        // Atributo "ObservableProperty" para a Descrição
        [ObservableProperty]
        private string _descricao = string.Empty;
    }
}