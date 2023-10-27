using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using TechChallenge.Aplicacao.Commands;
using TechChallenge.Aplicacao.Configurations;
using TechChallenge.Aplicacao.Services;
using TechChallenge.Aplicacaoptions.Configurations;
using TechChallenge.Dominio.Interfaces;
using TechChallenge.Dominio.Policies;
using TechChallenge.Infraestrutura.Data;
using TechChallenge.Infraestrutura.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();
builder.Services.AddScoped<IDemandaRepository, DemandaRepository>();
builder.Services.AddScoped<ISolucionadorPolicy, SolucionadorPolicy>();
builder.Services.AddScoped<UsuarioCommand>();
builder.Services.AddScoped<AtividadeCommand>();
builder.Services.AddScoped<DemandaCommand>();
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Secret"]!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    o.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context => Handlers.OnAuthenticationFailedHandler(context),
        OnForbidden = context => Handlers.OnForbiddenHandler(context)
    };
});
builder.Services.AddSwaggerGen(options => Options.SwaggerGenOptions(options));

var app = builder.Build();

app.UseExceptionHandler(Options.ExceptionHandlerOptions());
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
