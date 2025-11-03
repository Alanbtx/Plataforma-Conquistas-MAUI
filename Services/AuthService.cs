using GameAchievementsApp.Models;

namespace GameAchievementsApp.Services
{
    public class AuthService
    {
        // Esta propriedade pública vai guardar os dados do utilizador logado.
        // O "private set" significa que só esta classe pode alterá-lo.
        // O "?" a seguir a "Utilizador" significa que ele pode ser nulo (ninguém logado).
        public Utilizador? UtilizadorAtual { get; private set; }

        // Um método que o LoginViewModel vai chamar quando o login for bem-sucedido
        public void EfetuarLogin(Utilizador utilizador)
        {
            UtilizadorAtual = utilizador;
        }

        // Um método para "esquecer" o utilizador (quando fizermos logout no futuro)
        public void EfetuarLogout()
        {
            UtilizadorAtual = null;
        }
    }
}