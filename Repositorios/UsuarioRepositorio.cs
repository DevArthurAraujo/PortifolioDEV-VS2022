using PortifolioDEV.Models;
using PortifolioDEV.ORM;

namespace PortifolioDEV.Repositorios
{
    public class UsuarioRepositorio
    {
        private BdPortfolioDevContext _context;
        public UsuarioRepositorio(BdPortfolioDevContext context)
        {
            _context = context;
        }
        public void Add(UsuarioVM usuario)
        {

            // Cria uma nova entidade do tipo tbUsuario a partir do objeto Usuario recebido
            var tbUsuario = new TbUsuario()
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Senha = usuario.Senha,
                TipoUsuario = usuario.TipoUsuario
            };

            // Adiciona a entidade ao contexto
            _context.TbUsuarios.Add(tbUsuario);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbUsuario = _context.TbUsuarios.FirstOrDefault(f => f.IdUsuario == id);

            // Verifica se a entidade foi encontrada
            if (tbUsuario != null)
            {
                // Remove a entidade do contexto
                _context.TbUsuarios.Remove(tbUsuario);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Usuario não encontrado.");
            }
        }
        public List<UsuarioVM> GetAll()
        {
            List<UsuarioVM> listUsu = new List<UsuarioVM>();

            var listTb = _context.TbUsuarios.ToList();

            foreach (var item in listTb)
            {
                var usuario = new UsuarioVM
                {
                    Id = item.IdUsuario,
                    Nome = item.Nome,
                    Email = item.Email,
                    Telefone = item.Telefone,
                    Senha = item.Senha,
                    TipoUsuario = item.TipoUsuario

                };

                listUsu.Add(usuario);
            }

            return listUsu;
        }
        public UsuarioVM GetById(int id)
        {
            // Busca o Usuario pelo ID no banco de dados
            var item = _context.TbUsuarios.FirstOrDefault(c => c.IdUsuario == id);

            // Verifica se o Usuario foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Usuario
            var usuario = new UsuarioVM
            {
                Id = item.IdUsuario,
                Nome = item.Nome,
                Email = item.Email,
                Telefone = item.Telefone,
                Senha = item.Senha,
                TipoUsuario = item.TipoUsuario
            };

            return usuario; // Retorna o cliente encontrado
        }
        public void Update(UsuarioVM usuario)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbUsuario = _context.TbUsuarios.FirstOrDefault(f => f.IdUsuario == usuario.Id);

            // Verifica se a entidade foi encontrada
            if (tbUsuario != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Usuario recebido
                tbUsuario.Nome = usuario.Nome;
                tbUsuario.Senha = usuario.Senha;
                tbUsuario.Email = usuario.Email;
                tbUsuario.Telefone = usuario.Telefone;
                tbUsuario.TipoUsuario = usuario.TipoUsuario;


                // Atualiza as informações no contexto
                _context.TbUsuarios.Update(tbUsuario);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

        public TbUsuario GetByCredentials(string usuario, string senha)
        {
            // Aqui você deve usar a lógica de hash para comparar a senha
            return _context.TbUsuarios.FirstOrDefault(u => u.Email == usuario && u.Senha == senha);
        }
    }
}
