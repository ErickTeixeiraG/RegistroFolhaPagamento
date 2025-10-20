using APIFolha.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/api/folha/cadastrar", ([FromBody] FolhaPagamento folhaPagamento, [FromServices] AppDataContext ctx) =>
{
    if (folhaPagamento.Mes < 1 || folhaPagamento.Mes > 12 || folhaPagamento.Ano < 2000)
    {
        return Results.BadRequest("Mês inválido. Deve ser entre 1 e 12.");
    }
    if (ctx.FolhaPagamentos.Any(f =>
            f.CPF == folhaPagamento.CPF &&
            f.Mes == folhaPagamento.Mes &&
            f.Ano == folhaPagamento.Ano))
    {
        return Results.Conflict("Já existe uma folha de pagamento para este CPF, mês e ano.");
    }
    folhaPagamento.SalarioBruto = folhaPagamento.HorasTrabalhadas * folhaPagamento.ValorHora;

    switch (folhaPagamento.SalarioBruto)
    {
        case <= 1693.72:
            folhaPagamento.Inss = folhaPagamento.SalarioBruto * 0.08;
            break;
        case <= 2822.90:
            folhaPagamento.Inss = folhaPagamento.SalarioBruto * 0.09;
            break;
        case <= 5645.80:
            folhaPagamento.Inss = folhaPagamento.SalarioBruto * 0.11;
            break;
        default:
            folhaPagamento.Inss = 621.03;
            break;
    }

    switch (folhaPagamento.SalarioBruto)
    {
        case <= 1903.98:
            folhaPagamento.IR = 0.0;
            break;
        case <= 2826.65:
            folhaPagamento.IR = (folhaPagamento.SalarioBruto * 0.075) - 142.80;
            break;
        case <= 3751.05:
            folhaPagamento.IR = (folhaPagamento.SalarioBruto * 0.15) - 354.80;
            break;
        case <= 4664.68:
            folhaPagamento.IR = (folhaPagamento.SalarioBruto * 0.225) - 636.13;
            break;
        default:
            folhaPagamento.IR = (folhaPagamento.SalarioBruto * 0.275) - 869.36;
            break;
    }

    folhaPagamento.Fgts = folhaPagamento.SalarioBruto * 0.08;

    folhaPagamento.SalarioLiquido = folhaPagamento.SalarioBruto - folhaPagamento.Inss - folhaPagamento.IR;

    ctx.FolhaPagamentos.Add(folhaPagamento);

    ctx.SaveChanges();
    
    return Results.Created("", folhaPagamento);
});

app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.FolhaPagamentos.Any())
    {
        return Results.Ok(ctx.FolhaPagamentos.ToList());
    }
    return Results.NotFound("A lista de folhas de pagamento está vazia.");
});

app.MapGet("api/folha/buscar/{CPF}/{Mes}/{Ano}", (string CPF, int Mes, int Ano, [FromServices] AppDataContext ctx) =>
{
    if(string.IsNullOrEmpty(CPF) || Mes < 1 || Mes > 12 || Ano < 2000)
    {
        return Results.BadRequest("Parâmetros inválidos para CPF, mês ou ano.");
    }
    var folha = ctx.FolhaPagamentos.FirstOrDefault(f =>
        f.CPF == CPF &&
        f.Mes == Mes &&
        f.Ano == Ano);
    if (folha != null)
    {
        return Results.Ok(folha);
    }
    return Results.NotFound("Folha de pagamento não encontrada para o CPF, mês e ano informados.");
});

app.MapDelete("/api/folha/deletar/{Id}", ([FromRoute] int Id, [FromServices] AppDataContext ctx) =>
{
    var folha = ctx.FolhaPagamentos.Find(Id);
    if (folha != null)
    {
        ctx.FolhaPagamentos.Remove(folha);
        ctx.SaveChanges();
        return Results.Ok("Folha de pagamento deletada com sucesso.");
    }
    return Results.NotFound("Folha de pagamento não encontrada para o Id informado.");
});

app.MapGet("/api/folha/total-liquido", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.FolhaPagamentos.Any())
    {
        var totalLiquido = ctx.FolhaPagamentos.Sum(f => f.SalarioLiquido);
        return Results.Ok(totalLiquido);
    }
    return Results.NotFound("A lista de folhas de pagamento está vazia.");
});


app.Run();
