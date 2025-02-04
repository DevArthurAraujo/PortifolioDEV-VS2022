using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortifolioDEV.Models;
using PortifolioDEV.Repositorios;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PortifolioDEV.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly RelatorioRepositorio _relatorioRepositorio;
        private readonly ILogger<RelatorioController> _logger;
        public RelatorioController(RelatorioRepositorio relatorioRepositorio, ILogger<RelatorioController> logger)
        {
            _relatorioRepositorio = relatorioRepositorio;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
