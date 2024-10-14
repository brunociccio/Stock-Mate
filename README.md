# StockMate API

**StockMate** é uma API para gerenciamento de produtos e categorias utilizando MongoDB como banco de dados. A API oferece funcionalidades para criar, atualizar, listar e excluir produtos e categorias, além de fornecer documentação e monitoramento de saúde da aplicação via HealthCheck.

## Pré-requisitos

- [.NET 6 ou superior](https://dotnet.microsoft.com/download)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [MongoDB Compass](https://www.mongodb.com/products/compass) (opcional para visualização do banco de dados)
- [Insomnia ou Postman](https://insomnia.rest/download) para testar os endpoints

## Configuração do Banco de Dados

Antes de rodar a aplicação, configure o MongoDB com as seguintes variáveis de ambiente:

### Passo 1: Configuração das Variáveis de Ambiente

No **CMD** (Prompt de Comando), configure as variáveis de ambiente necessárias para a conexão com o MongoDB:

```bash
setx MONGO_CONNECTION_STRING "mongodb://localhost:27017"
setx MONGO_DB_NAME "StockMateDb"
```

Verifique se as variáveis foram configuradas corretamente:

```bash
echo %MONGO_CONNECTION_STRING%
```

A saída esperada deve ser:

```bash
mongodb://localhost:27017
```

### Passo 2: Abrir o MongoDB Compass (opcional)

Abra o **MongoDB Compass** para visualizar o banco de dados. Use a string de conexão configurada para acessar o banco **StockMateDb**.

## Como Rodar a Aplicação

### Passo 1: Restaurar as Dependências

No diretório do projeto, execute o seguinte comando para restaurar as dependências:

```bash
dotnet restore
```

### Passo 2: Rodar a Aplicação

Para iniciar a aplicação, execute:

```bash
dotnet run
```

A aplicação estará disponível em: `http://localhost:5219`.

## Endpoints da API

### Produtos

- **POST**: Criar um novo produto
  - **URL**: `http://localhost:5219/api/products`
  - **Exemplo de JSON de entrada**:
    ```json
    {
      "name": "Produto Exemplo",
      "price": 19.99,
      "description": "Este é um exemplo de produto.",
      "categoryId": "670d373a477327df30e8b451"
    }
    ```
  - **Exemplo de resposta esperada**:
    ```json
    {
      "id": "670d4c25477327df30e8b453",
      "name": "Produto Exemplo",
      "description": "Este é um exemplo de produto.",
      "categoryId": "670d373a477327df30e8b451",
      "price": 19.99
    }
    ```

- **GET**: Listar todos os produtos
  - **URL**: `http://localhost:5219/api/products`

- **GET (por ID)**: Obter um produto específico por ID
  - **URL**: `http://localhost:5219/api/products/{id}`

- **PUT**: Atualizar um produto existente
  - **URL**: `http://localhost:5219/api/products/{id}`
  - **Exemplo de JSON de entrada**:
    ```json
    {
      "name": "Celular Atualizado",
      "description": "Smartphone atualizado de última geração.",
      "price": 3199.99,
      "categoryId": "670d373a477327df30e8b451"
    }
    ```

- **DELETE**: Excluir um produto por ID
  - **URL**: `http://localhost:5219/api/products/{id}`

### Categorias

- **POST**: Criar uma nova categoria
  - **URL**: `http://localhost:5219/api/categories`
  - **Exemplo de JSON de entrada**:
    ```json
    {
      "name": "Eletrônicos",
      "description": "Produtos eletrônicos."
    }
    ```

- **GET**: Listar todas as categorias
  - **URL**: `http://localhost:5219/api/categories`

- **GET (por ID)**: Obter uma categoria específica por ID
  - **URL**: `http://localhost:5219/api/categories/{id}`

- **PUT**: Atualizar uma categoria existente
  - **URL**: `http://localhost:5219/api/categories/{id}`
  - **Exemplo de JSON de entrada**:
    ```json
    {
      "name": "Eletrônicos Atualizados",
      "description": "Produtos eletrônicos atualizados."
    }
    ```

- **DELETE**: Excluir uma categoria por ID
  - **URL**: `http://localhost:5219/api/categories/{id}`

## HealthCheck

Verifique o estado de saúde da aplicação e a conexão com o MongoDB acessando:

- **URL**: `http://localhost:5219/health`

A resposta esperada é algo como:

```json
{
  "status": "Healthy",
  "checks": [
    {
      "name": "MongoDB Connection Health",
      "status": "Healthy",
      "description": null
    }
  ]
}
```

## Swagger - Documentação da API

A documentação da API, incluindo os endpoints disponíveis e suas descrições, pode ser acessada em:

- **URL**: `http://localhost:5219/swagger/`

## Ambiente de Testes com xUnit

Os testes unitários para a aplicação estão implementados usando **xUnit**. Para rodar os testes:

1. Navegue até o diretório do projeto.
2. Execute o seguinte comando para rodar os testes:

```bash
dotnet test
```

Os testes cobrem as principais funcionalidades dos repositórios de produtos e categorias, garantindo que os métodos de criação, listagem e exclusão estejam funcionando corretamente.

---