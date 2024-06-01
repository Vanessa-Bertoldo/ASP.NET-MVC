using Microsoft.AspNetCore.Mvc;
using web.students.Models;

namespace web.students.Controllers
{
    public class ClienteController : Controller
    {
        public IList<ClienteModel> clientes { get; set; }
        public ClienteController() 
        {
            clientes = GerarClientesMocados();
        }

        public static List<ClienteModel> GerarClientesMocados()
        {
            var clientes = new List<ClienteModel>();
            for (int i = 1; i <= 5; i++)
            {
                var cliente = new ClienteModel
                {
                    ClienteId = i,
                    Nome = "Cliente" + i,
                    Sobrenome = "Sobrenome" + i,
                    Email = "cliente" + i + "@example.com",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    Observacao = "Observação do cliente " + i,
                    RepresentanteId = i,
                    Representante = new RepresentanteModel
                    {
                        RepresentanteId = i,
                        NomeRepresentante = "Representante" + i,
                        Cpf = "00000000191"
                    }
                };
                clientes.Add(cliente);
            }
            return clientes;
        }
        public IActionResult Index()
        {
            if(clientes == null)
            {
                clientes = new List<ClienteModel>();
            }
            return View(clientes);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Console.WriteLine("EXECUTOU A ACTION CADASTRAR()");
            return View(new ClienteModel());
        }

        [HttpPost]
        public IActionResult Create(ClienteModel cliente)
        {
            Console.WriteLine("GRAVADNO O CLKIENTE");
            TempData["mensagemSucesso"] = $"O cliente {cliente.Nome} foi cadastrado com sucesso";
            return RedirectToAction(nameof(Index));

        }
    }
}
