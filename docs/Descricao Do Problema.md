# Descrição do Problema:

Considere que uma empresa de varejo do ramo de Produtos Naturais deseja informatizar seu fluxo de trabalho. Atualmente, a empresa tem duas lojas fisicas (um estabelecimento comercial em Aracati e outro em Russas que funcionam em seus respectivos endereços e horários de funcionamento específicos). Hả planos de expansão de mais uma loja. A loja de Aracati possui 3 funcionários (2 caixas e 1 gerente), e a loja de Russas possul 4 funcionários (2 calxas, I repositor e 1 gerente). Cada funcionário possui uma única atribuição (é caixa, repositor ou gerente), recebe um salário, tem um horário de trabalho (hora de entrada e hora de saída) e um regime contratual (CLT ou CNPJ).

O público-alvo da empresa são clientes (que possuem nome, e-mail e histórico de compras) da região que adquirem os produtos na loja física ou clientes que adquirem produtos via loja online (site e-commerce da empresa). Para comprar via e-commerce, clientes precisam se cadastrar para acessar o sistema com login e senha. Um funcionário gerente também pode realizar o cadastro de um novo cliente quando este se dirigir à loja física. Para comprar na loja fisica, um cliente pode estar cadastrado ou não.

A Loja vende produtos diversos, como castanha, amendoim, linhaça e mel de cajú. Produtos podem ser vendidos na unidade ou no quilo. Para fins de organização e busca no sistema informatizado, um produto pertence à uma única categoria e um produto possui múltiplas tags (etiquetas).

Atualmente, quando o cliente compra na loja física, o funcionário caixa utiliza uma calculadora simples para contabilizar o valor de cada produto e calcular o valor final a ser pago. O sistema informatizado possibilitará reduzir falhas e irá melhorar a experiência do usuário. Na loja física, o cliente pode pagar via cartão de crédito, cartão de débito ou em dinheiro. Uma vez que o sistema esteja informatizado, o funcionário-caixa fará uso do sistema para efetuar a venda (o cliente vai à loja física, seleciona os produtos que quer comprar e o caixa para realizar a venda com base nos produtos selecionados pelo cliente). Semelhantemente, o cliente online também poderá adicionar produtos em um carrinho de compras, visualizar o valor final e realizar a compra via uma das modalidades de pagamento fornecidas (cartão de crédito ou débito).

Observação: Outros requisitos adicionais podem ser imaginados e implementados por vocês.



# Cronograma de Atividades

## Entrega 1- Modelagem + Estrutura + Fluxo minimo funcional 06/Março (Sexta-Feira)
Criação do Repositório Github. Adição do professor (silas.santiago@ifcc.edu.br) no ropositório; Enviar o link do ropositório no comentário privado da atividade no classroom.
Inicio do Desenvolvimento e codificação do Projeto Prático em Laboratório;

## Entrega incremental 1 com explicação ao professor em sala 13/Março (Sexta-Feira)
Diagrama simples de classes (criar o arquivo diagrama-classes.pdf em uma pasta docs/ no repositório github);
Solução C#.Net com projeto console e projeto(s) class library.
Implementação de regras de negócio e métodos para cadastro, leitura, atualização e remoção das entidades. Observação: A entrega incremental (release-1) deve conter um fluxo mínimo funcional do sistema, que permita ao menos: cadastro de produtos e simulação de uma venda simples.
Implementação das classes do dominio do problema proposto.
O entregável 1 deve estar na branch release-1.
Em sala, rode o sistema, e explique objetivamente as classes e estruturas ao professor;



# Detalhamento dos Entregáveis da Atividade:

## Diagrama de classes simples [0.5 pontos]: 
(podem desenhar à mão e fotografar ou podem usar uma ferramenta como LucidChart ou StarUML). Este diagrama deve estar na pasta docs/ do repositório git no github.

