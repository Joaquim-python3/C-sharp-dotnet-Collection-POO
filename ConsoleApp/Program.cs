using Business;
using Domain;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Cms;
using System;

Console.WriteLine("Iniciando cadastro de produto!");
Console.WriteLine("");

Database db = new Database();
ProdutoRepository repo_produto = new ProdutoRepository(db);
ClienteRepository repo_cliente = new ClienteRepository(db);

string opcao;
do
{
    Console.WriteLine("\n");
    Console.WriteLine("1 - Criar produto");
    Console.WriteLine("2 - Listar produtos");
    Console.WriteLine("3 - Alterar produtos");
    Console.WriteLine("4 - Deletar produtos");
    Console.WriteLine("5 - Criar lojas");
    Console.WriteLine("6 - Iniciar compra");
    Console.WriteLine("7 - Sair");
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
                return;
            }

            Console.Write("Categoria do produto (id): ");
            p.Categoria_id = int.Parse(Console.ReadLine());


            repo_produto.CriarProdutos(p);
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
                return;
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

        case "6": // Iniciando as compras (aqui vai ficar a logica da compra)
            string possui_login = null; ;
            Console.WriteLine("-=-=- Iniciando compras! -=-=-");

            Console.WriteLine("Compra online? (s/n)");
            string compra_online = Console.ReadLine().ToUpper();

            if (compra_online == "S")
            {
                Console.WriteLine("Possui login? (s/n)");
                possui_login = Console.ReadLine().ToUpper();
            }

            // realizar compra online
            else if (compra_online == "S" && possui_login == "S")
            {
                Console.WriteLine("Digite login: ");
                string login_cliente = Console.ReadLine();
                Console.WriteLine("Digite senha: ");
                string senha_cliente = Console.ReadLine();
                repo_cliente.ProcurarClientePeloEmailESenha(login_cliente, senha_cliente);

            }
            else if (compra_online == "S" && possui_login == "N")
            {
                Cliente cliente_novo = new Cliente();
                Console.WriteLine("Digite seu nome: ");
                cliente_novo.Nome = Console.ReadLine();

                Console.WriteLine("Digite seu email: ");
                cliente_novo.Email = Console.ReadLine();

                Console.WriteLine("Digite login: ");
                cliente_novo.Login = Console.ReadLine();

                Console.WriteLine("Digite senha: ");
                cliente_novo.Senha = Console.ReadLine();

                repo_cliente.CriarCliente(cliente_novo);
            }

            else
            {
                Console.WriteLine("-=-=- Iniciando compra presencial! -=-=-");
                CarrinhoDeCompras carrinho = new CarrinhoDeCompras(1, DateTime.Now, new Cliente());
                Console.WriteLine("Produtos disponíveis: ");
                repo_produto.ListarProdutos();
                Console.WriteLine("Digite o id: ");
                string id_produto_escolhido = Console.ReadLine();

                Produto produto_para_adicionar = repo_produto.ProcurarProdutoPeloId(id_produto_escolhido);
                carrinho.AdicionarAoCarrinho(produto_para_adicionar);

                foreach (var produto in carrinho.Carrinho)
                {
                    Console.WriteLine(produto.ToString());
                }
            }


            break;

        case "7":
            Console.WriteLine("Finalizando!");
            break;

        default:

            Console.WriteLine("Opção inválida!");
            break;
    }
} while (opcao != "7");



