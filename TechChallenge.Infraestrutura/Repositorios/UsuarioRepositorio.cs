using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Infraestrutura.Repositorios;

public class UsuarioRepositorio : EFRepositorio<Usuario>, IUsuarioRepositorio
{
    public UsuarioRepositorio(ApplicationDbContext context) : base(context)
    {
        if (!_context.Usuarios.Any())
        {
            Criar(new Usuario
            {
                Matricula = "c1111",
                Nome = "Pedro",
                CodigoDoDepartamento = "10",
                NomeDoDepartamento = "Financeiro",
                Senha = "senha",
                Role = Roles.Gestor
            });
            Criar(new Usuario
            {
                Matricula = "c2222",
                Nome = "Paulo",
                CodigoDoDepartamento = "20",
                NomeDoDepartamento = "Suporte",
                Senha = "senha",
                Role = Roles.Solucionador
            });
            Criar(new Usuario
            {
                Matricula = "c3333",
                Nome = "João",
                CodigoDoDepartamento = "30",
                NomeDoDepartamento = "Desenvolvimento",
                Senha = "senha",
                Role = Roles.Solicitante
            });
        }
    }

    public Usuario? BuscarPorCodigoESenha(string matricula, string senha)
    {
        return _context.Usuarios
            .FirstOrDefault(u => u.Matricula == matricula && u.Senha == senha);
    }
}