## Entregas incrementais da implementação técnica [3 pontos]: 
Em um repositório Github. Ao criar o repositório, adicione também um arquivo .gitignore (opção dotnet) e um arquivo de markdown README.md. Comandos básicos que você usará com git incluem git clone, git add., git commit -m "mensagem", git pull, git push, e estas operações podem também ser feitas via própria interface do visual studio code. Os membros da dupla devem ter contribuído para o desenvolvimento do projeto. O repositório deverá conter mínimo de 10 commits distribuídos ao longo do desenvolvimento. Há dois entregáveis de código-fonte:
13/Março - Entrega incremental 1 com explicação ao professor em sala;
20/Março - Entrega e apresentação da Implementação Final

## Vídeo Screencast individual com explicação técnica [4 pontos]:
Com a solução já desenvolvida, cada estudante grava um screencast da tela e explica os aspectos técnicos do projeto que foi construído. Explicar o fluxo do sistema. Justificar decisões (por exemplo, por que usou a estrutura de dados X para as entidades Y). O que será considerado erro grave: Não saber explicar o próprio código, não saber justificar uso de estruturas, código funcional mas sem compreensão. Não é necessário ligar a câmera. Faça o upload no Youtube com a opção "Não listado". Em seguida, envie o link do video na atividade do Google Classroom. O video deve ter uma duração mínima de 20 minutos. Opcionalmente, ao invés de um único vídeo, vocês podem preparar uma playlist não listada com videos curtos.

## Apresentação final em sala [2.5 pontos]:
Explicar aspectos de implementação que considere relevantes e importantes no desenvolvimento deste projeto. O propósito aqui é compartilhar as experiências de desenvolvimento (exitosas ou não) com a turma para aprendizagem conjunta.

## Relatório quanto ao uso de IA [obrigatório]: 
O uso de IA é permitido, mas avaliado. Mas deve-se preparar um relatório (Em PDF ou Markdown) em que seja descrito: (a) Quais ferramentas usou? (b) Quais prompts foram usados? (Exemplos de prompts utilizados); O que a IA gerou corretamente? O que você precisou corrigir? O que aprendeu revisando o código?. Quais tuas considerações quanto ao uso da IA para desenvolvimento de software durante o aprendizado de uma tecnologia? e no dia-a-dia no desenvolvimento de software ? Este relatório deve ficar na pasta docs/ do repositório.



# Aspectos Técnicos e de Implementação

- A solução deve ser organizada em um projeto console e em dois ou mais projetos class library. Por exemplo, Domain (com as Entidades), Business (para as regras de negócio) e Console.

- Deve existir também na solução um projeto de testes unitários (Projeto XUnit) para validar as implementações realizadas no projeto.
Usando os conhecimentos até agora discutidos na disciplina, vocês farão todo o gerenciamento dos dados em memória. Alternativamente, as seguintes opções também são válidas: Uso de arquivos de texto, arquivos JSON ou ADO.Net. Vocês não podem usar uma ORM neste estágio (como Dapper ou Entity Framework). Usem uma das opções sugeridas: nada (em memória), arquivos de texto (plain text), arquivos JSON ou acesso à dados via ADO.Net.

- Classes Obrigatórias: Empresa, Loja, Funcionário, Cliente, Produto, Categoria, Tag,
CarrinhoDeCompras.

- Vocês não deverão definir classes ou interfaces genéricas neste estágio;

- Operações obrigatórias: Para as classes modeladas no sistema, devem existir operações /métodos que permitam criar, obter, atualizar e remover objetos nas coleções.

    - Orientação sobre Commits: Ao realizarem commits, comitar uma funcionalidade por vez, independente se alterou um ou muitos arquivos. Dessa forma vai ser possível reverter uma mudança em caso de problemas. Verifiquem o seguinte link: https://www.conventionalcommits.org/en/v1.0.0/. Exemplos de commits conforme a convenção sugerida:
        feat: criação da classe Produto;
        feat: implementação do carrinho de compras;
        fix: correção cálculo total da venda feat: cadastro de clientes.