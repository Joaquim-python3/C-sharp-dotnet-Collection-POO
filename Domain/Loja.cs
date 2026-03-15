using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class Loja
{
    public int id { get; set; }
    public string Nome { get; set; }

    public string Cidade { get; set; }

    public string Endereco { get; set; }

    public TimeSpan HoraAbertura { get; set; }

    public TimeSpan HoraFechamento { get; set; }

    public List<Funcionario> ListFuncionarios;

    public Loja(int id, string nome, string cidade, string endereco, TimeSpan horaAbertura, TimeSpan horaFechamento)
    {
        this.id = id;
        Nome = nome;
        Cidade = cidade;
        Endereco = endereco;
        HoraAbertura = horaAbertura;
        HoraFechamento = horaFechamento;
    }

    public Loja()
    {
    }

    public override string ToString()
    {
        return "id= "+id+" | nome= "+Nome+" | cidade= "+Cidade+" | endereco= "+Endereco+" | horaAbertura= "+HoraAbertura+" | horaFechamento= "+HoraFechamento;
    }
    
}
