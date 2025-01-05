
# AWS Cognito Study - Projeto de Autenticação

Este repositório é um estudo prático de autenticação e autorização utilizando AWS Cognito, com integração a uma aplicação desenvolvida em .NET 8 e Angular 18.

## Visão Geral

O objetivo deste projeto é implementar autenticação de usuários com o AWS Cognito, protegendo APIs e gerenciando fluxos de login, logout e refresh token.

A aplicação está dividida em duas partes principais:

-   **Backend**: API REST em .NET 8, responsável pelo gerenciamento das requisições.
-   **Frontend**: Aplicação Angular 18 para a interface do usuário.

## Estrutura do Repositório

```
/aws-cognito-study
│
├── backend/
│   ├── ToDoApp.API/				# Expõe a API rest
│   ├── ToDoApp.Application/		# Regras de negócio
│   └── ToDoApp.Core/				# Base compartilhada
│   └── ToDoApp.Domain/				# Domínio
│   └── ToDoApp.Infra.Data/			# Persistência de dados
│   └── ToDoApp.Infra.External/		# Integrações externas
│
├── frontend/
│   ├── src/
│   │   ├── app/          
│   │   │   ├── core/      # Módulo central, serviços e funcionalidades globais
│   │   │   ├── features/  # Funcionalidades da aplicação
│
└── README.md

```

## Funcionalidades

-   **Autenticação de usuários com AWS Cognito** (login, registro e recuperação de senha).
-   **Proteção de rotas no frontend** com base no status de autenticação.
-   **Autorização por grupo de usuários** usando tokens JWT.
-   **Integração com o backend para validação de tokens**.

## Tecnologias Utilizadas

-   **.NET 8** - Backend API
-   **Angular 18** - Frontend Single Page Application
-   **AWS Cognito** - Serviço de autenticação

## Configuração e Execução

1.  Clone o repositório:
    
    ```bash
    git clone https://github.com/chandelima/aws-cognito-study.git
    
    ```
    
2.  Acesse a pasta do projeto:
    
    ```bash
    cd aws-cognito-study
    
    ```
    
3.  Configuração do backend:
    
    ```bash
    cd backend
    dotnet restore
    dotnet run
    
    ```
    
4.  Configuração do frontend:
    
    ```bash
    cd frontend
    npm install
    ng serve
    
    ```
    
5.  Acesse a aplicação:
    
    ```
    https://localhost:4200
    
    ```
    

## Configurações

### Backend
No backend você deve alterar as variáveis do appsettings.json para as equivalentes ao seu pool de usuário gerado no AWS Cognito.

### Frontend
No frontend, apenas é necessário alterar o arquivo enviroment.ts, apontando a URL base onde o backend está exposto.

## Contribuição

Contribuições são bem-vindas! Se encontrar algum problema ou tiver ideias para melhorias, sinta-se à vontade para abrir uma issue ou enviar um pull request.

## Licença

Este projeto é distribuído sob a licença MIT.
