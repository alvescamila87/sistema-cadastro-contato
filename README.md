# Sistema de Cadastro de Contatos

## Sobre o projeto
O "Sistema de cadastro de contatos" é projeto um permite a visualização, inclusão, edição e exclusão de um cadastro de contato, considerando informações como: nome, empresa, telefones (comercial e pessoal) e emails.

## Funcionalidades 
* Visualizar contato
* Adicionar contato
* Editar contato
* Deletar contato
* Listar contatos
* Consultar contato por: Nome, Empresa, Telefones (pessoal e/ou comercial) e/ou Email.

### Tecnologia utilizada
* C#
* ASP.NET MVC,
* Banco de dados em memória
* Documentação C# <summary>
* Swagger (annotations para geração de documentação de API)

## Documentação API
![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/ce11957d-9094-4040-a47b-b1cf77f05950)

### Exemplos para teste na API

#### Inclusão de contato
```json {
  {
  "nome": "Ana Maria Silva",
  "empresa": "Corp TI",
  "listaEmails": [
    {
      "enderecoEmail": "ana_maria1@gmail.com",
    },
    {
      "enderecoEmail": "ana_maria_costa2@gmail.com",
    }
  ],
  "telefonePessoal": "47-99999-5555",
  "telefoneComercial": "47-99999-3333"
}
```

#### Alteração de contato
```json {
  {
  "id": 3,
  "nome": "Ana Maria Costa",
  "empresa": "Corp II TI",
  "listaEmails": [
    {
      "id": 1,
      "enderecoEmail": "ana_maria_costa2@gmail.com",
      "contatoId": 3
    }
  ],
  "telefonePessoal": "47-99999-11111",
  "telefoneComercial": "47-99999-222222"
}
```

### Autor
* Camila Alves

