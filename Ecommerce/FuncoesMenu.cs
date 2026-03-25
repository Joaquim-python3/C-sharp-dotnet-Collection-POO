namespace Ecommerce;

using Business;
using Domain;

public class FuncoesMenu
{
    public void MenuCliente(Cliente cliente, CarrinhoRepository carrinhoRepo, ProdutoRepository produtoRepo)
    {
        var carrinho = carrinhoRepo.ObterOuCriarCarrinho(cliente.id);

        int opcao = -1;

        while (opcao != 0)
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1 - Adicionar produto ao carrinho");
            Console.WriteLine("2 - Ver carrinho");
            Console.WriteLine("3 - Remover item");
            Console.WriteLine("0 - Sair");

            Console.Write("Escolha: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    AdicionarProdutoMenu(carrinhoRepo, carrinho.id, produtoRepo);
                    break;

                case 2:
                    MostrarCarrinhoMenu(carrinhoRepo, carrinho.id);
                    break;

                case 3:
                    RemoverItemMenu(carrinhoRepo, carrinho.id);
                    break;

                case 0:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    void AdicionarProdutoMenu(CarrinhoRepository repo, int carrinhoId, ProdutoRepository produtoRepo)
    {
        Console.WriteLine("\n--- PRODUTOS DISPONÍVEIS ---");
        produtoRepo.ListarProdutos();

        Console.Write("ID do produto: ");
        int produtoId = int.Parse(Console.ReadLine());

        Console.Write("Quantidade: ");
        decimal qtd = decimal.Parse(Console.ReadLine());

        repo.AdicionarProduto(carrinhoId, produtoId, qtd);

        Console.WriteLine("Produto adicionado!");
    }

    void MostrarCarrinhoMenu(CarrinhoRepository repo, int carrinhoId)
    {
        var itens = repo.ListarItens(carrinhoId);

        Console.WriteLine("\n--- Carrinho ---");

        foreach (var item in itens)
        {
            Console.WriteLine($"{item.NomeProduto} | Qtd: {item.Quantidade} | Preço da Unidade: {item.Preco}R$ | Total: {item.Preco * item.Quantidade}");
        }
    }

    void RemoverItemMenu(CarrinhoRepository repo, int carrinhoId)
    {
        
        Console.Write("ID do produto para remover: ");
        int produtoId = int.Parse(Console.ReadLine());

        repo.RemoverItem(carrinhoId, produtoId);

        Console.WriteLine("Item removido!");
    }


}