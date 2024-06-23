﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.students.Data.Contexts;
using web.students.Models;
using web.students.ViweModels;

namespace web.students.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DatabaseContext _context;

        public ClienteController(DatabaseContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            // O método Include será explicado posteriomente
            var clientes = _context.Clientes.Include(c => c.Representante).ToList();
            return View(clientes);
        }


        //[HttpGet]
        //public IActionResult Create()
        //{
        //    ViewBag.Representantes =
        //        new SelectList(_context.Representantes.ToList()
        //                        , "RepresentanteId"
        //                        , "NomeRepresentante");
        //    return View();
        //}



        //// Anotação de uso do Verb HTTP Post
        //[HttpPost]
        //public IActionResult Create(ClienteModel clienteModel)
        //{
        //    _context.Clientes.Add(clienteModel);
        //    _context.SaveChanges();
        //    TempData["mensagemSucesso"] = $"O cliente {clienteModel.Nome} foi cadastrado com sucesso";
        //    return RedirectToAction(nameof(Index));
        //}


        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public IActionResult Detail(int id)
        {
            // Usando o método Include para carregar o representante associado
            var cliente = _context.Clientes
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




        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cliente = _context.Clientes.Find(id);
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


        [HttpPost]
        public IActionResult Edit(ClienteModel clienteModel)
        {
            _context.Update(clienteModel);
            _context.SaveChanges();
            TempData["mensagemSucesso"] = $"Os dados do cliente {clienteModel.Nome} foram alterados com sucesso";
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = $"Os dados do cliente {cliente.Nome} foram removidos com sucesso";
            }
            else
            {
                TempData["mensagemSucesso"] = "OPS !!! Cliente inexistente.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new ClienteCreateViewModel
            {
                Representantes = new SelectList(_context.Representantes.ToList(), "RepresentanteId", "NomeRepresentante")
            };
            return View(viewModel);
        }
        //[HttpPost]
        //public IActionResult Create(ClienteCreateViewModel viewModel)
        //{
        //    var clienteModel = new ClienteModel
        //    {
        //        ClienteId = viewModel.ClienteId,
        //        Nome = viewModel.Nome,
        //        Sobrenome = viewModel.Sobrenome,
        //        Email = viewModel.Email,
        //        DataNascimento = viewModel.DataNascimento,
        //        Observacao = viewModel.Observacao,
        //        RepresentanteId = viewModel.RepresentanteId
        //    };
        //    _context.Clientes.Add(clienteModel);
        //    _context.SaveChanges();
        //    TempData["mensagemSucesso"] = $"O cliente {clienteModel.Nome} foi cadastrado com sucesso";
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult Create(ClienteCreateViewModel viewModel)
        {
            // Verifica se todos os dados enviados estão válidos conforme as regras definidas no ViewModel
            if (ModelState.IsValid)
            {
                var cliente = new ClienteModel
                {
                    ClienteId = viewModel.ClienteId,
                    Nome = viewModel.Nome,
                    Sobrenome = viewModel.Sobrenome,
                    Email = viewModel.Email,
                    DataNascimento = viewModel.DataNascimento,
                    Observacao = viewModel.Observacao,
                    RepresentanteId = viewModel.RepresentanteId
                };
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = $"O cliente {viewModel.Nome} foi cadastrado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Se os dados não estão válidos, recarrega a lista de representantes para a seleção na View
                viewModel.Representantes = new SelectList(_context.Representantes.ToList(), "RepresentanteId", "NomeRepresentante", viewModel.RepresentanteId);
                // Retorna a View com o ViewModel contendo os dados submetidos e os erros de validação
                return View(viewModel);
            }
        }
    }
}
