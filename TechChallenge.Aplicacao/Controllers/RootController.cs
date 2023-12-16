﻿using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Aplicacao.Controllers;

[ApiController]
[Route("/")]
public class RootController : BaseController
{
    public RootController() { }

    [HttpGet]
    public ActionResult Redirecionar()
    {
        return Redirect("/swagger/index.html");
    }
}
