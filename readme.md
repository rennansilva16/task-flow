# 🗂️ TaskFlow – Gerenciador de Tarefas

**TaskFlow** é uma aplicação de **gerenciamento de tarefas em formato Kanban**, desenvolvida com foco em **organização, clareza visual e evolução contínua da aplicação**.

O projeto foi estruturado desde o início com uma **arquitetura moderna**, separando front-end, back-end e camada compartilhada, permitindo que novas funcionalidades sejam adicionadas de forma incremental, sem comprometer a base existente.

---

## 🎯 Objetivo do Projeto

O objetivo central do projeto é fornecer uma solução simples e eficiente para **organização de tarefas**, ao mesmo tempo em que serve como um ambiente prático para aplicação de **boas práticas de desenvolvimento**.

A ideia é utilizar um único projeto para:

* Organizar tarefas de forma visual (Kanban)
* Aplicar conceitos de arquitetura em aplicações web modernas
* Evoluir funcionalidades de maneira incremental
* Manter um código limpo, organizado e escalável

Cada nova funcionalidade adicionada representa uma evolução técnica do projeto.

---

## 🧠 Conceitos Trabalhados (até o momento)

Atualmente, o projeto trabalha principalmente com:

* Arquitetura em camadas (Client, Server e Shared)
* Separação de responsabilidades entre API e interface
* Organização de regras de negócio
* Persistência de dados com ORM
* Consumo de API REST no front-end
* Estruturação de aplicações com foco em manutenção e evolução

Esses conceitos serão ampliados continuamente.

---

## ⚙️ Estado Atual do Projeto

* Aplicação com quadro Kanban funcional
* Gerenciamento de tarefas com estados:

  * Pendentes
  * Fazendo
  * Finalizado
* Criação, edição e exclusão de tarefas
* Organização das tarefas por categorias
* Interface com suporte a modo noturno
* Estrutura preparada para futuras evoluções

---

## 🏗️ Arquitetura da Aplicação

A aplicação está organizada em três projetos principais:

* **Client**: Blazor WebAssembly
* **Server**: ASP.NET Core Web API
* **Shared**: Biblioteca compartilhada contendo models e contratos

Essa abordagem garante maior clareza no código, facilidade de manutenção e escalabilidade futura.

---

## 🛠️ Tecnologias Utilizadas

* **ASP.NET Core Web API**
* **Blazor WebAssembly**
* **Entity Framework Core**
* **SQL Server**
* **C#**
* **RESTful APIs**
* **Git**

---

## 🚀 Como Executar o Projeto

### Pré-requisitos

* .NET SDK instalado
* SQL Server (local ou em container)
* Ambiente configurado para execução de aplicações ASP.NET Core e Blazor

### Execução

1. Clone o repositório:

   ```bash
   git clone https://github.com/rennansilva16/task-flow.git
   ```

2. Abra a solução no Visual Studio ou editor de sua preferência.

3. Configure a string de conexão do banco de dados, se necessário.

4. Execute o projeto **Server** (API).

5. Execute o projeto **Client** (Blazor WebAssembly).

---

## 🔮 Evolução Planejada

O projeto será evoluído gradualmente, podendo incluir:

* Autenticação e autorização mais completas
* Suporte a múltiplos usuários
* Novos módulos além do gerenciamento de tarefas
* Melhorias de usabilidade e performance
* Testes automatizados
* Aprimoramento da arquitetura conforme o crescimento do sistema

O escopo permanece aberto para evolução contínua.

---

## 📌 Status do Projeto

🚧 **Em desenvolvimento contínuo**
Projeto em evolução, com foco em organização, aprendizado prático e qualidade técnica. Teste

---

## 👤 Autor

Desenvolvido por **Rennan Silva**
GitHub: [https://github.com/rennansilva16](https://github.com/rennansilva16)