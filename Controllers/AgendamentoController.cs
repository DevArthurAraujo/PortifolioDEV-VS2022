using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortifolioDEV.Models;
using PortifolioDEV.Repositorio;
using PortifolioDEV.Repositorios;
using System.Diagnostics;

namespace PortifolioDEV.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly AgendamentoRepositorio _agendamentoRepositorio;
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private readonly ServicoRepositorio _servicoRepositorio;

        public AgendamentoController(AgendamentoRepositorio agendamentoRepositorio, UsuarioRepositorio usuarioRepositorio, ServicoRepositorio servicoRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _servicoRepositorio = servicoRepositorio;
        }

        public IActionResult Index()
        {
            // Buscar os usuários e serviços no banco de dados
            var usuarios = _usuarioRepositorio.ListarUsuarios();
            var servicos = _servicoRepositorio.ListarServicos();

            // Preencher as listas para os dropdowns
            List<SelectListItem> idUsuario = usuarios.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Nome
            }).ToList();

            List<SelectListItem> idServico = servicos.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.TipoServico
            }).ToList();

            ViewBag.lstIdUsuario = new SelectList(idUsuario, "Value", "Text");
            ViewBag.lstIdServico = new SelectList(idServico, "Value", "Text");

            // Buscar os agendamentos e incluir os nomes de Usuário e Serviço
            var agendamentos = _agendamentoRepositorio.ListarAgendamentos();

            return View(agendamentos);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Gerencimento_Agendamento_Usuario()
        {
            return View();
        }

        public IActionResult Create()
        {
            // Recarrega as listas de usuários e serviços
            var usuarios = _usuarioRepositorio.ListarUsuarios();
            var servicos = _servicoRepositorio.ListarServicos();

            List<SelectListItem> idUsuario = usuarios.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Nome
            }).ToList();

            List<SelectListItem> idServico = servicos.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.TipoServico
            }).ToList();

            ViewBag.lstIdUsuario = new SelectList(idUsuario, "Value", "Text");
            ViewBag.lstIdServico = new SelectList(idServico, "Value", "Text");

            return View();
        }

        public IActionResult ConsultarAgendamento(string data)
        {

            var agendamento = _agendamentoRepositorio.ConsultarAgendamento(data);

            if (agendamento != null)
            {
                return Json(agendamento);
            }
            else
            {
                return NotFound();
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
