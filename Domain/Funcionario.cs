using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class Funcionario
{


    public int id { get; set; }

    public string Nome { get; set; }

    public Cargo Cargo { get; set; }

    public decimal Salario { get; set; }
    public DateTime HoraEntrada { get; set; }

    public DateTime HoraSaida { get; set; }

    public RegimeContratual RegimeContratual { get; set; }
    public Funcionario(int id, string nome, Cargo cargo, decimal salario, DateTime horaEntrada, DateTime horaSaida, RegimeContratual regimeContratual)
    {
        this.id = id;
        Nome = nome;
        Cargo = cargo;
        Salario = salario;
        HoraEntrada = horaEntrada;
        HoraSaida = horaSaida;
        RegimeContratual = regimeContratual;
    }

    public Funcionario()
    {
    }
}
