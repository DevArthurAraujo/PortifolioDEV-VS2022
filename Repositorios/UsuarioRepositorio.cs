
using Microsoft.EntityFrameworkCore;
using PortifolioDEV.Models;
using PortifolioDEV.ORM;

namespace PortifolioDEV.Repositorios
{
    public class UsuarioRepositorio
    {

        private BdPortfolioDevContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioRepositorio(BdPortfolioDevContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public UsuarioVM VerificarLogin(string email, string senha)
        {
            // Verifica se o e-mail e a senha estão presentes no banco de dados
            var usuario = _context.TbUsuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);

            // Se encontrar o usuário, cria um objeto UsuarioVM para retornar
            if (usuario != null)
            {
                var usuarioVM = new UsuarioVM
                {
                    Id = usuario.IdUsuario,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Telefone = usuario.Telefone,
                    Senha = usuario.Senha, // Senha pode ser omitida por questões de segurança
                    TipoUsuario = usuario.TipoUsuario
                };

                // Armazenando informações do usuário na sessão
                _httpContextAccessor.HttpContext.Session.SetInt32("USUARIO_ID", usuario.IdUsuario);
                _httpContextAccessor.HttpContext.Session.SetString("USUARIO_NOME", usuario.Nome);
                _httpContextAccessor.HttpContext.Session.SetString("USUARIO_EMAIL", usuario.Email);
                _httpContextAccessor.HttpContext.Session.SetString("USUARIO_TELEFONE", usuario.Telefone);
                _httpContextAccessor.HttpContext.Session.SetString("USUARIO_TIPO", usuario.TipoUsuario.ToString());
                return usuarioVM;
            }
            
            return null; // Ou você pode lançar uma exceção, dependendo de sua estratégia
        }

        public bool InserirUsuario(string nome, string email, string telefone, string senha, int tipoUsuario)
        {
            try
            {
                TbUsuario usuario = new TbUsuario();
                usuario.Nome = nome;
                usuario.Email = email;
                usuario.Telefone = telefone;
                usuario.Senha = senha;
                usuario.TipoUsuario = tipoUsuario;

                _context.TbUsuarios.Add(usuario);  // Supondo que _context.TbUsuarios seja o DbSet para a entidade de usuários
                _context.SaveChanges();

                return true;  // Retorna true para indicar sucesso
            }
            catch (Exception ex)
            {
                // Trate o erro ou faça um log do ex.Message se necessário
                return false;  // Retorna false para indicar falha
            }
        }

        public List<UsuarioVM> ListarUsuarios()
        {
            List<UsuarioVM> listFun = new List<UsuarioVM>();

            var listTb = _context.TbUsuarios.ToList();

            foreach (var item in listTb)
            {
                var usuarios = new UsuarioVM
                {
                    Id = item.IdUsuario,
                    Nome = item.Nome,
                    Email = item.Email,
                    Telefone = item.Telefone,
                    Senha = item.Senha,
                    TipoUsuario = item.TipoUsuario,
                };

                listFun.Add(usuarios);
            }

            return listFun;
        }

        public bool AtualizarUsuario(int id, string nome, string email, string telefone, string senha, int tipoUsuario)
        {
            try
            {
                // Busca o usuário pelo ID
                var usuario = _context.TbUsuarios.FirstOrDefault(u => u.IdUsuario == id);
                if (usuario != null)
                {
                    // Atualiza os dados do usuário
                    usuario.Nome = nome;
                    usuario.Email = email;
                    usuario.Telefone = telefone;
                    usuario.Senha = senha;  // Não se esqueça de criptografar a senha antes de atualizar
                    usuario.TipoUsuario = tipoUsuario;

                    // Salva as mudanças no banco de dados
                    _context.SaveChanges();

                    return true;  // Retorna verdadeiro se a atualização for bem-sucedida
                }
                else
                {
                    return false;  // Retorna falso se o usuário não foi encontrado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o usuário com ID {id}: {ex.Message}");
                return false;
            }
        }

        public bool ExcluirUsuario(int id)
        {
            try
            {
                // Busca o usuário pelo ID
                var usuario = _context.TbUsuarios.FirstOrDefault(u => u.IdUsuario == id);

                // Se o usuário não for encontrado, lança uma exceção personalizada
                if (usuario == null)
                {
                    throw new KeyNotFoundException("Usuário não encontrado.");
                }


                // Remove o usuário do banco de dados
                _context.TbUsuarios.Remove(usuario);
                _context.SaveChanges();  // Isso pode lançar uma exceção se houver dependências

                // Se tudo correr bem, retorna true indicando sucesso
                return true;

            }
            catch (Exception ex)
            {
                // Aqui tratamos qualquer erro inesperado e logamos para depuração
                Console.WriteLine($"Erro ao excluir o usuário com ID {id}: {ex.Message}");

                // Relança a exceção para ser capturada pelo controlador
                throw new Exception($"Erro ao excluir o usuário: {ex.Message}");
            }
        }



    }
}
