using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Aplicacao.Commands;
using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Exceptions;

namespace TechChallenge.Aplicacao.Controllers;

[ApiController]
[Route("[controller]")]
public class AtividadeController : BaseController
{
    private readonly AtividadeCommand _comandos;

    public AtividadeController(AtividadeCommand comandos)
    {
        _comandos = comandos;
    }

    [HttpGet("ativas")]
    [Authorize]
    [ProducesResponseType(typeof(IList<AtividadeDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    public ActionResult<IList<AtividadeDTO>> ListarAtividadesAtivas()
    {
        var listaDeAtividadesDTO = _comandos.ListarAtividadesAtivas()
            .Select(AtividadeDTO.EntidadeParaDTO)
            .ToList();
        return Ok(listaDeAtividadesDTO);
    }

    [HttpGet("do-departamento")]
    [Authorize]
    [ProducesResponseType(typeof(IList<AtividadeDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    public ActionResult<IList<AtividadeDTO>> ListarAtividadesDoDepartamento()
    {
        var usuario = ObterUsuarioAutenticado();
        var listaDeAtividadesDTO = _comandos.ListarAtividadesPorDepartamentoSolucionador(usuario)
            .Select(AtividadeDTO.EntidadeParaDTO)
            .ToList();
        return Ok(listaDeAtividadesDTO);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult<AtividadeDTO> ConsultarAtividade(int id)
    {
        return _comandos.ConsultarAtividade(id) is Atividade atividade
            ? Ok(AtividadeDTO.EntidadeParaDTO(atividade))
            : NotFound(new RespostaDTO(RespostaDTO.Tipos.Aviso, "Atividade não encontrada."));
    }

    [HttpPost("criar/")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    public ActionResult<AtividadeDTO> CriarAtividade(CriarAtividadeDTO criarAtividadeDTO)
    {
        // CORRIGIR: Pegar dados de validação
        if (!ModelState.IsValid) throw new ModeloInvalidoException();

        var usuario = ObterUsuarioAutenticado();
        var atividadeDTO = criarAtividadeDTO.ConverterParaAtividadeDTO(usuario.Departamento);

        Atividade atividade = _comandos.CriarAtividade(usuario, atividadeDTO);
        return CreatedAtAction(nameof(ConsultarAtividade), new { id = atividade.Id }, AtividadeDTO.EntidadeParaDTO(atividade));
    }

    [HttpPut("editar/{id}")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public IActionResult EditarAtividade(int id, EditarAtividadeDTO editarAtividadeDTO)
    {
        // CORRIGIR: Pegar dados de validação
        if (!ModelState.IsValid) throw new ModeloInvalidoException();

        var usuario = ObterUsuarioAutenticado();
        var atividadeDTO = editarAtividadeDTO.ConverterParaAtividadeDTO(id, usuario.Departamento);

        return _comandos.EditarAtividade(usuario, atividadeDTO)
            ? NoContent()
            : NotFound();
    }

    [HttpDelete("apagar/{id}")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public IActionResult ApagarAtividade(int id)
    {
        return _comandos.ApagarAtividade(ObterUsuarioAutenticado(), id)
            ? NoContent()
            : NotFound();
    }

    [HttpPost("{id}/definir-solucionadores")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult<RespostaDTO> DefinirSolucionadores([FromBody] IdsDosUsuariosDTO idsDosUsuariosDTO, int id)
    {
        RespostaDTO resposta = _comandos.DefinirSolucionadores(ObterUsuarioAutenticado(), idsDosUsuariosDTO, id);
        if (resposta.Tipo == RespostaDTO.Tipos.Erro) return BadRequest(resposta);
        if (resposta.Tipo == RespostaDTO.Tipos.Aviso) return NotFound(resposta);
        return Ok(resposta);
    }
}
