using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Aplicacao.Commands;
using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Demanda;
using TechChallenge.Dominio.Exceptions;

namespace TechChallenge.Aplicacao.Controllers;

[ApiController]
[Route("[controller]")]
public class DemandaController : BaseController
{
    private readonly DemandaCommand _comandos;
    private readonly string _detalhesInvalidos = "O preenchimento dos detalhes é obrigatório e seu conteúdo deve possuir ao menos 3 caracteres.";
    private readonly string _mensagemInvalida = "O preenchimento da mensagem é obrigatório e seu conteúdo deve possuir ao menos 3 caracteres.";

    public DemandaController(DemandaCommand comandos)
    {
        _comandos = comandos;
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult<Demanda> BuscarDemanda(int id)
    {
        // CONVERTER RETORNO PARA DTO
        return Ok(_comandos.ConsultarDemanda(id));
    }

    [HttpGet("do-solicitante")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<Demanda>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    public ActionResult<IEnumerable<Demanda>> BuscarDemandasDoSolicitante()
    {
        // CONVERTER RETORNO PARA DTO
        return Ok(_comandos.ListarDemandasDoSolicitante(ObterUsuarioAutenticado()));
    }

    [HttpGet("do-departamento-solicitante")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<Demanda>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    public ActionResult<IEnumerable<Demanda>> BuscarDemandasDoDepartamentoSolicitante()
    {
        // CONVERTER RETORNO PARA DTO
        return Ok(_comandos.ListarDemandasDoDepartamentoSolicitante(ObterUsuarioAutenticado()));
    }

    [HttpGet("do-solucionador")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<Demanda>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    public ActionResult<IEnumerable<Demanda>> BuscarDemandasDoSolucionador()
    {
        // CONVERTER RETORNO PARA DTO
        return Ok(_comandos.ListarDemandasDoSolucionador(ObterUsuarioAutenticado()));
    }

    [HttpGet("do-departamento-solucionador")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<Demanda>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    public ActionResult<IEnumerable<Demanda>> BuscarDemandasDoDepartamentoSolucionador()
    {
        // CONVERTER RETORNO PARA DTO
        return Ok(_comandos.ListarDemandasDoDepartamentoSolucionador(ObterUsuarioAutenticado()));
    }

    [HttpPost("abrir")]
    [Authorize]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult<Demanda> AbrirDemanda(int id, TextoDTO detalhes)
    {
        if (!ModelState.IsValid) throw new ModeloInvalidoException(_detalhesInvalidos);
        var demanda = _comandos.AbrirDemanda(ObterUsuarioAutenticado(), id, detalhes.Conteudo);
        // CONVERTER RETORNO PARA DTO
        return Ok(demanda);
    }

    [HttpPut("{id}/encaminhar")]
    [Authorize]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult EncaminharDemanda(int id, int idNovoSolucionador, TextoDTO mensagem)
    {
        if (!ModelState.IsValid) throw new ModeloInvalidoException(_mensagemInvalida);
        _comandos.EncaminharDemanda(ObterUsuarioAutenticado(), id, idNovoSolucionador, mensagem.Conteudo);
        return NoContent();
    }

    [HttpPut("{id}/capturar")]
    [Authorize]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult CapturarDemanda(int id)
    {
        _comandos.CapturarDemanda(ObterUsuarioAutenticado(), id);
        return NoContent();
    }

    [HttpPut("{id}/rejeitar")]
    [Authorize]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult RejeitarDemanda(int id, TextoDTO mensagem)
    {
        if (!ModelState.IsValid) throw new ModeloInvalidoException(_mensagemInvalida);
        _comandos.RejeitarDemanda(ObterUsuarioAutenticado(), id, mensagem.Conteudo);
        return NoContent();
    }

    [HttpPut("{id}/responder")]
    [Authorize]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult ResponderDemanda(int id, TextoDTO mensagem)
    {
        if (!ModelState.IsValid) throw new ModeloInvalidoException(_mensagemInvalida);
        _comandos.ResponderDemanda(ObterUsuarioAutenticado(), id, mensagem.Conteudo);
        return NoContent();
    }

    [HttpPut("{id}/cancelar")]
    [Authorize]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult CancelarDemanda(int id, TextoDTO mensagem)
    {
        if (!ModelState.IsValid) throw new ModeloInvalidoException(_mensagemInvalida);
        _comandos.CancelarDemanda(ObterUsuarioAutenticado(), id, mensagem.Conteudo);
        return NoContent();
    }

    [HttpPut("{id}/reabrir")]
    [Authorize]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult<Demanda> ReabrirDemanda(int id, TextoDTO mensagem)
    {
        if (!ModelState.IsValid) throw new ModeloInvalidoException(_mensagemInvalida);
        var demanda = _comandos.ReabrirDemanda(ObterUsuarioAutenticado(), id, mensagem.Conteudo);
        // CONVERTER RETORNO PARA DTO
        return Ok(demanda);
    }
}
