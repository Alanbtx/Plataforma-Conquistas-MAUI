using GameAchievementsApp.Models;

namespace GameAchievementsApp.Models
{
    // Esta classe é o nosso "molde" para o relatório.
    // Ela junta dados de 4 tabelas diferentes.
    public class RelatorioProgressoItem
    {
        public string NomeUtilizador { get; set; } = string.Empty;
        public string JogoTitulo { get; set; } = string.Empty;
        public string ConquistaTitulo { get; set; } = string.Empty;
        public StatusConquista Status { get; set; }
    }
}