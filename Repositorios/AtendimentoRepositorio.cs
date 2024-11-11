using PortifolioDEV.ORM;
using PortifolioDEV.Models;

namespace PortifolioDEV.Repositorio
{
    public class AtendimentoRepositorio
    {
        private BdPortfolioDevContext _context;
        public AtendimentoRepositorio(BdPortfolioDevContext context)
        {
            _context = context;
        }
        public void Add(AtendimentoVM atendimento)
        {

            // Cria uma nova entidade do tipo tbFuncionario a partir do objeto Funcionario recebido
            var tbAtendimento = new TbAtendimento()
            {
                DtHoraAgendamento = atendimento.DtHoraAgendamento,
                DataAtendimento = atendimento.DataAtendimento,
                Horario = atendimento.Horario
            };

            // Adiciona a entidade ao contexto
            _context.TbAtendimentos.Add(tbAtendimento);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbAtendimento = _context.TbAtendimentos.FirstOrDefault(f => f.IdAtendimento == id);

            // Verifica se a entidade foi encontrada
            if (tbAtendimento != null)
            {
                // Remove a entidade do contexto
                _context.TbAtendimentos.Remove(tbAtendimento);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }
        public List<AtendimentoVM> GetAll()
        {
            List<AtendimentoVM> listAte = new List<AtendimentoVM>();

            var listTb = _context.TbAtendimentos.ToList();

            foreach (var item in listTb)
            {
                var atendimento = new AtendimentoVM
                {
                    Id = item.IdAtendimento,
                    DtHoraAgendamento = item.DtHoraAgendamento,
                    DataAtendimento = item.DataAtendimento,
                    Horario = item.Horario

                };

                listAte.Add(atendimento);
            }

            return listAte;
        }
        public AtendimentoVM GetById(int id)
        {
            // Busca o Atendimento pelo ID no banco de dados
            var item = _context.TbAtendimentos.FirstOrDefault(c => c.IdAtendimento == id);

            // Verifica se o Atendimento foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Atendimento
            var atendimento = new AtendimentoVM
            {
                Id = item.IdAtendimento,
                DtHoraAgendamento = item.DtHoraAgendamento,
                DataAtendimento = item.DataAtendimento,
                Horario = item.Horario
            };

            return atendimento; // Retorna o cliente encontrado
        }
        public void Update(AtendimentoVM atendimento)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbAtendimento = _context.TbAtendimentos.FirstOrDefault(f => f.IdAtendimento == atendimento.Id);

            // Verifica se a entidade foi encontrada
            if (tbAtendimento != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbAtendimento.DtHoraAgendamento = atendimento.DtHoraAgendamento;
                tbAtendimento.DataAtendimento = atendimento.DataAtendimento;
                tbAtendimento.Horario = atendimento.Horario;


                // Atualiza as informações no contexto
                _context.TbAtendimentos.Update(tbAtendimento);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }
    }
}
