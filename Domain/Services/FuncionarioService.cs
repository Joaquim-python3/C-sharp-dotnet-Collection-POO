using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services;

public class FuncionarioService
{
    /// <summary>
    /// Função para atualizar o funcionário sem precisar instanciar outro objeto funcionário
    /// Recebe input do usuário para atualizar as informacoes
    /// Nao precisa de outro usuário
    /// </summary>
    /// <returns>Funcionario com informacoes atualizadas</returns>
    public Funcionario AtualizarFuncionario(Funcionario funcionario_antigo)
    {
        /*string nome = funcionario_antigo.Nome;
        string cargos = String.Join(" , ", funcionario_antigo.Cargos);
        decimal salario = funcionario_antigo.Salario;
        DateTime hora_entrada = funcionario_antigo.HoraEntrada;
        DateTime hora_saida = funcionario_antigo.HoraSaida;
        string regime_contratual = funcionario_antigo.RegimeContratual;*/

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
            opcao_alterar_funcionario = (Console.ReadLine();
            
            switch (opcao_alterar_funcionario)
            {
                case "1":
                    Console.WriteLine("Digite o novo nome: ");
                    funcionario_antigo.Nome = Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Cargos associados a:" +funcionario_antigo.Nome);
                    foreach (var cargo in funcionario_antigo.Cargos)
                    {
                        Console.WriteLine(" + "+ cargo);
                    }
                    Console.WriteLine("Remover ou adicionar cargo? (1 - adicionar 2 - remover)");
                    // Adicionar lógica para adicionar ou remover cargo
                    Console.WriteLine("Cargo alterado");
                    break;
                case "3":
                    break;
            }

        } while (opcao_alterar_funcionario != "0");

        return funcionario_antigo;
    }
}
