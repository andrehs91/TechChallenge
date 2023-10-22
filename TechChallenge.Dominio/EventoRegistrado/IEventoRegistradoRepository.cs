﻿namespace TechChallenge.Dominio.EventoRegistrado;

public interface IEventoRegistradoRepository
{
    void Criar(EventoRegistrado eventoRegistrado);
    EventoRegistrado? BuscarPorId(short id);
    IList<EventoRegistrado> BuscarTodos();
    IList<EventoRegistrado> BuscarPorIdDaDemanda(int id);
    void Editar(EventoRegistrado eventoRegistrado);
    void Apagar(EventoRegistrado eventoRegistrado);
}
