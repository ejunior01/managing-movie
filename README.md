<h1 align="center">
   ASP.NET Core 9 Web API com Entity Framework Core
<h1/>

<p align="center">
<img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/ejunior01/managing-movie">
<img  alt="licença" src="https://img.shields.io/github/license/ejunior01/managing-movie" />
<img alt="Made by Junior Santos" src="https://img.shields.io/badge/made%20by-Junior Santos-%237519C1">
<p/>

### Sobre o projeto

Este projeto demonstra a construção de uma API Web utilizando ASP.NET Core 9 e Entity Framework Core, seguindo práticas recomendadas de Design Orientado a Domínio (DDD) e Clean Code. A aplicação realiza operações CRUD (Criar, Ler, Atualizar, Deletar) para gerenciar detalhes de filmes. Utilizando os conceitos abordados no site: [codewithmukesh.com](https://codewithmukesh.com/blog/aspnet-core-webapi-crud-with-entity-framework-core-full-course/#what-we-will-build).

### Features
   - Filme
      - Criar um novo filme: Adiciona um novo filme fornecendo detalhes como título, diretor, data de lançamento e gênero.
      - Obter um filme por ID: Recupera os detalhes de um filme específico usando seu identificador único.
      - Listar todos os filmes: Lista todos os filmes cadastrados.
      - Atualizar um filme: Atualiza as informações de um filme existente especificando seu ID e os novos detalhes.
      - Deletar um filme: Remove um filme do banco de dados usando seu ID.

### Novas funcionalidades e melhorias
- [x] Logging
- [x] Validação de entrada de dados
- [ ] Paginação, classificação e filtragem
- [x] CQRS

### Tecnologias e Bibliotecas utilizadas
- ASP.NET Core 9: Framework moderno para desenvolvimento de aplicações web.
- Entity Framework Core: ORM para interações eficientes com o banco de dados.
- PostgreSQL: Banco de dados relacional utilizado na aplicação.
- Docker: Utilizado para containerizar o banco de dados PostgreSQL.
- OpenAPI com Scalar: Geração e exploração de documentação da API.
- Minimal APIs - Implementação enxuta e performática para APIs.
- Serilog - Biblioteca de logging para armazenamento em arquivos, console e outros destinos.
- MediatR - Implementação do padrão Mediator para melhor organização de responsabilidades.
- FluentValidation - Biblioteca .NET para validação de modelos, permitindo regras de validação expressivas e reutilizáveis.






Licença
Este projeto está licenciado sob a Licença MIT. Veja o arquivo LICENSE para mais detalhes.


