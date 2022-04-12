# Cinemas API

## Sobre o Repo
API de cinemas e filmes criada no curso da Alura de .NETs, na formação ASP.NET Core REST APIs, dentro do meu processo de aprendizado e prática dessa plataforma. Da forma que foi construída, a API pode ser utilizada sem necessidade de forte acoplamento com estruturas MVC, que podem acabar com responsabilidades demais quando não utilizam de API's. O arquivo _.gitignore_ foi gerado automaticament utilizando do comando "dotnet new gitignore".
    
## Documentação
Os endpoints podem ser facilmente visualizados e consultados utilizando a URL que os exibe tratados no formato do Swagger.

# Configuração
## Banco de Dados
Dentro da pasta da solução FilmesAPI, contém um arquivo *appsettings.example.json*, que pode ser utilizado para definir as string de conexão, sendo a principal para MySQL. Ao remover o sufixo ".example", o arquivo não será considerado pelo git, pois ficará com suas configurações pessoais.

As credenciais para uso de banco de dados com a solução UsuariosAPI, podem ser as mesmas utilizados na FilmesAPI, mas com a base sendo *db_usuarios* ao invés de *db_filmes* por padrão.

Recomenda-se manter os bancos de dados separados e com a nomeação que vêm de configuração, mas fica a critério do utilizador da aplicação, não sendo um **critério** para funcionamento.

## E-mail
Para que o *client* de e-mail funcione corretamente na aplicação, também deve-se configurar suas credenciais para endereço e senha no nó "EmailSettings" do *appsettings.json*.

A fim de oferecer maior suporte e ofecer facilidade para os utilizadores da aplicação, escolheu-se o "Gmail" como *client* principal, mas isso também pode ser facilmente alterado.

Em casos em que a conta tem a opção de Autenticação Por Dois Fatores habilitada, a senha pessoal não funcionará como forma de autenticação, sendo necessário gerar uma senha específica para o dispositivo. Maiores informações sobre esse processo pode ser encontrada na [documentação do serviço](https://support.google.com/accounts/answer/185833?visit_id=637851082192925311-3785191376&rd=1#ts=3202254,3202256).

# Estrutura do Banco de Dados
O banco de dados é relativamente simples, contendo apenas algumas entidades e a tabela de migrações criada pelo framework (que, intencionalmente, não aparecerá no diagrama).

![MER do Banco de Dados](https://user-images.githubusercontent.com/67481026/161175997-2ddaaa1c-74be-45bd-9ca0-ebc72d239ae2.png)

Modelo construído utilizando as ferramentas nativas do MySQL Workbench

# Futuras Implementações
- [ ] Documentar as classes e métodos;
- [ ] Modificar classes para internas onde for necessário;
- [ ] Alterar o banco de dados para que gêneros de filme seja uma entidade à parte com um relacionamento n:n com filmes;
- [ ] Alterar ids das chaves primárias de inteiros com autoincrement para uuid ou identificações mais aleatórias e únicas;
- [ ] Utilizar conversão de HTML para o corpo do e-mail e tornar mais visualmente agradável;
- [ ] Hospedar a API.

# Atualizações
**Versão 1.1:**

A versão 1.1 contém toda a parte de autenticação e login, com critérios de senha e ativação de conta por e-mail, por parte de uma outro projeto dentro da mesma solução, chamada UsuariosAPI.
