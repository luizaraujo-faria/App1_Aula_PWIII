using Microsoft.AspNetCore.Mvc;
using PrimeiroApp.Models;
using PrimeiroApp.Repositories;
using PrimeiroApp.Repositories.Contracts;

namespace PrimeiroApp.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _repository;
        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.ObterUsuarios());
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _repository.Cadastrar(usuario);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AtualizarUsuario(int id)
        {
            return View(_repository.ObterUsuario(id));
        }
        [HttpPost]
        public IActionResult AtualizarUsuario(Usuario usuario)
        {
            _repository.Atualizar(usuario);

            return RedirectToAction(nameof(Index));
        }
    }
}
