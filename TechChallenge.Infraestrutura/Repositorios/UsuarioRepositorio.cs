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
                Departamento = "Financeiro",
                Senha = "senha",
                Funcao = Funcoes.Gestor
            });
            Criar(new Usuario
            {
                Matricula = "c2222",
                Nome = "Paulo",
                Departamento = "Suporte",
                Senha = "senha",
                Funcao = Funcoes.Solucionador
            });
            Criar(new Usuario
            {
                Matricula = "c3333",
                Nome = "João",
                Departamento = "Desenvolvimento",
                Senha = "senha",
                Funcao = Funcoes.Solicitante
            });
        }
    }

    public Usuario? BuscarPorCodigoESenha(string matricula, string senha)
    {
        return _context.Usuarios
            .FirstOrDefault(u => u.Matricula == matricula && u.Senha == senha);
    }
}
