using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain;

public class Cliente
{
    public int id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Login { get; set;}
    public string Senha { get; set;}

    public Cliente() { }

    public Cliente(int id, string nome, string email, string login, string senha)
    {
        this.id = id;
        Nome = nome;
        Email = email;
        Login = login;
        Senha = senha;
    }

    public Cliente(string nome, string email, string login, string senha)
    {
        Nome = nome;
        Email = email;
        Login = login;
        Senha = senha;
    }

}
