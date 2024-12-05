using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortifolioDEV.Models;
using PortifolioDEV.ORM;
using PortifolioDEV.Repositorio;
using PortifolioDEV.Repositorios;
using System.Diagnostics;

namespace PortifolioDEV.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly AgendamentoRepositorio _agendamentoRepositorio;        
        private BdPortfolioDevContext _context;

        public AgendamentoController(AgendamentoRepositorio agendamentoRepositorio, BdPortfolioDevContext context)
        {
            _agendamentoRepositorio = agendamentoRepositorio;           
            _context = context;
        }

        public IActionResult Index()
        {
            var servicos = new ServicoRepositorio(_context);
            var nomeServicos = servicos.ListarNomesServicos();
            if (nomeServicos != null && nomeServicos.Any())
            {
                // Cria a lista de SelectListItem
                var selectList = nomeServicos.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),  // O valor do item será o ID do usuário
                    Text = u.TipoServico             // O texto exibido será o nome do usuário
                }).ToList();

                // Passa a lista para o ViewBag para ser utilizada na view
                ViewBag.lstTipoServico = selectList;
            }

            // Buscar os usuários e serviços no banco de dados
            var usuarios = _agendamentoRepositorio.ListarNomesAgendamentos();
            if (usuarios != null && usuarios.Any())
            {
                // Cria a lista de SelectListItem
                var selectList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),  // O valor do item será o ID do usuário
                    Text = u.Nome             // O texto exibido será o nome do usuário
                }).ToList();

                // Passa a lista para o ViewBag para ser utilizada na view
                ViewBag.Usuarios = selectList;
            }
            
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

        public IActionResult InserirAgendamento(DateTime dtHoraAgendamento, DateOnly dataAtendimento, TimeOnly horario, int IdUsuario, int IdServico)
        {
            try
            {
                // Chama o método do repositório que realiza a inserção no banco de dados
                var resultado = _agendamentoRepositorio.InserirAgendamento(dtHoraAgendamento, dataAtendimento, horario, IdUsuario, IdServico);

                // Verifica o resultado da inserção
                if (resultado)
                {
                    return Json(new { success = true, message = "Atendimento inserido com sucesso!" });
                }
                else
                {
                    return Json(new { success = false, message = "Erro ao inserir o atendimento. Tente novamente." });
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro inesperado, captura e exibe o erro
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
