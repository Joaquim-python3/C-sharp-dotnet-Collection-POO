namespace LojaFisica;

using Domain;
using Business;

public class LojaFisicaMenu
{
    public void Menu(
        ProdutoRepository produtoRepo,
        VendaRepository vendaRepo,
        ClienteRepository clienteRepo
    )
    {
        int opcao = -1;

        while (opcao != 0)
        {
            Console.WriteLine("\n===== LOJA FÍSICA =====");
            Console.WriteLine("1 - Realizar venda");
            Console.WriteLine("2 - Cadastrar cliente");
            Console.WriteLine("0 - Sair");

            Console.Write("Escolha: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    RealizarVenda(produtoRepo, vendaRepo);
                    break;

                case 2:
                    CadastrarCliente(clienteRepo);
                    break;

                case 0:
                    Console.WriteLine("Saindo...");
                    break;
            }
        }
    }

    void RealizarVenda(ProdutoRepository produtoRepo, VendaRepository vendaRepo)
    {
        Console.WriteLine("\n--- NOVA VENDA ---");

        Console.Write("ID do funcionário: ");
        int funcionarioId = int.Parse(Console.ReadLine());

        Console.Write("ID da loja: ");
        int lojaId = int.Parse(Console.ReadLine());

        Console.Write("ID do cliente (ou 0 para nenhum): ");
        int clienteInput = int.Parse(Console.ReadLine());

        int? clienteId = clienteInput == 0 ? null : clienteInput;

        var itens = new List<(int produtoId, decimal quantidade)>();

        string continuar;

        do
        {
            Console.WriteLine("\n--- PRODUTOS ---");
            produtoRepo.ListarProdutos();

            Console.Write("ID do produto: ");
            int produtoId = int.Parse(Console.ReadLine());

            Console.Write("Quantidade: ");
            decimal qtd = decimal.Parse(Console.ReadLine());

            itens.Add((produtoId, qtd));

            Console.Write("Adicionar outro produto? (s/n): ");
            continuar = Console.ReadLine().ToLower();

        } while (continuar == "s");

        Console.WriteLine("\nForma de pagamento:");
        Console.WriteLine("1 - Crédito");
        Console.WriteLine("2 - Débito");
        Console.WriteLine("3 - Dinheiro");

        int opPag = int.Parse(Console.ReadLine());

        string formaPagamento = opPag switch
        {
            1 => "credito",
            2 => "debito",
            3 => "dinheiro",
            _ => throw new Exception("Pagamento inválido")
        };

        vendaRepo.FinalizarVenda(
            clienteId,
            funcionarioId,
            lojaId,
            "fisica",
            itens,
            formaPagamento
        );

        Console.WriteLine("Venda concluída!");
    }

    void CadastrarCliente(ClienteRepository repo)
    {
        Cliente c = new Cliente();

        Console.Write("Nome: ");
        c.Nome = Console.ReadLine();

        Console.Write("Email: ");
        c.Email = Console.ReadLine();

        Console.Write("Login: ");
        c.Login = Console.ReadLine();

        Console.Write("Senha: ");
        c.Senha = Console.ReadLine();

        repo.CriarCliente(c);

        Console.WriteLine("Cliente cadastrado!");
    }

}    