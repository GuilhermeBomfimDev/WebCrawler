# WebCrawler - Teste para Dev Júnior eLaw

## Objetivo do Teste
Este teste tinha como objetivo criar um WebCrawler que é basicamente um robô para acessar um site e obter algumas informações. 

Neste teste em específico tinha como alvo obter os dados: 
<ul>
  <li>IP Adress</li>
  <li>Port</li>
  <li>Country</li>
  <li>Protocol</li>
</ul>

## Tecnologias usadas
<ul>
  <li>C#</li>
  <li>MySQL</li>
</ul>

## Arquitetura e estrutura do código
Para alcançar esse objetivo, eu usei da arquitetura MVC (Model-View-Controller) que é a que tenho mais afinidade além de deixar meu código mais organizado. 
Com o MVC, meu projeto ficou dividido da seguinte forma:

```bash
WebCrawler/ 
│ 
├── Controllers/ 
│  ├── WebCrawlerController.cs  
│ 
├── Models/ 
│  ├── DadosProxy.cs 
│  └── DadosLog.cs   
│ 
├── Uitls/ 
│  ├── PagesCount.cs 
│  └── ThreadLimiter.cs 
│ 
├── Data/ 
│  ├── WebCrawlerContext.cs 
│  └── WebCrawlerContextFactory.cs
│ 
├── Services/ 
│  ├── CrawlerService.cs 
│  └── HtmlSaverService.cs 
│  └── JsonWriterService.cs  
│ 
├── Migrations/ 
│  ├── (Nome da sua migration para Banco de dados).cs  
│ 
├── PrintHtml/ 
│  ├── (Todas as páginas html salvas de cada URL).html 
│ 
├── Jsons/ 
│  ├── (Arquivos Json Salvos ao rodar o projeto).json 
│
├── .env
├── .gitignore
├── README.md
└── Program.cs 
```

## Passo a passo do código
<ol>
  <li>Program.cs inicia o projeto e chama o WebCrawlerController.cs</li>
  <li>O controller centraliza todas as funções do projeto e começa definindo o horário inicial e chamando a função que obtem as URL de cada página do site pedido</li>
  <li>A função GetAllPages acessa o site pedido, baixa o código HTML da página principal e com base nas tags &lt;a&gt; ele verifica quantas páginas o site tem ao total.
    Após saber a quantidade total de páginas, ele cria uma lista de URLs, monta cada URL adicionando "/page/{númeroDaPágina}", 
    salva nessa lista todas as URLs e retorna a lista</li>
  <li>Na sequência é chamada a função SaveAllHtml. Essa função acessa cada URL da lista que foi criada, obtem o código HTML de cada página, gera o nome de cada novo arquivo
    com base no número da página acessada, salva na pasta PrintHtml e adiciona em uma nova lista criada.</li>
  <li>A seguir é chamada a função ThreadLimiter, que é responsável por controlar a concorrência das tarefas que acessam os arquivos HTML salvos. 
    Ela permite que no máximo 3 tarefas sejam executadas ao mesmo tempo, evitando sobrecarga no sistema e garantindo uma execução mais estável e eficiente. 
    É com base nessa função que os dados de cada página HTML serão lidos e processados para extração das informações</li>
  <li>Por fim, a função SaveToJsonAsync monta um arquivo Json com os dados que foram obtidos e salva as informações solicitadas no banco de dados.</li>
</ol>


## Rodando localmente

Clone o projeto

```bash
git clone https://github.com/GuilhermeBomfimDev/WebCrawler.git
```

Instale as dependências

```bash
dotnet add package DotNetEnv --version 3.1.1
dotnet add package HtmlAgilityPack --version 1.12.1
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.13
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.13
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.3
```

Crie o arquivo .env no mesmo local que o Program.cs e coloque sua string de conexão. Essa string é para o MySQL.

```bash
# String do LocalHost MySQL
LocalConnectString=server=localhost;database=Nome_do_BancoDeDados;user=Seu_User;password=Sua_Senha;
```

Rode o projeto

```bash
F5 ou Ctrl+F5
```
