using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.students.Data.Contexts;
using web.students.Models;

namespace web.students.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DatabaseContext _context;
        public IList<ClienteModel> clientes { get; set; }
        public IList<RepresentanteModel> representantes { get; set; }
        public ClienteController(DatabaseContext context) 
        {
            _context = context;
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
     

        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Simulando a busca no banco de dados 
            var clienteConsultado =
                clientes.Where(c => c.ClienteId == id).FirstOrDefault();
            if (clienteConsultado != null)
            {
                TempData["mensagemSucesso"] = $"Os dados do cliente {clienteConsultado.Nome} foram removidos com sucesso";
            }
            else
            {
                TempData["mensagemSucesso"] = $"OPS !!! Cliente inexistente.";
            }
            return RedirectToAction(nameof(Index));
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

        [HttpPost]
        public IActionResult create(ClienteModel cliente)
        {
            _context.Cliente.Add(cliente);
            _context.SaveChanges();
            TempData["mensagemSucesso"] = $"O cliente {cliente.Nome} foi cadastrado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EditCliente(ClienteModel clienteModel)
        {
            _context.Update(clienteModel);
            _context.SaveChanges();
            TempData["mensagemSucesso"] = $"Os dados do cliente {clienteModel.Nome} foram alterados com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DeleteCliente(int id)
        {
            var cliente = _context.Cliente.Find(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = $"Os dados do cliente {cliente.Nome} foram removidos com sucesso";
            }
            else
            {
                TempData["mensagemSucesso"] = "OPS !!! Cliente inexistente.";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var clientes = _context.Cliente.Include(c => c.Representante).ToList();
            return View(clientes);
        }

        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cliente = _context.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.Representantes =
                    new SelectList(_context.Representantes.ToList(),
                                    "RepresentanteId",
                                    "NomeRepresentante",
                                    cliente.RepresentanteId);
                return View(cliente);
            }
        }

        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public IActionResult Detail(int id)
        {
            // Usando o método Include para carregar o representante associado
            var cliente = _context.Cliente
                            .Include(c => c.Representante) // Carrega o representante junto com o cliente
                            .FirstOrDefault(c => c.ClienteId == id); // Encontra o cliente pelo id
            if (cliente == null)
            {
                return NotFound(); // Retorna um erro 404 se o cliente não for encontrado
            }
            else
            {
                return View(cliente); // Retorna a view com os dados do cliente e seu representante
            }
        }

        //public IActionResult Index()
        //{
        //    if(clientes == null)
        //    {
        //        clientes = new List<ClienteModel>();
        //    }
        //    return View(clientes);
        //}
        // Anotação de uso do Verb HTTP Get
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var selectListRepresentantes =
        //        new SelectList(representantes,
        //                        nameof(RepresentanteModel.RepresentanteId),
        //                        nameof(RepresentanteModel.NomeRepresentante));
        //    ViewBag.Representantes = selectListRepresentantes;
        //    // Simulando a busca no banco de dados 
        //    var clienteConsultado =
        //        clientes.Where(c => c.ClienteId == id).FirstOrDefault();
        //    // Retornando o cliente consultado para a View
        //    return View(clienteConsultado);
        //}
        [HttpPost]
        public IActionResult Edit(ClienteModel clienteModel)
        {
            TempData["mensagemSucesso"] = $"Os dados do cliente {clienteModel.Nome} foram alterados com suceso";
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //public IActionResult Detail(int id)
        //{
        //    var selectListRepresentantes =
        //        new SelectList(representantes,
        //                        nameof(RepresentanteModel.RepresentanteId),
        //                        nameof(RepresentanteModel.NomeRepresentante));
        //    ViewBag.Representantes = selectListRepresentantes;
        //    // Simulando a busca no banco de dados 
        //    var clienteConsultado =
        //        clientes.Where(c => c.ClienteId == id).FirstOrDefault();
        //    // Retornando o cliente consultado para a View
        //    return View(clienteConsultado);
        //}
    }
}
