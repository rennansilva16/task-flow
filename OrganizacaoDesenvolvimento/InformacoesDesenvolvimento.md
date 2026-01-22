## Tarefas Maiores que devem ser concluídas para que o projeto fique pronto
1. Criar Projeto Models para definir as classes.
2. Criar projeto API ASP NET CORE e conectar com o banco
3. Criar Projeto Contract para servir o cliente e o servidor.
4. Criar a interface utilizando Blazor WebAssembly  e MudBlazor.

---

### Criar Projeto Models para definir as classes
- Classes:
   - Tarefa: (feito)
      - TarefaId
      - Titulo
      - Descrição
      - Prazo
      - Importância
      - Prioridade
      - Status
      - TipoDeTarefa


   - Importância (feito)
      - enum: Baixa, média, alta

            
   - Status (feito)
      - enum: Recorrentes, Pendentes, Fazendo, Finalizadas, Excluídas 

      
   - TipoDeTarefa
      - TipoDeTarefaId
      - Nome
      - Descricao

   - Usuario:
     - ID
     - Nome
     - Login
     - Senha
     - List<Tarefa>

### Criar Projeto API ASP NET Core para lidar com o banco, repositories e services, e servir os endpoints para a aplicação Blazor

- Repository
    - TarefaRepository
        - AdicionarTarefa
        - EditarTarefa
        - FinalizarTarefa
        - IniciarTarefa
        - EncerrarTarefa
        - ExcluirTarefa
        - VisualizarTarefa
    - UsuarioRepository
        - AdicionarUsuario
        - EditarUsuario
        - ExcluirUsuario
        - VisualizarUsuario
    - TipoDeTarefaRepository
        - AdicionarTipoDeTarefa
        - EditarTipoDeTarefa
        - ExcluirTipoDeTarefa
        - VisualizarTipoDeTarefa

