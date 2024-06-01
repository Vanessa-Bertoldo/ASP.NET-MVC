using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using web.students.Models;

namespace web.students.Controllers
{
    public class ClienteController : Controller
    {
        public IList<ClienteModel> clientes { get; set; }
        public IList<RepresentanteModel> representantes { get; set; }
        public ClienteController() 
        {
            clientes = GerarClientesMocados();
            representantes = GerarRepresentantesMocados();
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
            Console.WriteLine("Executou a Action Cadastrar()");
            //Cria a variável para armazenar o SelectList
            var selectListRepresentantes =
                new SelectList(representantes,
                                nameof(RepresentanteModel.RepresentanteId),
                                nameof(RepresentanteModel.NomeRepresentante));
            //Adiciona o SelectList a ViewBag para se enviado para a View
            //A propriedade Representantes é criada de forma dinâmica na ViewBag
            ViewBag.Representantes = selectListRepresentantes;
            // Retorna para a View Create um 
            // objeto modelo com as propriedades em branco 
            return View(new ClienteModel());
        }
        // Anotação de uso do Verb HTTP Post
        [HttpPost]
        public IActionResult Create(ClienteModel clienteModel)
        {
            // Simila que os dados foram gravados.
            Console.WriteLine("Gravando o cliente");
            //Criando a mensagem de sucesso que será exibida para o Cliente
            TempData["mensagemSucesso"] = $"O cliente {clienteModel.Nome} foi cadastrado com suceso";
            // Substituímos o return View()
            // pelo método de redirecionamento
            return RedirectToAction(nameof(Index));
            // O trecho nameof(Index) poderia ser usado da forma abaixo
            // return RedirectToAction("Index");
        }

        public static List<RepresentanteModel> GerarRepresentantesMocados()
        {
            var representantes = new List<RepresentanteModel>
            {
                new RepresentanteModel { RepresentanteId = 1, NomeRepresentante = "Representante 1", Cpf = "111.111.111-11" },
                new RepresentanteModel { RepresentanteId = 2, NomeRepresentante = "Representante 2", Cpf = "222.222.222-22" },
                new RepresentanteModel { RepresentanteId = 3, NomeRepresentante = "Representante 3", Cpf = "333.333.333-33" },
                new RepresentanteModel { RepresentanteId = 4, NomeRepresentante = "Representante 4", Cpf = "444.444.444-44" }
            };
            return representantes;
        }
    }
}
