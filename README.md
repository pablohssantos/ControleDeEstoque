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

Sistema web desenvolvido como projeto acadêmico para a disciplina de Programação Web do curso de **Análise e Desenvolvimento de Sistemas** na **Fatec Americana**, sob orientação do **Professor Diógenes**.

O projeto implementa um sistema completo de controle de estoque com autenticação de usuários, gerenciamento de produtos, controle de entrada e saída, histórico de movimentações e relatórios.

---

## ✨ Funcionalidades

### 🔐 **Sistema de Autenticação**
- Login seguro com criptografia SHA-256
- Dois níveis de acesso: **Administrador** e **Usuário**
- Sistema de sessões para controle de acesso

### 👥 **Gerenciamento de Usuários** (Admin)
- Cadastro de novos usuários
- Edição de informações
- Ativação/Desativação de contas
- Pesquisa por nome, usuário ou ID

### 📦 **Gerenciamento de Produtos** (Usuário)
- Cadastro completo de produtos (nome, código, fornecedor, preços, estoque)
- Validação de margem de lucro mínima (20%)
- Edição de produtos cadastrados
- Ativação/Desativação de produtos
- Pesquisa por ID, nome ou código

### 📥 **Controle de Entrada**
- Sistema de pesquisa inteligente de produtos
- Registro de entradas com fornecedor e nota fiscal
- Atualização automática do estoque
- Modal interativo para registro rápido

### 📤 **Controle de Saída**
- Validação automática de estoque disponível
- Alertas visuais para produtos com estoque baixo/zerado
- Registro de saídas com identificação do cliente
- Atualização automática do estoque

### 📋 **Histórico de Movimentações**
- Visualização completa de todas as movimentações
- Código de cores (verde=entrada, vermelho=saída)
- Filtros de pesquisa por produto, fornecedor ou cliente
- Auditoria completa (quem fez, quando fez)

---

## 🛠️ Tecnologias Utilizadas

### **Backend**
- **C# / ASP.NET Web Forms** - Framework principal
- **MySQL** - Banco de dados relacional
- **ADO.NET** - Acesso a dados
- **SHA-256** - Criptografia de senhas

### **Frontend**
- **HTML5 / CSS3** - Estrutura e estilização
- **JavaScript** - Interações do lado do cliente
- **Master Pages** - Layout consistente

### **Arquitetura**
- **Padrão DAL** (Data Access Layer) - Separação de camadas
- **ViewState** - Gerenciamento de estado
- **Session** - Controle de autenticação

---

## 📊 Banco de Dados

### **Estrutura das Tabelas**

#### **logins** - Controle de usuários
```sql
- id (INT, PK, AUTO_INCREMENT)
- nomeFuncionario (VARCHAR)
- usuarioFuncionario (VARCHAR, UNIQUE)
- senhaHash (VARCHAR)
- tipoUsuario (ENUM: 'Admin', 'Usuario')
- ativo (BOOLEAN)
- dataCriacao (DATETIME)
```

#### **produtos** - Cadastro de produtos
```sql
- id (INT, PK, AUTO_INCREMENT)
- nome (VARCHAR)
- codigo (VARCHAR, UNIQUE)
- descricao (TEXT)
- fornecedor (VARCHAR)
- quantidadeEstoque (INT)
- precoCusto (DECIMAL)
- precoVenda (DECIMAL)
- ativo (BOOLEAN)
- dataCadastro (DATETIME)
```

#### **movimentacoes** - Histórico de entrada/saída
```sql
- id (INT, PK, AUTO_INCREMENT)
- produtoId (INT, FK)
- produtoNome (VARCHAR)
- tipo (ENUM: 'Entrada', 'Saida')
- quantidade (INT)
- fornecedor (VARCHAR)
- cliente (VARCHAR)
- notaFiscal (VARCHAR)
- observacao (TEXT)
- usuarioId (INT)
- usuarioNome (VARCHAR)
- dataMovimentacao (DATETIME)
```

---

## 🖥️ Demonstração

### 🔐 **Tela de Login e Gerenciamento de Logins**
<img width="1905" height="906" alt="image" src="https://github.com/user-attachments/assets/a4d2423d-44f7-45da-b61c-36a6b28afff8" />
<img width="1910" height="927" alt="image" src="https://github.com/user-attachments/assets/9fd08a43-e925-44af-a00b-cab011a3f270" />
<img width="1912" height="931" alt="image" src="https://github.com/user-attachments/assets/bcfc333e-078b-443a-8117-babf95de0bdb" />
<img width="1899" height="921" alt="image" src="https://github.com/user-attachments/assets/dc63dc89-b3cb-418d-b589-7d15af98fede" />

### 📦 **Gerenciamento de Produtos**

<img width="1917" height="936" alt="image" src="https://github.com/user-attachments/assets/b7840957-89d2-4e00-a253-b714531687e4" />
<img width="1900" height="942" alt="image" src="https://github.com/user-attachments/assets/415e56df-899a-4271-95c1-0c6abfe01f83" />
<img width="1909" height="943" alt="image" src="https://github.com/user-attachments/assets/656c7517-279e-46d5-88a7-15d1265ac1d4" />

