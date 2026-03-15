using Business;
using Domain;
using Org.BouncyCastle.Asn1.Cms;
using System;

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
            Console.WriteLine("Criando lojas!");
            Loja loja_aracati = new Loja(1, "Aracati", "Aracati", "Rua centro nº123", new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0));
            Loja loja_russas = new Loja(1, "Russas", "Russas", "Rua Central nº1010", new TimeSpan(9,0,0), new TimeSpan(18,0,0));
            Console.WriteLine(loja_aracati.ToString());
            Console.WriteLine(loja_russas.ToString());
            break;

        case "6":
            Console.WriteLine("Iniciando compras!");
            
            break;

        case "7":
            Console.WriteLine("Finalizando!");
            break;

        default:

            Console.WriteLine("Opção inválida!");
            break;
    }
} while (opcao != "7");



