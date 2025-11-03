using GameAchievementsApp.Models; // Precisamos disto para o StatusConquista

namespace GameAchievementsApp.Models
{
    // Esta classe é um "ViewModel" de item.
    // Ela junta os dados de 3 tabelas diferentes
    // para serem mostrados de forma fácil na nossa página.
    public class MeuProgressoItem
    {
        public string JogoTitulo { get; set; } = string.Empty;
        public string ConquistaTitulo { get; set; } = string.Empty;
        public StatusConquista Status { get; set; }
    }
}