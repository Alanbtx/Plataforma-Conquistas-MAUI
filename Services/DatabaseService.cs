using GameAchievementsApp.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameAchievementsApp.Services
{
    public class DatabaseService
    {
        // Esta variável privada vai guardar a nossa ligação à base de dados.
        // Usamos SQLiteAsyncConnection para que as operações não bloqueiem a interface do utilizador.
        private SQLiteAsyncConnection _database;

        // O 'const' define uma constante. Este será o nome do nosso ficheiro de base de dados.
        private const string DatabaseFilename = "GameAchievementsSQLite.db3";

        // Esta é uma construção para garantir que só temos uma instância da nossa ligação.
        // Chama-se "Singleton Pattern". É uma forma segura de trabalhar com bases de dados.
        private async Task Init()
        {
            // Se _database já foi inicializado, não fazemos nada.
            if (_database is not null)
                return;

            // Combina o caminho do diretório de dados da aplicação com o nome do nosso ficheiro.
            // Cada sistema operativo (Android, iOS, Windows) sabe onde guardar estes dados de forma segura.
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

            // Cria a ligação à base de dados e guarda-a na nossa variável.
            _database = new SQLiteAsyncConnection(dbPath);

            // Pede ao SQLite para criar as tabelas com base nos nossos Models,
            // mas só se elas ainda não existirem.
            await _database.CreateTableAsync<Utilizador>();
            await _database.CreateTableAsync<Jogo>();
            await _database.CreateTableAsync<Conquista>();
            await _database.CreateTableAsync<ProgressoConquista>();
        }

        // --- Métodos para gerir Utilizadores ---

        // Método para login
        public async Task<Utilizador> GetUtilizador(string nomeUtilizador)
        {
            await Init(); // Garante que a BD está inicializada
            return await _database.Table<Utilizador>().Where(u => u.NomeUtilizador == nomeUtilizador).FirstOrDefaultAsync();
        }

        // Método para registo
        public async Task<int> SaveUtilizador(Utilizador utilizador)
        {
            await Init();
            // Procura se já existe um utilizador com o mesmo Id.
            var existingUser = await _database.Table<Utilizador>().Where(u => u.Id == utilizador.Id).FirstOrDefaultAsync();
            if (existingUser is null)
            {
                // Se não existe, insere um novo.
                return await _database.InsertAsync(utilizador);
            }
            else
            {
                // Se já existe, atualiza-o.
                return await _database.UpdateAsync(utilizador);
            }
        }

        // Adiciona este novo método
        public async Task<Utilizador> GetUtilizadorPorNome(string nomeUtilizador)
        {
            await Init(); // Garante que a BD está inicializada
            // Procura na tabela por um utilizador com o nome exato.
            // O .FirstOrDefaultAsync() retorna o utilizador ou 'null' se não encontrar.
            return await _database.Table<Utilizador>().Where(u => u.NomeUtilizador == nomeUtilizador).FirstOrDefaultAsync();
        }

        // --- MÉTODOS PARA JOGOS ---
        public async Task<List<Jogo>> GetJogos()
        {
            await Init();
            return await _database.Table<Jogo>().ToListAsync();
        }

        public async Task<int> SaveJogo(Jogo jogo)
        {
            await Init();
            return await _database.InsertAsync(jogo);
            // NOTA: Para simplificar, não estamos a implementar a lógica de 'Update' aqui ainda.
        }

        public async Task<int> UpdateJogoAsync(Jogo jogo)
        {
            await Init();
            return await _database.UpdateAsync(jogo);
        }

        // Novo método para Excluir um Jogo
        public async Task<int> DeleteJogoAsync(Jogo jogo)
        {
            await Init();
            return await _database.DeleteAsync(jogo);
        }

        // --- ADICIONA ESTES DOIS NOVOS MÉTODOS ---

        // Método para guardar uma nova conquista na base de dados
        public async Task<int> SaveConquistaAsync(Conquista conquista)
        {
            await Init();
            // Simplesmente insere a nova conquista.
            return await _database.InsertAsync(conquista);
        }

        // Método para obter todas as conquistas de um jogo específico
        public async Task<List<Conquista>> GetConquistasPorJogoAsync(int jogoId)
        {
            await Init();
            // Procura na tabela 'conquistas' onde o 'JogoId' corresponde
            // ao Id que recebemos e retorna-os como uma lista.
            return await _database.Table<Conquista>().Where(c => c.JogoId == jogoId).ToListAsync();
        }
        // ... (depois do método GetConquistasPorJogoAsync)

        // Novo método para Atualizar uma Conquista
        public async Task<int> UpdateConquistaAsync(Conquista conquista)
        {
            await Init();
            return await _database.UpdateAsync(conquista);
        }

        // Novo método para Excluir uma Conquista
        public async Task<int> DeleteConquistaAsync(Conquista conquista)
        {
            await Init();
            return await _database.DeleteAsync(conquista);
        }

        // --- MÉTODOS PARA PROGRESSO DO JOGADOR ---

        // Procura um registo de progresso específico
        public async Task<ProgressoConquista> GetProgressoAsync(int utilizadorId, int conquistaId)
        {
            await Init();
            // Procura na tabela um registo que combine o ID do utilizador E o ID da conquista
            return await _database.Table<ProgressoConquista>()
                                  .Where(p => p.UtilizadorId == utilizadorId && p.ConquistaId == conquistaId)
                                  .FirstOrDefaultAsync();
        }

        // Guarda ou Atualiza um registo de progresso
        public async Task<int> SaveProgressoAsync(ProgressoConquista progresso)
        {
            await Init();
            if (progresso.Id != 0)
            {
                // Se o 'Id' já existe, atualiza o registo
                return await _database.UpdateAsync(progresso);
            }
            else
            {
                // Se for 'Id' 0, insere um novo registo
                return await _database.InsertAsync(progresso);
            }
        }

        // --- MÉTODOS AJUDANTES ---

        public async Task<Utilizador> GetUtilizadorAsync(int id)
        {
            await Init();
            return await _database.Table<Utilizador>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Jogo> GetJogoAsync(int id)
        {
            await Init();
            return await _database.Table<Jogo>().Where(j => j.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Conquista> GetConquistaAsync(int id)
        {
            await Init();
            return await _database.Table<Conquista>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        // --- MÉTODO PRINCIPAL PARA O PROGRESSO ---

        public async Task<List<MeuProgressoItem>> GetMeuProgressoAsync(int utilizadorId)
        {
            await Init();

            var listaDeProgressoFinal = new List<MeuProgressoItem>();

            // 1. Pega em todos os registos de progresso para este utilizador
            var progressosDoUtilizador = await _database.Table<ProgressoConquista>()
                                                        .Where(p => p.UtilizadorId == utilizadorId)
                                                        .ToListAsync();

            // 2. Para cada registo, busca os detalhes
            foreach (var progresso in progressosDoUtilizador)
            {

                // 3. Busca a Conquista
                var conquista = await GetConquistaAsync(progresso.ConquistaId);
                if (conquista == null) continue; // Se a conquista foi apagada, pula

                // 4. Busca o Jogo
                var jogo = await GetJogoAsync(conquista.JogoId);
                if (jogo == null) continue; // Se o jogo foi apagado, pula

                // 5. Junta tudo no nosso "molde"
                var itemFinal = new MeuProgressoItem
                {
                    JogoTitulo = jogo.Titulo,
                    ConquistaTitulo = conquista.Titulo,
                    Status = progresso.Status
                };

                listaDeProgressoFinal.Add(itemFinal);
            }

            return listaDeProgressoFinal;
        }
        // --- MÉTODO PARA O RELATÓRIO DO ADMIN ---

        public async Task<List<RelatorioProgressoItem>> GetRelatorioProgressoAsync()
        {
            await Init();

            var relatorioFinal = new List<RelatorioProgressoItem>();

            // 1. Pega em TODOS os registos de progresso de TODOS os jogadores
            var todosOsProgressos = await _database.Table<ProgressoConquista>().ToListAsync();

            // 2. Para cada registo, busca os detalhes
            foreach (var progresso in todosOsProgressos)
            {
                // 3. Busca o Utilizador
                var utilizador = await GetUtilizadorAsync(progresso.UtilizadorId);
                if (utilizador == null) continue; // Pula se o utilizador foi apagado

                // 4. Busca a Conquista
                var conquista = await GetConquistaAsync(progresso.ConquistaId);
                if (conquista == null) continue; // Pula se a conquista foi apagada

                // 5. Busca o Jogo
                var jogo = await GetJogoAsync(conquista.JogoId);
                if (jogo == null) continue; // Pula se o jogo foi apagado

                // 6. Junta tudo no nosso "molde"
                relatorioFinal.Add(new RelatorioProgressoItem
                {
                    NomeUtilizador = utilizador.NomeUtilizador,
                    JogoTitulo = jogo.Titulo,
                    ConquistaTitulo = conquista.Titulo,
                    Status = progresso.Status
                });
            }

            return relatorioFinal;
        }
    }

}