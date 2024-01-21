# books-api - api para uma loja de livros

<p align="center">
  <image
  src="https://img.shields.io/github/languages/count/JCDMeira/books-api"
  />
  <image
  src="https://img.shields.io/github/languages/top/JCDMeira/books-api"
  />
  <image
  src="https://img.shields.io/github/last-commit/JCDMeira/books-api"
  />
  <image
  src="https://img.shields.io/github/watchers/JCDMeira/books-api?style=social"
  />
</p>

# 📋 Indíce

- [Proposta](#id01)
  - [Conclusões](#id01.01)
- [Requisitos](#id02)
  - [Requisitos funcionais](#id02.1)
  - [Requisitos não funcionais](#id02.2)
  - [Requisitos não obrigatórios](#id02.3)
- [Aprendizados](#id03)
- [Feito com](#id04)
- [Pré-requisitos](#id05)
- [Procedimentos de instalação](#id06)
- [Autor](#id07)

# 🚀 Proposta <a name="id01"></a>

Este é o projeto tem como objetivo central a criação de uma api que conte com end-point de get all para pessoas, a entidade contém os dados de nome, idade e gênero, além de um id único em formato GUID.

# 🎯 Requisitos <a name="id02"></a>

## 🎯 Requisitos funcionais <a name="id02.1"></a>

Sua aplicação deve ter:

- Um end-point para cada um dos métodos get, put, post e delete.
- Deve ser possível criar um registro de livro
- Deve ser possível editar um registro de livro
- Deve ser possível deletar um registro de livro
  - usar o id na rota
- Deve ser possível buscar a lista de livros já catalogados
- Um livro deve ter:

  - Título
  - Autor
  - Descrição
  - preço
  - Ed - edição (opcional)
  - gênero literário

- Descrição e edição devem ser campo opicionais. E os demais obrigatórios
- É preciso usar um banco de dados mongoDB
  - deve ter um id único gerado para o mongoDB

## 🎯 Requisitos não funcionais <a name="id02.2"></a>

É obrigatório a utilização de:

- .net

## 🎯 Requisitos não obrigatórios <a name="id02.3"></a>

- Criar campo de data de criação que não pode ser alterado
- criar um campo de data de alteração que é alterado a cada update
  - o valor inicial na criação é o mesmo valor da data de criação
- configurar o máximo de descrição para 511 caracteres
- definir o valor mínimo do título para 10 caracteres e o máximo em 127

# Aprendizados <a name="id03"></a>

Para fazer o bong funionar é adicionado um driver o mongoDB.

Então é criado uma model que representa as configurações para conexão com a cluster.
Essa model traz representação de string de conexão, o nome da base e o nome da coleção.

```c#
namespace BookStoreApi.Models;

public class BookStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string BooksCollectionName { get; set; } = null!;
}
```

Então é adicionado n appseettings.json o seguuinte bloco:

```json
"BookStoreDatabase": {
    "ConnectionString": "mongodb+srv://nome:senha@cluster0.jhwjo5u.mongodb.net/",
    "DatabaseName": "BookStore",
    "BooksCollectionName": "Books"
  },
```

Esse bloco contém as dfinições da classe de configuração usada como model.

Após isso é preciso definir essa base no Program.cs, oo que é feito adicionando as seguintes linhas.

```c#
    builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection("BookStoreDatabase"));
```

Para usar de fato esse banco é criado um modelo da entidade que vamos armazenar e manipular, que é um livro. Essa classe estará na pasta de models.

```c#
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStoreApi.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Title")]
    public string Title { get; set; } = null!;

    public decimal Price { get; set; }
    public string? Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Author { get; set; } = null!;


    public int? Edition { get; set; } = null!;

    public DateTime? Created_time { get;  } =  DateTime.Now!;

    public DateTime? Updated_time { get; set; } = DateTime.Now!;
}
```

Quando há a marcação da interrogação após o tipo de dado é que ele é opcional, e os que não tem é devido a ser um dado obrigatório. Para casos comoo id, Created_time e updated_time é porque não se espera esses dados do body quando se cria, mas eles são inseridos pela controller.

E para casos de deixar um valor de fallback é usado a notação abaixo, sendo que estiver entre o = e a ! é o valor de fallback

```c#
= null!
```

A nossa controller não difere muito do modelo de uso normal, ou usado com um context db.

É importado a model e a service de book necessárias na parte superior do arquivo.

```c#
using BookStoreApi.Models;
using BookStoreApi.Services;
```

Então see cria a instância da service no construtor da classe.

```c#
private readonly BooksService _booksService;

  public BookController(BooksService booksService) =>
      _booksService = booksService;

```

No demais se usa os métodos com notação async/await e sempre usando a instância da srvice com o métodos presentes na service.
E para valores não esperados por rotas como o post e put são atribuídos dentro do método.

```c#
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace books_api.Controllers
{
    [Route("/api/books")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly BooksService _booksService;

        public BookController(BooksService booksService) =>
            _booksService = booksService;

        [HttpGet]
        public async Task<List<Book>> Get() =>
        await _booksService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
                return NotFound();

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Book newBook)
        {
            await _booksService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book updatedBook)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
                return NotFound();

            updatedBook.Id = book.Id;
            updatedBook.updated_time = DateTime.Now;

            await _booksService.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
                return NotFound();

            await _booksService.RemoveAsync(id);

            return NoContent();
        }
    }
}
```

Como melhoria podia se validar melhor dados vindos para os métodos, para não aceitar nada fora do formato exato esperado.

A service é construída usando o construtor para adicionar as propriedades do mongo, para executar como um client do mesmo.
Cada método executa alguma lógica de manipulação dos dados.

É interessante salientar o comportamento de sobrecarga de métodos do c# aqui, que tem dois getAsync, sendo um que não recebe parâmetros e outro que recebe. Isso já torna o service capaz de identificar qual método chamar em cada caso.

```c#
using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class BooksService
{
    private readonly IMongoCollection<Book> _booksCollection;

    public BooksService(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _booksCollection = mongoDatabase.GetCollection<Book>(
            bookStoreDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<Book>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Book?> GetAsync(string id) =>
        await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book newBook) =>
        await _booksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Book updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x.Id == id);
}
```

Para a service poder ser usada adequadamente é preciso declarar ela no Program.cs como mostrado abaixo.

```c#
builder.Services.AddSingleton<BooksService>();
```

## Considerações

Conectar um banco mongo de forma simples no c# com asp.net é bem simpes, já que a doc do .net fornece exemplos muito bons.
Apesar disso não é tão simples encontrar alguns exemplos de uso, na doc ou em vídeos de tutoriais. Como por exemplo, definir o dado de forma mais completa na model, tal como quantidade mínima e máxima de caracteres de um campo, o que não sei se é devido a prática não ser usada no .net ou se a informação não é tão fácil de localizar.

A service interage bem com a controller, o que é uma diferença para o context db que não tinha service.

Para dados mais complexos e tratamentos mais elaborados pode ser mais complicado de achar exemplos concretos.

# 🛠 Feito com <a name="id04"></a>

<br />

- C#
- .net 8
- visual studio
- mongoDB

<br />

# :sunglasses: Autor <a name="id07"></a>

<br />

- Linkedin - [jeanmeira](https://www.linkedin.com/in/jeanmeira/)
- Instagram - [@jean.meira10](https://www.instagram.com/jean.meira10/)
- GitHub - [JCDMeira](https://github.com/JCDMeira)
