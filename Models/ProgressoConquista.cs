using SQLite;

namespace GameAchievementsApp.Models
{
    [Table("progresso_conquistas")]
    public class ProgressoConquista
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // A chave que nos diz a que utilizador este progresso pertence.
        [Indexed]
        public int UtilizadorId { get; set; }

        // A chave que nos diz a que conquista este progresso se refere.
        [Indexed]
        public int ConquistaId { get; set; }

        // O estado atual do progresso.
        public StatusConquista Status { get; set; }
    }

    public enum StatusConquista
    {
        NaoIniciado,
        EmAndamento,
        Concluido
    }
}