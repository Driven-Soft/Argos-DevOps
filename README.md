# ARGOS - Sistema de Monitoramento Preventivo de Enchentes e Deslizamentos

## Descrição

O ARGOS é uma solução desenvolvida para monitoramento preventivo de enchentes e deslizamentos em áreas de risco, integrando sensores IoT, processamento de dados e serviços em nuvem.

A aplicação disponibiliza uma API REST desenvolvida em .NET e um banco de dados Oracle executados em containers Docker. A infraestrutura necessária para execução da solução é provisionada automaticamente na Microsoft Azure através de scripts de automação utilizando Azure CLI.

O objetivo do projeto é demonstrar uma arquitetura moderna baseada em Cloud Computing, Containers e Banco de Dados Oracle, permitindo o gerenciamento de informações relacionadas ao monitoramento de áreas sujeitas a desastres naturais.

---

## Desenhos da arquitetura

### Arquitetura Macro

<img width="986" height="581" alt="image" src="https://github.com/user-attachments/assets/5ca10c73-9a20-438f-a45e-801c6f6009d0" />

### Fluxo das requisições

<img width="1078" height="489" alt="image" src="https://github.com/user-attachments/assets/7b6143ac-b6ea-43bd-9141-88e77c9774b7" />

### Infraestrutura Azure

<img width="736" height="761" alt="image" src="https://github.com/user-attachments/assets/0fc0716e-1a89-4536-a3c1-ddefc26e0c8f" />

---

## Tecnologias Utilizadas

* .NET 10
* Oracle Database XE
* Docker
* Docker Compose
* Microsoft Azure
* Azure CLI
* Swagger

---

# Pré-Requisitos

Antes de iniciar a instalação, é necessário possuir:

* Conta Microsoft Azure ativa e configurada
* Azure CLI instalada
* Git instalado
* Bash (Linux, WSL ou Git Bash)

---

# Passo 1 - Clonar o Repositório

```bash
git clone <URL_DO_REPOSITORIO>
cd Argos-DevOps
```

---

# Passo 2 - Login na Azure

Realize autenticação na conta Azure:

```bash
az login
```

Será aberta uma janela do navegador para autenticação.

---

# Passo 3 - Dar Permissão aos Scripts (se necessário)

```bash
chmod +x criacao.sh
chmod +x remocao.sh
```

---

# Passo 4 - Criar a Infraestrutura

Execute o script de criação:

```bash
./criacao.sh
```

O script realizará automaticamente:

* Criação do Resource Group
* Criação da Virtual Network
* Criação da Subnet
* Criação do Network Security Group
* Configuração das regras de acesso
* Criação da Máquina Virtual Ubuntu
* Instalação do Docker
* Instalação do Docker Compose
* Instalação do Git

A criação da infraestrutura pode levar alguns minutos.

---

# Passo 5 - Obter o IP Público da Máquina Virtual

```bash
az vm list-ip-addresses \
  --resource-group rg-argos \
  --name vm-argos \
  --output table
```

Anote o endereço IP retornado.

---

# Passo 6 - Conectar na Máquina Virtual

```bash
ssh azureuser@IP_DA_VM
```

Informe a senha configurada no script de criação (ArgosGS@2026).

---

# Passo 7 - Clonar o Projeto na VM

Dentro da VM:

```bash
git clone https://github.com/Driven-Soft/Argos-DevOps.git
cd Argos-DevOps
```

---

# Passo 8 - Construir as Imagens Docker

```bash
docker compose build
```

---

# Passo 9 - Iniciar os Containers

```bash
docker compose up -d
```

---

# Passo 10 - Verificar os Containers

```bash
docker ps
```

Os seguintes containers devem estar em execução:

```text
argos-api
argos-oracle
```

---

# Passo 11 - Acessar o Swagger

Abra o navegador:

```text
http://IP_DA_VM:8080/swagger
```

A interface Swagger deverá ser exibida. Caso não apareça, aguarde algnus minutos até que o container do banco tenha tempo de subir.

---

# Passo 12 - Testar a API

Utilize os endpoints disponíveis para:

* Criar registros (POST)
* Consultar registros (GET)
* Atualizar registros (PUT)
* Excluir registros (DELETE)

---

# Verificação de Persistência dos Dados

Após cadastrar registros através do Swagger:

## Acessar o Container Oracle

```bash
docker exec -it argos-oracle bash
```

## Conectar ao Banco

```bash
sqlplus system/argos@XEPDB1
```

## Listar Tabelas

```sql
SELECT table_name FROM user_tables;
```

## Consultar Dados

```sql
SELECT * FROM NOME_DA_TABELA;
```

Os registros cadastrados pela API deverão estar presentes no banco de dados.

---

# Teste de Persistência

Parar os containers:

```bash
docker compose down
```

Subir novamente:

```bash
docker compose up -d
```

Repetir a consulta SQL:

```sql
SELECT * FROM NOME_DA_TABELA;
```

Os dados deverão permanecer armazenados, comprovando a persistência através do volume Docker.

---

# Remoção da Infraestrutura

Para remover todos os recursos criados na Azure:

```bash
./remocao.sh
```

O script removerá completamente o Resource Group e todos os recursos associados ao projeto.

---

# Integrantes

* Max Hayashi Batista (RM563717)
* Felipe Bezerra Beatrici (RM564723)
* Henrique Cunha Torres (RM565119)

---

# Disciplina

DevOps Tools & Cloud Computing

Global Solution 2026
