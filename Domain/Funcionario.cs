using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class Funcionario
{
    public int id { get; set; }
    public string Nome { get; set; }
    public List<string> Cargos { get; set; }
    public decimal Salario { get; set; }
    public DateTime HoraEntrada { get; set; }
    public DateTime HoraSaida { get; set; }
    public string RegimeContratual { get; set; }
    public int LojaId {get; set;}
    public Funcionario(int id, string nome, List<string> cargos, decimal salario, DateTime horaEntrada, DateTime horaSaida, string regimeContratual)
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
    
    public override string ToString()
    {
        return $"Id: {id} | " +
           $"Nome: {Nome} | " +
           $"Cargos: {Cargos} | " +
           $"Salário: {Salario} | " +
           $"Entrada: {HoraEntrada} | " +
           $"Saída: {HoraSaida} | " +
           $"Regime: {RegimeContratual}"+
           $"LojaId: {LojaId} | ";
    }
}
