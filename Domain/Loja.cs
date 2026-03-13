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

    public DateTime HoraAbertura { get; set; }

    public DateTime HoraFechamento { get; set; }

    public List<Funcionario> ListFuncionarios;

    public Loja(int id, string nome, string cidade, string endereco, DateTime horaAbertura, DateTime horaFechamento)
    {
        this.id = id;
        Nome = nome;
        Cidade = cidade;
        Endereco = endereco;
        HoraAbertura = horaAbertura;
        HoraFechamento = horaFechamento;
        ListFuncionarios = null;
    }

    public Loja()
    {
    }
}
