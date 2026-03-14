using Business;
using Domain;

Console.WriteLine("Iniciando cadastro de produto!");
Console.WriteLine("");

Database db = new Database();
ProdutoRepository repo = new ProdutoRepository(db);

string opcao;
do
{
    Console.WriteLine("\n");
    Console.WriteLine("1 - Criar produto");
    Console.WriteLine("2 - Listar produtos");
    Console.WriteLine("3 - Alterar produtos");
    Console.WriteLine("4 - Deletar produtos");
    Console.WriteLine("5 - Sair");
    Console.WriteLine("\n");
    Console.WriteLine("-=-=-=-=-=-=-=-=--=-=-");
    Console.WriteLine("-= 6 - Menu de CRUD -=");
    Console.WriteLine("-=-=-=-=-=-=-=-=--=-=-");


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

            repo.CriarProdutos(p);
            break;

        case "2":

            repo.ListarProdutos();
            break;

        case "3":

            Console.WriteLine("LISTA DE PRODUTOS ----------");
            repo.ListarProdutos();
            Console.WriteLine();
            Console.Write("Digite o id do produto: ");
            int At_id = int.Parse(Console.ReadLine());

            Produto At_p = new Produto();

            Console.Write("Digite o novo nome do produto: ");
            At_p.Nome = Console.ReadLine();

            Console.Write("Digite o novo preço do produto: ");
            At_p.Preco = decimal.Parse(Console.ReadLine());

            repo.AtualizarProdutos(At_id, At_p);
            Console.WriteLine("LISTA ATUALIZADA ----------");
            repo.ListarProdutos();
            Console.WriteLine("Produto atualizado com sucesso!");
            break;

        case "4":

            Console.Write("Digite o id do produto: ");
            int Del_p = int.Parse(Console.ReadLine());

            repo.DeletarProdutos(Del_p);
            repo.ListarProdutos();
            Console.WriteLine("Produto deletado com sucesso!");
            break;

        case "5":

            Console.WriteLine("Finalizando!");
            break;

        case "6":
        // isso aqui sera usado para ser usado para manipular os objetos
            Console.WriteLine("1 - LOJA");
            Console.WriteLine("2 - FUNCIONARIO");
            Console.WriteLine("3 - PRODUTO");
            Console.WriteLine("7 - Sair");

            string opcao_crud = Console.ReadLine();

            switch (opcao_crud)
            {
                case "1":
                    break;
                default:
                    Console.WriteLine("Erro de input");
                    break;
            }

            break;

        default:

            Console.WriteLine("Opção inválida!");
            break;
    }
} while (opcao != "5");

