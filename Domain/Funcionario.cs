using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class Funcionario
{
    public int id { get; set; }

    public string Nome { get; set; }

    public List<Cargo> Cargos { get; set; }

    public decimal Salario { get; set; }
    public DateTime HoraEntrada { get; set; }

    public DateTime HoraSaida { get; set; }

    public RegimeContratual RegimeContratual { get; set; }
    public Funcionario(int id, string nome, List<Cargo> cargos, decimal salario, DateTime horaEntrada, DateTime horaSaida, RegimeContratual regimeContratual)
    {
        this.id = id;
        Nome = nome;
        Cargos = cargos;
        Salario = salario;
        HoraEntrada = horaEntrada;
        HoraSaida = horaSaida;
        RegimeContratual = regimeContratual;
    }

    public Funcionario()
    {
    }
}
