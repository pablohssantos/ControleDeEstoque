# 📦 Sistema de Controle de Estoque

<div align="center">
  
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)
![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)

**Sistema completo de gerenciamento de estoque desenvolvido em ASP.NET Web Forms**

</div>

---

## 📖 Sobre o Projeto

Sistema web desenvolvido como projeto acadêmico para a disciplina de **Programação Web** do curso de **Análise e Desenvolvimento de Sistemas** na **Fatec Americana**, sob orientação do **Professor Diógenes**.

O projeto implementa um sistema completo de controle de estoque com autenticação de usuários, gerenciamento de produtos, controle de entrada e saída, e histórico de movimentações.

---

## ✨ Funcionalidades

### 🔐 **Sistema de Autenticação**
- Login seguro com criptografia SHA-256
- Dois níveis de acesso: **Administrador** e **Usuário**
- Controle de sessões

### 👥 **Gerenciamento de Usuários** (Admin)
- Cadastro, edição e exclusão de usuários
- Ativação/Desativação de contas
- Pesquisa por nome, usuário ou ID

### 📦 **Gerenciamento de Produtos** (Usuário)
- Cadastro completo (nome, código, fornecedor, preços, estoque)
- Validação de margem de lucro mínima (20%)
- Edição e ativação/desativação de produtos
- Sistema de pesquisa

### 📥 **Controle de Entrada**
- Pesquisa inteligente de produtos
- Registro com fornecedor e nota fiscal
- Atualização automática do estoque

### 📤 **Controle de Saída**
- Validação de estoque disponível
- Alertas visuais para estoque baixo/zerado
- Registro com identificação do cliente
- Atualização automática do estoque

### 📋 **Histórico de Movimentações**
- Visualização completa das movimentações
- Código de cores (verde=entrada, vermelho=saída)
- Filtros de pesquisa
- Auditoria completa

---

## 🛠️ Tecnologias Utilizadas

- **C# / ASP.NET Web Forms** - Framework principal
- **MySQL** - Banco de dados
- **ADO.NET** - Acesso a dados
- **SHA-256** - Criptografia de senhas
- **HTML5 / CSS3** - Interface
- **JavaScript** - Interações
- **Padrão DAL** - Separação de camadas

---

## 📊 Banco de Dados

### **Tabelas do Sistema**

**logins** - Usuários do sistema
- `id`, `nomeFuncionario`, `usuarioFuncionario`, `senhaHash`, `tipoUsuario`, `ativo`, `dataCriacao`

**produtos** - Cadastro de produtos
- `id`, `nome`, `codigo`, `descricao`, `fornecedor`, `quantidadeEstoque`, `precoCusto`, `precoVenda`, `ativo`, `dataCadastro`

**movimentacoes** - Histórico de entrada/saída
- `id`, `produtoId`, `produtoNome`, `tipo`, `quantidade`, `fornecedor`, `cliente`, `notaFiscal`, `observacao`, `usuarioId`, `usuarioNome`, `dataMovimentacao`

---

## 🖥️ Demonstração

### 🔐 Tela de Login e Gerenciamento de Logins
<img width="1905" alt="Login" src="https://github.com/user-attachments/assets/a4d2423d-44f7-45da-b61c-36a6b28afff8" />
<img width="1910" alt="Gerenciar Usuários" src="https://github.com/user-attachments/assets/9fd08a43-e925-44af-a00b-cab011a3f270" />
<img width="1912" alt="Cadastrar Usuário" src="https://github.com/user-attachments/assets/bcfc333e-078b-443a-8117-babf95de0bdb" />
<img width="1899" alt="Editar Usuário" src="https://github.com/user-attachments/assets/dc63dc89-b3cb-418d-b589-7d15af98fede" />

### 📦 Gerenciamento de Produtos
<img width="1917" alt="Lista de Produtos" src="https://github.com/user-attachments/assets/b7840957-89d2-4e00-a253-b714531687e4" />
<img width="1900" alt="Cadastrar Produto" src="https://github.com/user-attachments/assets/415e56df-899a-4271-95c1-0c6abfe01f83" />
<img width="1909" alt="Editar Produto" src="https://github.com/user-attachments/assets/656c7517-279e-46d5-88a7-15d1265ac1d4" />

