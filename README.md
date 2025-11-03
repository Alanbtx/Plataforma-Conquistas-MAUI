# Plataforma Colaborativa para Conquistas de Games

Este é um projeto de aplicação multiplataforma construído com **.NET MAUI**, focado em criar uma comunidade para jogadores rastrearem e partilharem o seu progresso em conquistas de videojogos.

A aplicação foi desenvolvida seguindo o padrão de arquitetura **MVVM (Model-View-ViewModel)** e utiliza uma base de dados **SQLite** local para persistência de dados.

## 🚀 Funcionalidades Principais

A aplicação possui dois tipos de utilizadores com permissões distintas: Administrador e Jogador.

### 👤 Administrador (Admin)
O Administrador é responsável por gerir todo o conteúdo da plataforma:

* **Gestão de Jogos (CRUD):** Criar, Ler, **Editar** e **Excluir** jogos da plataforma.
* **Gestão de Conquistas (CRUD):** Para cada jogo, o Admin pode adicionar, **editar** e **excluir** as conquistas (achievements) associadas.
* **Relatório Geral:** Visualizar uma tela de relatório que mostra o progresso de **todos os jogadores** em todas as conquistas.

### 🎮 Jogador (Player)
O Jogador é o consumidor final da plataforma:

* **Navegação:** Visualizar a lista de todos os jogos e as suas respetivas conquistas.
* **Rastreamento de Progresso:** Selecionar conquistas e marcá-las como **"Em Andamento"** ou **"Concluída"**.
* **Painel Pessoal:** Aceder a uma página de **"O Meu Progresso"** que resume todas as conquistas que estão a ser rastreadas.

## 🛠️ Tecnologias Utilizadas

* **.NET MAUI:** Framework para criação de aplicações nativas multiplataforma (Android, iOS, Windows) a partir de uma única base de código C#.
* **SQLite-net-pcl:** Biblioteca para gestão da base de dados local SQLite.
* **MVVM (Model-View-ViewModel):** Padrão de arquitetura principal para separar a lógica de negócio da interface do utilizador.
* **CommunityToolkit.Mvvm:** Biblioteca (NuGet) para simplificar a implementação do MVVM com `ObservableObject` e `RelayCommand`.
* **Injeção de Dependências (DI):** Usada para gerir serviços como `DatabaseService` e `AuthService`.
