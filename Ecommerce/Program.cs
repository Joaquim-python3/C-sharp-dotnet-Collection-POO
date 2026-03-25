using Business;
using Domain;
using System;

Cliente c = new Cliente();
Database db = new Database();
ClienteRepository repo_cliente = new ClienteRepository(db);

Console.WriteLine("=============== BEM VINDO AO ECOMMERCE ===============");

Console.Write("Deseja fazer Login (1) ou Cadastrar-se (2)? ");
int opcao = int.Parse(Console.ReadLine());
switch (opcao)
{
    case 1:
        Console.WriteLine("-> Iniciando Login <-");
        Console.Write("Digite seu email: ");
        c.Email = Console.ReadLine();

        Console.Write("Digite sua senha: ");
        c.Senha = Console.ReadLine();

        var verificar = repo_cliente.Login(c.Email,c.Senha);

        if(verificar == true)
        {
            Console.WriteLine("Você está logado!");
        }
        else
        {
            Console.WriteLine("Email ou senha inválidos.");
        }

    break;

    case 2:
        Console.WriteLine("-> Iniciando Cadastro <-");
        Console.Write("Digite seu Nome: ");
        c.Nome = Console.ReadLine();

        Console.Write("Digite seu email: ");
        c.Email = Console.ReadLine();

        Console.Write("Digite seu Login: ");
        c.Login = Console.ReadLine();        

        Console.Write("Digite sua senha: ");
        c.Senha = Console.ReadLine();

        repo_cliente.CriarCliente(c);

    break;

}

