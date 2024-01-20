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
