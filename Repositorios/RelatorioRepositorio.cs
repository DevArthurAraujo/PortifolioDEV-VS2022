using PortifolioDEV.ORM;
using PortifolioDEV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace PortifolioDEV.Repositorios
{
    public class RelatorioRepositorio
    {

        private readonly BdPortfolioDevContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Construtor
        public RelatorioRepositorio(BdPortfolioDevContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;  // Injeção de IHttpContextAccessor
        }

    }
}
