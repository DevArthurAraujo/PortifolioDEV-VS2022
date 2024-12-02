using PortifolioDEV.ORM;
using PortifolioDEV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace PortifolioDEV.Repositorio
{
    public class AgendamentoRepositorio
    {
        private BdPortfolioDevContext _context;
        public AgendamentoRepositorio(BdPortfolioDevContext context)
        {
            _context = context;
        }

        public void Add(AgendamentoVM agendamento)
        {
            var tbAgendamento = new TbAgendamento()
            {
                DtHoraAgendamento = agendamento.DtHoraAgendamento,
                DataAtendimento = agendamento.DataAtendimento,
                Horario = agendamento.Horario,
                IdUsuario = agendamento.IdUsuario,  // Vincula o usuário
                IdServico = agendamento.IdServico   // Vincula o serviço
            };

            _context.TbAgendamentos.Add(tbAgendamento);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tbAgendamento = _context.TbAgendamentos.FirstOrDefault(f => f.IdAgendamento == id);
            if (tbAgendamento != null)
            {
                _context.TbAgendamentos.Remove(tbAgendamento);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Agendamento não encontrado.");
            }
        }

        public List<AgendamentoVM> ListarAgendamentos()
        {
            var listAte = new List<AgendamentoVM>();

            // Inclui a navegação para carregar dados do usuário (e-mail, telefone)
            var listTb = _context.TbAgendamentos
                .Include(a => a.IdUsuarioNavigation)  // Inclui a navegação para o usuário
                .ToList();

            foreach (var item in listTb)
            {
                var agendamento = new AgendamentoVM
                {
                    Id = item.IdAgendamento,
                    DtHoraAgendamento = item.DtHoraAgendamento,
                    DataAtendimento = item.DataAtendimento,
                    Horario = item.Horario,
                    // Dados do Usuário pela navegação
                    UsuarioNome = item.IdUsuarioNavigation.Nome,   // Nome do usuário
                    UsuarioEmail = item.IdUsuarioNavigation.Email, // E-mail do usuário
                    UsuarioTelefone = item.IdUsuarioNavigation.Telefone, // Telefone do usuário
                    ServicoNome = item.IdServicoNavigation.TipoServico,   // Nome do serviço
                    ServicoValor = item.IdServicoNavigation.Valor  // Valor do serviço
                };

                listAte.Add(agendamento);
            }

            return listAte;
        }

        public void Update(AgendamentoVM agendamento)
        {
            var tbAgendamento = _context.TbAgendamentos.FirstOrDefault(f => f.IdAgendamento == agendamento.Id);
            if (tbAgendamento != null)
            {
                tbAgendamento.DtHoraAgendamento = agendamento.DtHoraAgendamento;
                tbAgendamento.DataAtendimento = agendamento.DataAtendimento;
                tbAgendamento.Horario = agendamento.Horario;
                tbAgendamento.IdUsuario = agendamento.IdUsuario;
                tbAgendamento.IdServico = agendamento.IdServico;

                _context.TbAgendamentos.Update(tbAgendamento);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Agendamento não encontrado.");
            }
        }

        public List<AgendamentoVM> ConsultarAgendamento(string datap)
        {
            DateOnly data = DateOnly.ParseExact(datap, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string dataFormatada = data.ToString("yyyy-MM-dd"); // Formato desejado: "yyyy-MM-dd"

            try
            {
                // Consulta ao banco de dados, convertendo para o tipo AtendimentoVM
                var ListaAgendamento = _context.TbAgendamentos
                    .Where(a => a.DataAtendimento == DateOnly.Parse(dataFormatada))
                    .Select(a => new AgendamentoVM
                    {
                        // Mapear as propriedades de TbAtendimento para AtendimentoVM
                        // Suponha que TbAtendimento tenha as propriedades Id, DataAtendimento, e outras:
                        Id = a.IdAgendamento,
                        DtHoraAgendamento = a.DtHoraAgendamento,
                        DataAtendimento = DateOnly.Parse(dataFormatada),
                        Horario = a.Horario,
                        IdUsuario = a.IdUsuario,
                        IdServico = a.IdServico
                    })
                    .ToList(); // Converte para uma lista

                return ListaAgendamento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar agendamentos: {ex.Message}");
                return new List<AgendamentoVM>(); // Retorna uma lista vazia em caso de erro
            }
        }
    }
}
