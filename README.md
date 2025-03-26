# Indies - Cadastro de Músicas Indies

## Descrição
O **Indies** é uma aplicação web desenvolvida para catalogar e gerenciar músicas independentes. Os usuários podem cadastrar informações sobre músicas, artistas e álbuns, além de explorar um banco de dados colaborativo de canções indie.

![Texto alternativo](./Indies/wwwroot/img/indies.png)
Link para o post:[link.in](https://www.linkedin.com/feed/update/urn:li:activity:7307817725337198593/)

## Funcionalidades
- Cadastro de músicas com nome, artista, álbum, gênero e ano de lançamento
- Pesquisa e listagem de músicas cadastradas
- Edição e remoção de músicas do banco de dados
- Sistema de autenticação para gerenciar cadastros
- Interface responsiva e intuitiva

## Tecnologias Utilizadas
- **Frontend:** Razor Pages / Blazor
- **Backend:** ASP.NET Core
- **Banco de Dados:** SQL Server / Entity Framework Core
- **Autenticação:** Identity Framework

## Instalação e Execução
### Requisitos
- .NET SDK instalado
- SQL Server configurado

### Passos
1. Clone o repositório:
   ```sh
   git clone https://github.com/seu-usuario/indietrack.git
   cd indietrack
   ```
2. Configure a string de conexão no arquivo `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=SEU_SERVIDOR;Database=IndieTrackDB;Trusted_Connection=True;"
     }
   }
   ```
3. Execute as migrações do banco de dados:
   ```sh
   dotnet ef database update
   ```
4. Inicie o servidor:
   ```sh
   dotnet run
   ```
5. Acesse a aplicação no navegador: `http://localhost:5000`

## Contribuição
Sinta-se à vontade para contribuir com o projeto!
1. Faça um fork do repositório.
2. Crie uma branch com sua feature: `git checkout -b minha-feature`.
3. Commit suas alterações: `git commit -m 'Adicionando nova funcionalidade'`.
4. Envie para o repositório remoto: `git push origin minha-feature`.
5. Abra um Pull Request.

