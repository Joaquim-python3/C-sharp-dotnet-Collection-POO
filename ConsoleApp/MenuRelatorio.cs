namespace ConsoleApp;

using Business;
using Domain;

public class MenuRelatorio
{
    public void MenuRelatorioFunction(VendaRepository vendaRepo)
    {
        Console.WriteLine("\n--- RELATÓRIO DE VENDAS ---");

        Console.Write("Data início (yyyy-mm-dd): ");
        DateTime dataInicio = DateTime.Parse(Console.ReadLine());

        Console.Write("Data fim (yyyy-mm-dd): ");
        DateTime dataFim = DateTime.Parse(Console.ReadLine());

        Console.Write("Filtrar por loja? (id ou 0 para todos): ");
        int lojaInput = int.Parse(Console.ReadLine());
        int? lojaId = lojaInput == 0 ? null : lojaInput;

        Console.WriteLine("Tipo de venda:");
        Console.WriteLine("1 - Física");
        Console.WriteLine("2 - Online");
        Console.WriteLine("0 - Todos");

        int tipoInput = int.Parse(Console.ReadLine());

        string? tipoVenda = tipoInput switch
        {
            1 => "fisica",
            2 => "online",
            _ => null
        };

        Console.Write("Filtrar por funcionário? (id ou 0 para ignorar): ");
        int funcInput = int.Parse(Console.ReadLine());
        int? funcionarioId = funcInput == 0 ? null : funcInput;

        vendaRepo.RelatorioVendas(
            dataInicio,
            dataFim,
            lojaId,
            tipoVenda,
            funcionarioId
        );
    }
}