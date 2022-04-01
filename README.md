# Cinemas API

## Sobre o Repo
API de cinemas e filmes criada no curso da Alura de .NETs, na formação ASP.NET Core REST APIs, dentro do meu processo de aprendizado e prática dessa plataforma. Da forma que foi construída, a API pode ser utilizada sem necessidade de forte acoplamento com estruturas MVC, que podem acabar com responsabilidades demais quando não utilizam de API's. O arquivo _.gitignore_ foi gerado automaticament utilizando do comando "dotnet new gitignore".
    
## Documentação
Os endpoints podem ser facilmente visualizados e consultados utilizando a URL que os exibe tratados no formato do Swagger.

# Configuração
Dentro da pasta da solução, contém um arquivo _appsettings.example.json_, que pode ser utilizado para definir as string de conexão, sendo a principal para MySQL. Ao remover o sufixo ".example", o arquivo não será considerado pelo git, pois ficará com suas configurações pessoais.

# Estrutura do Banco de Dados
O banco de dados é relativamente simples, contendo apenas algumas entidades e a tabela de migrações criada pelo framework (que, intencionalmente, não aparecerá no diagrama).
![MER do Banco de Dados](https://user-images.githubusercontent.com/67481026/161175997-2ddaaa1c-74be-45bd-9ca0-ebc72d239ae2.png)

Modelo construído utilizando as ferramentas nativas do MySQL Workbench

# Futuras Implementações
- [ ] Alterar o banco de dados para que gêneros de filme seja uma entidade à parte com um relacionamento n:n com filmes;
- [ ] Alterar ids das chaves primárias de inteiros com autoincrement para uuid ou identificações mais aleatórias e únicas;
- [ ] Hospedar a API;

# Atualizações
**Versão 1.0:**
A versão 1.0 contém apenas o que foi proposto nas aulas, sem alterações que afetem a implementação. Todas as operações estão disponíveis sem necessidade de autenticação.
