using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Infraestrutura.Repositories;

public class UsuarioRepository : EntidadeRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context)
    {
        if (!_context.Usuarios.Any())
        {
            Criar(new Usuario
            {
                Matricula = "c1111",
                Nome = "Pedro",
                Departamento = Departamentos.Financeiro,
                Funcao = Funcoes.Gestor
            });
            Criar(new Usuario
            {
                Matricula = "c1112",
                Nome = "Tiago",
                Departamento = Departamentos.Financeiro,
                Funcao = Funcoes.Solicitante
            });
            Criar(new Usuario
            {
                Matricula = "c1113",
                Nome = "André",
                Departamento = Departamentos.Financeiro,
                Funcao = Funcoes.Solicitante
            });
            Criar(new Usuario
            {
                Matricula = "c2222",
                Nome = "Paulo",
                Departamento = Departamentos.SuporteTecnologico,
                Funcao = Funcoes.Solucionador
            });
            Criar(new Usuario
            {
                Matricula = "c3333",
                Nome = "João",
                Departamento = Departamentos.Desenvolvimento,
                Funcao = Funcoes.Solicitante
            });
        }
    }

    public Usuario? BuscarUsuarioPorMatricula(string matricula)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Matricula.ToLower() == matricula.ToLower());
    }

    public IList<Usuario> BuscarUsuariosPorIds(IList<long> idsDosUsuarios)
    {
        return _context.Usuarios.Where(u => idsDosUsuarios.Contains(u.Id)).ToList();
    }

    public IList<Usuario> BuscarUsuariosPorDepartamento(Departamentos departamento)
    {
        return _context.Usuarios.Where(u => u.Departamento == departamento).ToList();
    }

    public void DefinirGestores(IList<Usuario> usuarios)
    {
        _context.Usuarios.UpdateRange(usuarios);
        _context.SaveChanges();
    }
}
