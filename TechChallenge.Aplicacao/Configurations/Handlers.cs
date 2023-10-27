﻿using TechChallenge.Aplicacao.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;
using System.Text.Json;
using TechChallenge.Dominio.Exceptions;

namespace TechChallenge.Aplicacao.Configurations;

public static class Handlers
{
    public static Task OnAuthenticationFailedHandler(AuthenticationFailedContext context)
    {
        context.Response.OnStarting(async () =>
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                Encoding.UTF8.GetString(
                    JsonSerializer.SerializeToUtf8Bytes(
                        new RespostaDTO(RespostaDTO.Tipos.Erro, "Token inválido.")
                    )
                )
            );
        });
        return Task.CompletedTask;
    }

    public static Task OnForbiddenHandler(ForbiddenContext context)
    {
        context.Response.OnStarting(async () =>
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                Encoding.UTF8.GetString(
                    JsonSerializer.SerializeToUtf8Bytes(
                        new RespostaDTO(RespostaDTO.Tipos.Erro, "Usuário não autorizado.")
                    )
                )
            );
        });
        return Task.CompletedTask;
    }

    public static Task ExceptionHandler(HttpContext context)
    {
        context.Response.OnStarting(async () =>
        {
            int statusCode = 500;
            RespostaDTO resposta = new(RespostaDTO.Tipos.Erro, "Erro interno do servidor.");

            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandlerFeature is not null)
            {
                var exception = exceptionHandlerFeature.Error;
                if (exception is DadosInvalidosException)
                {
                    statusCode = 400;
                    resposta = new(RespostaDTO.Tipos.Aviso, exception.Message.ToString());
                }
                else if (exception is UsuarioNaoAutorizadoException)
                {
                    statusCode = 403;
                    resposta = new(RespostaDTO.Tipos.Erro, exception.Message.ToString());
                }
                else if (exception is EntidadeNaoEncontradaException)
                {
                    statusCode = 404;
                    resposta = new(RespostaDTO.Tipos.Aviso, exception.Message.ToString());
                }
            }
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                Encoding.UTF8.GetString(
                    JsonSerializer.SerializeToUtf8Bytes(resposta)
                )
            );
        });
        return Task.CompletedTask;
    }
}
