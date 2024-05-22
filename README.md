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
* Documentação C#
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

### QA Step-by-step
#### Adicionar contato
![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/6142c30c-c472-40d5-b80d-df373761d36b)

#### Listar contatos cadastrados
![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/cfa082f6-cc95-4374-8aea-0de3b625b77d)

#### Pesquisar contato por ID
![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/a9a84432-af2d-4feb-909c-1cc530edab38)

#### Filtrar contato por:
![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/6870ccc1-38c4-4a9d-826a-de38d9aad704)

#### Ver mais do contato
![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/b7b0deb0-7f71-4cec-b61f-6fdfe3c9a916)

#### Editar dados
![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/85faafc6-79a0-4c87-912e-46a4c0e0b8d3)

![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/73c250dd-2412-4991-862f-4aefa3624257)

#### Excluir contato
![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/d354e191-0e90-4235-8f2e-fdfedd85547f)

![image](https://github.com/alvescamila87/sistema-cadastro-contato/assets/116912821/a4b15830-dbfe-4d4a-8044-e88adb0ca7fc)








### Autor
* Camila Alves

