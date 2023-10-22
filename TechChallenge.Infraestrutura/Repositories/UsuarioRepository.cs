using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Usuario;
using TechChallenge.Infraestrutura.Data;

namespace TechChallenge.Infraestrutura.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    protected ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;

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

    public void Criar(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public Usuario? BuscarPorId(int id)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Id == id);
    }

    public Usuario? BuscarPorMatricula(string matricula)
    {
        matricula = matricula.ToLower();
        return _context.Usuarios.FirstOrDefault(u => u.Matricula.ToLower() == matricula);
    }

    public IList<Usuario> BuscarTodos()
    {
        return _context.Usuarios.ToList();
    }

    public IList<Usuario> BuscarPorIds(IList<int> ids)
    {
        return _context.Usuarios.Where(u => ids.Contains(u.Id)).ToList();
    }

    public IList<Usuario> BuscarPorMatriculas(IList<string> matriculas)
    {
        matriculas = matriculas.Select(m => m.ToLower()).ToList();
        return _context.Usuarios.Where(u => matriculas.Contains(u.Matricula.ToLower())).ToList();
    }

    public IList<Usuario> BuscarPorDepartamento(Departamentos departamento)
    {
        return _context.Usuarios.Where(u => u.Departamento == departamento).ToList();
    }

    public void Editar(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();
    }

    public void EditarVarios(IList<Usuario> usuarios)
    {
        _context.Usuarios.UpdateRange(usuarios);
        _context.SaveChanges();
    }

    public void Apagar(int id)
    {
        var usuario = BuscarPorId(id);
        if (usuario is not null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }

    public void ApagarVarios(IList<int> ids)
    {
        var usuarios = BuscarPorIds(ids);
        if (usuarios.Any())
        {
            _context.Usuarios.RemoveRange(usuarios);
            _context.SaveChanges();
        }
    }
}
