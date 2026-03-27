using Business;
using ConsoleApp;
using Domain;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Cms;
using System;

Console.WriteLine("Iniciando cadastro de produto!");
Console.WriteLine("");

Database db = new Database();
ProdutoRepository repo_produto = new ProdutoRepository(db);
ClienteRepository repo_cliente = new ClienteRepository(db);
EstoqueRepository repo_estoque = new EstoqueRepository(db);
VendaRepository repo_venda = new VendaRepository(db);
MenuRelatorio menuRelatorio = new MenuRelatorio();

string opcao;
do
{
    Console.WriteLine("\n");
    Console.WriteLine("1 - Criar produto");
    Console.WriteLine("2 - Listar produtos");
    Console.WriteLine("3 - Alterar produtos");
    Console.WriteLine("4 - Deletar produtos");
    Console.WriteLine("5 - Criar lojas");
    Console.WriteLine("6 - Repor estoque");
    Console.WriteLine("7 - Retirar estoque");
    Console.WriteLine("8 - Mostrar estoque");
    Console.WriteLine("9 - Relatorio vendas");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("\n");


    Console.Write("Escolha uma opção: ");
    opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":

            Produto p = new Produto();

            Console.Write("Nome do produto: ");
            p.Nome = Console.ReadLine();

            Console.Write("Preço do produto: ");
            p.Preco = decimal.Parse(Console.ReadLine());

            Console.Write("Tipo da venda (unidade/quilo): ");
            p.TipoVenda = Console.ReadLine().ToLower();
            if (p.TipoVenda != "unidade" && p.TipoVenda != "quilo")
            {
                Console.WriteLine("Tipo de venda inválido!");
                break;
            }

            Console.Write("Categoria do produto (id): ");
            p.Categoria_id = int.Parse(Console.ReadLine());


                Console.Write("Quantidade inicial: ");
                decimal quantidade = decimal.Parse(Console.ReadLine());

                Console.Write("ID da loja: ");
                int lojaId = int.Parse(Console.ReadLine());


                int produtoId = repo_produto.CriarProdutos(p);

                repo_estoque.AdicionarEstoque(produtoId, lojaId, quantidade);
                Console.WriteLine("Produto criado com estoque!");

            break;

        case "2":

            repo_produto.ListarProdutos();
            break;

        case "3":

            Console.WriteLine("LISTA DE PRODUTOS ----------");
            repo_produto.ListarProdutos();
            Console.WriteLine();
            Console.Write("Digite o id do produto: ");
            int At_id = int.Parse(Console.ReadLine());

            Produto At_p = new Produto();

            Console.Write("Novo nome do produto: ");
            At_p.Nome = Console.ReadLine();

            Console.Write("Novo preço do produto: ");
            At_p.Preco = decimal.Parse(Console.ReadLine());

            Console.Write("Novo tipo da venda (unidade/quilo): ");
            At_p.TipoVenda = Console.ReadLine().ToLower();
            if (At_p.TipoVenda != "unidade" && At_p.TipoVenda != "quilo")
            {
                Console.WriteLine("Tipo de venda inválido!");
                break;
            }

            Console.Write("Nova categoria do produto (id): ");
            At_p.Categoria_id = int.Parse(Console.ReadLine());

            repo_produto.AtualizarProdutos(At_id, At_p);
            Console.WriteLine("LISTA ATUALIZADA ----------");
            repo_produto.ListarProdutos();
            Console.WriteLine("Produto atualizado com sucesso!");
            break;

        case "4":

            Console.Write("Digite o id do produto: ");
            int Del_p = int.Parse(Console.ReadLine());

            repo_produto.DeletarProdutos(Del_p);
            repo_produto.ListarProdutos();
            Console.WriteLine("Produto deletado com sucesso!");
            break;

        case "5": // Criando as lojas (em memoria)
            Console.WriteLine("Criando lojas!");
            Loja loja_aracati = new Loja(1, "Aracati", "Aracati", "Rua centro nº123", new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0));
            Loja loja_russas = new Loja(1, "Russas", "Russas", "Rua Central nº1010", new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0));
            Console.WriteLine(loja_aracati.ToString());
            Console.WriteLine(loja_russas.ToString());
            break;

        case "6": 

            Console.WriteLine("----- REPOSIÇÃO DE ESTOQUE -----");

            repo_produto.ListarProdutos();

            Console.Write("\nID do produto: ");
            int produtoId_repo = int.Parse(Console.ReadLine());

            Console.Write("ID da loja: ");
            int lojaId_repo = int.Parse(Console.ReadLine());

            Console.Write("Quantidade a adicionar: ");
            decimal quantidade_repo = decimal.Parse(Console.ReadLine());

            repo_estoque.ReporEstoque(produtoId_repo, lojaId_repo, quantidade_repo);

            break;

        case "7":

            Console.WriteLine("----- BAIXA DE ESTOQUE -----");

            repo_produto.ListarProdutos();

            Console.Write("\nID do produto: ");
            int produtoIdBaixa = int.Parse(Console.ReadLine());

            Console.Write("ID da loja: ");
            int lojaIdBaixa = int.Parse(Console.ReadLine());

            Console.WriteLine(repo_estoque.ObterEstoque(produtoIdBaixa, lojaIdBaixa));

            Console.Write("Quantidade a remover: ");
            decimal qtdBaixa = decimal.Parse(Console.ReadLine());

            repo_estoque.BaixarEstoque(produtoIdBaixa, lojaIdBaixa, qtdBaixa);

            break;

        case "8":

            repo_estoque.ListarEstoqueGeral();

            break;

        case "9":

            menuRelatorio.MenuRelatorioFunction(repo_venda);

            break;    

        case "0":
            Console.WriteLine("Finalizando!");
            break;

        default:

            Console.WriteLine("Opção inválida!");
            break;
    }
} while (opcao != "0");



