using Business;
using Business.Services;
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
FuncionarioRepository repo_funcionario = new FuncionarioRepository(db);
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
    Console.WriteLine("10 - Criar Funcionario");
    Console.WriteLine("11 - Listar Funcionarios");
    Console.WriteLine("12 - Deletar Funcionarios");
    Console.WriteLine("13 - Atualizar Funcionarios");
    Console.WriteLine("14 - Cargo");

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

        case "10":
            Funcionario f = new Funcionario();
            f.Cargos = new List<string>();

            Console.Write("Nome do funcionário: ");
            f.Nome = Console.ReadLine();

            string cargo;

            // logica para adiocionar o cargo
            do
            {
                Console.WriteLine("\nDigite o cargo (ou 0 para sair):");
                Console.WriteLine("1 - Caixa\n2 - Repositor\n3 - Gerente\n4 - Entregador\n0 - Sair");
                cargo = Console.ReadLine();

                if (cargo != "0")
                {
                    string nomeCargo = cargo switch
                    {
                        "1" => "caixa",
                        "2" => "repositor",
                        "3" => "gerente",
                        "4" => "entregador",
                        _ => null
                    };

                    if (nomeCargo != null)
                    {
                        f.Cargos.Add(nomeCargo);
                        Console.WriteLine($"Cargo '{nomeCargo}' adicionado.");
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida!");
                    }
                }

            } while (cargo != "0");

            Console.WriteLine("Digite o salario: (use ,) ");
            decimal salario = decimal.Parse(Console.ReadLine());

            Console.Write("Digite a hora de entrada (HH:mm): ");
            string hora_entrada = Console.ReadLine();
            f.HoraEntrada = DateTime.Parse(hora_entrada);

            Console.Write("Digite a hora de entrada (HH:mm): ");
            string hora_saida = Console.ReadLine();
            f.HoraSaida = DateTime.Parse(hora_saida);

            Console.WriteLine("Digite o seu regime contratuaL: (1 - CLT\n2 - CNPJ)\n");
            string regime_contratual = Console.ReadLine();
            switch (regime_contratual)
            {
                case "1":
                    f.RegimeContratual = "CLT";
                    break;

                case "2":
                    f.RegimeContratual = "CNPJ";
                    break;
            }

            Console.WriteLine("Digite a loja\n1 - Aracati\n2 - Russas");
            int loja_escolhida = int.Parse(Console.ReadLine());
            f.LojaId = loja_escolhida;
            repo_funcionario.CriarFuncionario(f);

            break;

        case "11":
            repo_funcionario.ListarFuncionarios();
            break;

        case "12":
            repo_funcionario.ListarFuncionarios();
            Console.WriteLine("Digite o id qual funcionario deseja apagar?");
            int id_funcionario_deletar = int.Parse(Console.ReadLine());
            repo_funcionario.DeletarFuncionario(id_funcionario_deletar);
            break;

        case "13":
            repo_funcionario.ListarFuncionarios();
            int id_funcionario = int.Parse(Console.ReadLine());
            repo_funcionario.AtualizarFuncionario(repo_funcionario.FuncionarioPeloId(id_funcionario));
            break;

        case "14":
            Funcionario func = new Funcionario();
            FuncionarioService service = new FuncionarioService();
            service.AssociarFucionarioComCargos(func);
            break;
        case "0":
            Console.WriteLine("Finalizando!");
            break;

        default:

            Console.WriteLine("Opção inválida!");
            break;
    }
} while (opcao != "0");



