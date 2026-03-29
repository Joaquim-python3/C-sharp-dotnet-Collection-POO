namespace Business.Services;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FuncionarioService
{
    public FuncionarioService()
    {
    }
    /// <summary>
    /// Função para atualizar o funcionário sem precisar instanciar outro objeto funcionário
    /// Recebe input do usuário para atualizar as informacoes
    /// Nao precisa de outro usuário
    /// </summary>
    /// <returns>Funcionario com informacoes atualizadas</returns>
    public Funcionario AtualizarFuncionario(Funcionario funcionario_antigo)
    {

        string opcao_alterar_funcionario;
        do
        {
            Console.WriteLine("Qual informação deseja alterar?");
            Console.WriteLine("1 - Nome");
            Console.WriteLine("2 - Cargos");
            Console.WriteLine("3 - Salario");
            Console.WriteLine("4 - Hora Entrada");
            Console.WriteLine("5 - Hora Saida");
            Console.WriteLine("6 - Regime Contratual");
            Console.WriteLine("0 - Finalizar");

            opcao_alterar_funcionario = Console.ReadLine();
            switch (opcao_alterar_funcionario)
            {
                case "1":
                    Console.WriteLine("Digite o novo nome: ");
                    funcionario_antigo.Nome = Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Cargos associados a:" + funcionario_antigo.Nome);
                    foreach (var cargo in funcionario_antigo.Cargos)
                    {
                        Console.WriteLine(" + " + cargo);
                    }
                    Console.WriteLine("Remover ou adicionar cargo? (1 - adicionar 2 - remover)");
                    // Adicionar lógica para adicionar ou remover cargo
                    Console.WriteLine("Cargo alterado");
                    break;
                case "3":
                    Console.WriteLine("Digite o novo salario: ");
                    funcionario_antigo.Salario = decimal.Parse(Console.ReadLine());
                    break;

                case "4":
                    Console.Write("Digite a hora de entrada (HH:mm): ");
                    string hora_entrada = Console.ReadLine();
                    funcionario_antigo.HoraEntrada = DateTime.Parse(hora_entrada);
                    break;

                case "5":
                    Console.Write("Digite a hora de saida (HH:mm): ");
                    string hora_saida = Console.ReadLine();
                    funcionario_antigo.HoraSaida = DateTime.Parse(hora_saida);
                    break;
                case "6":
                    Console.WriteLine("Digite o seu regime contratuaL: (1 - CLT\n2 - CNPJ)\n");
                    string regime_contratual = Console.ReadLine();
                    switch (regime_contratual)
                    {
                        case "1":
                            funcionario_antigo.RegimeContratual = "CLT";
                            break;

                        case "2":
                            funcionario_antigo.RegimeContratual = "CNPJ";
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Entrada inválida");
                    break;
            }

        } while (opcao_alterar_funcionario != "0");

        return funcionario_antigo;
    }

    public void AssociarFucionarioComCargos(Funcionario funcionario)
    {
        CargoRepository repo_cargo = new CargoRepository(new Database());
    }
}