### 📥 **Controle de Entrada (Modal)**
<img width="1919" height="931" alt="image" src="https://github.com/user-attachments/assets/ef137022-3010-4172-9789-f4d44d43976f" />
<img width="1914" height="926" alt="image" src="https://github.com/user-attachments/assets/9bebe47f-7eeb-4d77-b185-739291adfb11" />

### 📤 **Controle de Saída (com alertas)**
<img width="1894" height="931" alt="image" src="https://github.com/user-attachments/assets/05eb3530-e625-4cd4-b9ac-5908d471b218" />
<img width="1874" height="930" alt="image" src="https://github.com/user-attachments/assets/9e562a80-2cd7-4a18-a309-ec38993da103" />

### 📋 **Histórico Colorido**
<img width="1895" height="926" alt="image" src="https://github.com/user-attachments/assets/1f164552-9e2e-42e5-b3a0-12c450991c63" />

---

## 🛠️ Como Instalar

### **Pré-requisitos**
- Visual Studio 2019 ou superior
- MySQL Server 8.0 ou superior
- .NET Framework 4.7.2 ou superior

### **Passo 1: Clone o repositório**
```bash
git clone https://github.com/pablohssantos/ControleDeEstoque.git
cd ControleDeEstoque
```

### **Passo 2: Configure o banco de dados**

# 📋 Instruções de Instalação do Banco de Dados

## Passo a Passo

1. **Abra o MySQL Workbench ou linha de comando do MySQL**

2. **Crie o banco de dados:**
```sql
CREATE DATABASE controledeestoque;
USE controledeestoque;
```

3. **Execute os scripts na ordem:**

### **Ordem de execução:**
1. `01_criar_tabela_logins.sql`
2. `02_criar_tabela_produtos.sql`
3. `03_criar_tabela_movimentacoes.sql`
4. `04_insert_admin_padrao.sql`

### **No MySQL Workbench:**
- File → Open SQL Script → Selecione o arquivo
- Clique no raio ⚡ para executar

### **Na linha de comando:**
```bash
mysql -u root -p controledeestoque < 01_criar_tabela_logins.sql
mysql -u root -p controledeestoque < 02_criar_tabela_produtos.sql
mysql -u root -p controledeestoque < 03_criar_tabela_movimentacoes.sql
mysql -u root -p controledeestoque < 04_insert_admin_padrao.sql
```

## 🔐 Credenciais Padrão

**Administrador:**
- Usuário: `admin`
- Senha: `admin123`

⚠️ **IMPORTANTE:** A senha está criptografada em SHA-256 no banco de dados por segurança.

## ✅ Verificação

Para confirmar que tudo foi criado corretamente:
```sql
-- Ver tabelas criadas
SHOW TABLES;

-- Verificar usuário admin
SELECT * FROM logins WHERE usuarioFuncionario = 'admin';
```

Pronto! Agora você pode acessar o sistema! 🚀
### **Passo 3: Compile e Execute**

1. Abra o projeto no Visual Studio
2. Compile o projeto (Ctrl + Shift + B)
3. Execute (F5)

### **Login Padrão**
- **Admin**: `admin` / Senha: `admin123`
- **Usuário**: Cadastre pela área administrativa

---

## 📚 Conceitos Aplicados

### **Programação**
✅ Orientação a Objetos (Classes DAL)  
✅ Validação de dados (client-side e server-side)  
✅ Criptografia (SHA-256)  
✅ Tratamento de exceções  
✅ Padrão de projeto (Data Access Layer)  

### **Banco de Dados**
✅ Modelagem relacional  
✅ Relacionamento entre tabelas (Foreign Keys)  
✅ Transações (para garantir integridade)  
✅ Consultas parametrizadas (prevenção de SQL Injection)  

### **Web**
✅ ASP.NET Web Forms  
✅ Master Pages  
✅ ViewState e Session  
✅ GridView e eventos  
✅ CSS responsivo  

---

## 🚀 Melhorias Futuras

- [ ] Dashboard com gráficos de vendas
- [ ] Relatórios em PDF/Excel
- [ ] Sistema de alertas de estoque mínimo
- [ ] Múltiplos fornecedores por produto
- [ ] Controle de validade de produtos
- [ ] API REST para integração
- [ ] Modo escuro
- [ ] Aplicativo mobile

---

## 👨‍💻 Autor

**Pablo Henrique Soares dos Santos**

📚 Análise e Desenvolvimento de Sistemas - 2º Semestre  
🏫 Fatec Americana - Ministro Ralph Biasi  
👨‍🏫 Orientador: Prof. Diógenes  
📅 2025

### 🔗 Contatos

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)]([https://linkedin.com/in/seu-perfil](https://www.linkedin.com/in/phs-soares-dos-santos-8b6676355/?trk=opento_sprofile_goalscard))
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
