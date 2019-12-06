# Saldo Bancário

## Project setup

### Configurar a conexão com o banco no arquivo appsettings.json conforme exemplo abaixo:
```
"ConnectionStrings": {
    "DefaultConnection": "Data Source=SaldoBancario.db" 
```
Onde DefaultConnection é o caminho do banco de dados. Por padrão o sistema já gera o banco na pasta raiz da aplicação

### Carregar a estrutura do Banco de Dados
Você pode utilizar o arquivo initDB.sql que está na raiz do repositório ou gerar a estrutura utilizando o Entity Framework conforme abaixo:

```
No Visual Studio: 
    Update-Database

No VS Code (Terminal):
    dotnet ef database update
```
