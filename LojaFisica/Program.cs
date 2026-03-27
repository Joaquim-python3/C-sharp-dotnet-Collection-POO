using LojaFisica;
using Business;
using Domain;

Database db = new Database();
LojaFisicaMenu menuLoja = new LojaFisicaMenu();
ClienteRepository repo_cliente = new ClienteRepository(db);
ProdutoRepository produtoRepo = new ProdutoRepository(db);
VendaRepository vendaRepo = new VendaRepository(db);

menuLoja.Menu(produtoRepo, vendaRepo, repo_cliente);