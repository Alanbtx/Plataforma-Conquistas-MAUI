// Este 'using' permite-nos usar os atributos como [Table], [PrimaryKey], etc.
using SQLite;

namespace GameAchievementsApp.Models
{
    // [Table("...")] define o nome da tabela que será criada na base de dados.
    [Table("utilizadores")]
    public class Utilizador
    {
        // [PrimaryKey] diz que esta propriedade é a chave única da tabela.
        // [AutoIncrement] faz com que a base de dados gere um número novo para cada registo.
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // [Unique] garante que não podem existir dois utilizadores com o mesmo nome.
        [Unique]
        public string NomeUtilizador { get; set; }

        public string Password { get; set; }

        // Vamos guardar o tipo de utilizador para saber se é um Admin ou um Jogador.
        public TipoUtilizador Tipo { get; set; }
    }

    // Um 'enum' é um tipo especial que só pode ter um dos valores definidos.
    // É perfeito para o nosso caso!
    public enum TipoUtilizador
    {
        Jogador,
        Admin
    }
}