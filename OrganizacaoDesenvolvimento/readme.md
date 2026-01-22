# Objetivo
Criar aplicação para gerenciamento de Tarefas com base no Kanban.

## Funcionalidades
- Criar, editar e excluir tarefas
- Organizar tarefas em colunas (Pendentes, Em andamento, Concluídas)
- Mover tarefas entre colunas via drag-and-drop
- Persistência dos dados no SQL Server
- Interface interativa com Blazor

## Tecnologias Utilizadas
- Blazor WebAssembly (frontend)
- ASP.NET Core Web API (backend)
- Entity Framework Core (acesso ao banco de dados)
- SQL Server (persistência)
- C#

## Como Executar o Projeto
1. Clone este repositório
2. Configure o SQL Server local e ajuste a connection string no arquivo `appsettings.json`
3. Execute o projeto da API (`dotnet run` dentro da pasta `Server`)
4. Execute o projeto do cliente Blazor (`dotnet run` dentro da pasta `Client`)
5. Acesse `https://localhost:5001` no navegador

## Estrutura do Projeto
- `Client/` → Aplicação Blazor WebAssembly
- `Server/` → API ASP.NET Core
- `Shared/` → Modelos e classes compartilhadas
- `Contracts` → DTOs
## Próximos Passos
- Adicionar autenticação de usuários
- Implementar filtros e pesquisa de tarefas
- Criar relatórios de produtividade

---

## 📝 Visão do Projeto – MVP Kanban

### Funcionalidades

1. **Autenticação de usuários**

   * Cadastro e login de usuários.
   * Cada usuário vê apenas suas próprias tarefas.

2. **Tarefas**

   * Criar, editar e excluir tarefas.
   * Cada tarefa terá:

     * **Título** (obrigatório)
     * **Tipo de tarefa** (escolhido de uma lista ou criado pelo usuário)
     * **Descrição** (opcional)
     * **Prazo** (data/hora limite)
     * **Importância** (baixo, médio, alto)
     * **Status** (recorrentes, pendente, fazendo, concluída)
     * **Prioridade**

3. **Kanban**

   * Tarefas exibidas em **colunas** (Recorrentes, Pendentes, Fazendo, Concluídas).
   * Usuário pode arrastar tarefas entre colunas para mudar o status.
   * Dentro da mesma coluna, pode arrastar para cima ou para baixo para ordenar por prioridade.

4. **Tipos de Tarefas**

   * Tipos básicos iniciais: saúde, trabalho, hobbies, casa, geral.
   * Usuário pode criar seus próprios tipos.
   * Filtro para visualizar tarefas por tipo, ou todas juntas.

---

## 🧭 Linha do Tempo / Casos de Uso do Usuário

### 1. Login e Acesso

* Usuário entra no site.
* Se já tem cadastro, insere **login e senha**.
* Ao logar, é redirecionado para a **Página Inicial (Kanban)**.

### 2. Cadastro de Usuário

* Na tela inicial de login, o usuário clica em **“Ainda não é usuário? Faça seu cadastro.”**.
* Abre uma **modal de cadastro** com os campos:

  * Nome completo
  * Login (não pode ser repetido, precisa ser único)
  * Senha
* Ao clicar em **Criar**, a modal se fecha e aparece uma mensagem acima da caixa de login:

  * *“Usuário criado com sucesso. Agora você pode entrar com seu login e senha.”*

### 3. Página Inicial – Visualização de Tarefas

* A página inicial mostra **todas as tarefas do usuário** distribuídas nas colunas:

  * **Pendentes**
  * **Fazendo**
  * **Concluídas**
  * **Excluídas**
* O usuário pode **filtrar** por tipo de tarefa (ex: “Hobbies”) e ver apenas aquelas relacionadas.

### 4. Criar Nova Tarefa

* Na Página Inicial há um botão **“Adicionar Tarefa”**.
* Ao clicar, abre uma **modal de criação de tarefa** com os campos:

  * Título (obrigatório)
  * Descrição (opcional)
  * Importância (Baixa, Média, Alta)
  * Prazo (data/hora limite)
  * Tipo de tarefa (seleciona ou cria um novo)
  * Checkbox **“Recorrente”** (define se é tarefa única ou recorrente)
* Usuário clica em **Adicionar**.
* A tarefa é criada e aparece uma mensagem:

  * *“Tarefa adicionada com sucesso. Total de X tarefas criadas.”*
* A modal **não se fecha automaticamente**. Ela fecha somente se o usuário:

  * clicar fora dela,
  * clicar no **X** no canto superior direito,
  * ou clicar no botão **Voltar**.

### 5. Interação com Tarefa Existente

* Cada tarefa é exibida como um **card/caixa** no Kanban.
* No card há **três pontos (menu de opções)** com as ações:

  * **Visualizar** → abre modal em modo leitura, mostrando os detalhes.
  * **Editar** → abre modal semelhante à de criação, mas com os campos preenchidos para alterar.
  * **Mudar status** → permite trocar o status (ou arrastar a tarefa entre colunas).
  * **Apagar** → exibe mensagem de confirmação:

    * *“Deseja mover esta tarefa para a Coluna de Excluídas?”*
    * Se confirmar, a tarefa é movida para a coluna **Excluídas**.

### 6. Coluna de Excluídas

* Na coluna **Excluídas**, o usuário pode:

  * Restaurar a tarefa para uma outra coluna.
  * Excluir permanentemente a tarefa.
---