### 📥 Controle de Entrada
<img width="1919" alt="Controle de Entrada" src="https://github.com/user-attachments/assets/ef137022-3010-4172-9789-f4d44d43976f" />
<img width="1914" alt="Modal de Entrada" src="https://github.com/user-attachments/assets/9bebe47f-7eeb-4d77-b185-739291adfb11" />

### 📤 Controle de Saída
<img width="1894" alt="Controle de Saída" src="https://github.com/user-attachments/assets/05eb3530-e625-4cd4-b9ac-5908d471b218" />
<img width="1874" alt="Modal de Saída" src="https://github.com/user-attachments/assets/9e562a80-2cd7-4a18-a309-ec38993da103" />

### 📋 Histórico de Movimentações
<img width="1895" alt="Histórico" src="https://github.com/user-attachments/assets/1f164552-9e2e-42e5-b3a0-12c450991c63" />

---

## 🛠️ Como Instalar

### **Pré-requisitos**
- Visual Studio 2019 ou superior
- MySQL Server 8.0 ou superior
- .NET Framework 4.7.2 ou superior

---

### **Passo 1: Clone o repositório**
```bash
git clone https://github.com/pablohssantos/ControleDeEstoque.git
cd ControleDeEstoque
```

---

### **Passo 2: Configure o banco de dados**

#### **2.1 - Crie o banco de dados:**
```sql
CREATE DATABASE controledeestoque;
USE controledeestoque;
```

#### **2.2 - Importe as tabelas:**

**Opção A - MySQL Workbench:**
1. Abra o MySQL Workbench
2. Vá em **Server → Data Import**
3. Selecione **"Import from Self-Contained File"**
4. Escolha o arquivo `Database/estrutura_banco.sql`
5. Clique em **"Start Import"**

**Opção B - Linha de Comando:**
```bash
mysql -u root -p controledeestoque < Database/estrutura_banco.sql
```

#### **2.3 - Login Padrão:**

Após importar o banco, você já pode fazer login:

| Campo | Valor |
|-------|-------|
| **Usuário** | `admin` |
| **Senha** | `admin` |

> ⚠️ **IMPORTANTE:** O usuário admin já vem criado automaticamente no arquivo SQL! A senha está criptografada (SHA-256) por segurança.

---

### **Passo 3: Configure a conexão no código**

Abra os arquivos `*DAL.cs` (UsuarioDAL, ProdutoDAL, MovimentacaoDAL) e atualize a string de conexão:
```csharp
private string conexaoBD = "Server=localhost;Database=controledeestoque;Uid=root;Pwd=SUA_SENHA_AQUI;";
```

Substitua `SUA_SENHA_AQUI` pela senha do seu MySQL.

---

### **Passo 4: Execute o projeto**

1. Abra o projeto no **Visual Studio**
2. Compile (Ctrl + Shift + B)
3. Execute (F5)
4. Faça login com:
   - Usuário: `admin`
   - Senha: `admin`

---

## 📚 Conceitos Aplicados

### **Programação**
✅ Orientação a Objetos  
✅ Padrão DAL (Data Access Layer)  
✅ Validação de dados  
✅ Criptografia SHA-256  
✅ Tratamento de exceções  

### **Banco de Dados**
✅ Modelagem relacional  
✅ Foreign Keys  
✅ Transações  
✅ Prevenção de SQL Injection  

### **Web**
✅ ASP.NET Web Forms  
✅ Master Pages  
✅ ViewState e Session  
✅ GridView  
✅ CSS responsivo  

---

## 🚀 Melhorias Futuras

- [ ] Dashboard com gráficos
- [ ] Relatórios em PDF/Excel
- [ ] Alertas de estoque mínimo
- [ ] Controle de validade
- [ ] API REST
- [ ] Modo escuro
- [ ] App mobile

---

## 👨‍💻 Autor

**Pablo Henrique Soares dos Santos**

📚 Análise e Desenvolvimento de Sistemas - 2º Semestre  
🏫 Fatec Americana - Ministro Ralph Biasi  
👨‍🏫 Prof. Diógenes  
📅 2025

### 🔗 Contatos

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/pablo-henrique-soares-dos-santos-8b6676355)
[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/pablohssantos)
[![Email](https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:pablohssoares@gmail.com)
---

## 📄 Licença

Este projeto foi desenvolvido para fins acadêmicos.

---

<div align="center">
  
**⭐ Se este projeto te ajudou, deixe uma estrela!**

Desenvolvido com 💙 por Pablo Santos

</div>
