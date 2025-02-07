using PortifolioDEV.ORM;
using PortifolioDEV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Highsoft.Web.Mvc.Charts;

namespace PortifolioDEV.Repositorios
{
    public class DashboardRepositorio
    {

        private readonly BdPortfolioDevContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Construtor
        public DashboardRepositorio(BdPortfolioDevContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;  // Injeção de IHttpContextAccessor
        }

        public List<LineSeriesData> ObterDadosGrafico()
        {
            return new List<LineSeriesData>
        {
            new LineSeriesData { Y = 10 },
            new LineSeriesData { Y = 25 },
            new LineSeriesData { Y = 35 },
            new LineSeriesData { Y = 50 }
        };
        }

        public int ContarAgendamentosPorAno(int ano)
        {
            return _context.TbAgendamentos
                .Where(a => a.DtHoraAgendamento.Year == ano)
                .Count();
        }

    }
}
