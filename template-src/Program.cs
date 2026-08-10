using Desafio.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Registro dos serviços do MVC (Controllers + Views).
builder.Services.AddControllersWithViews(options =>
{
    var mensagens = options.ModelBindingMessageProvider;

    mensagens.SetValueMustNotBeNullAccessor(
        _ => "Este campo é obrigatório.");

    mensagens.SetAttemptedValueIsInvalidAccessor(
        (valor, campo) =>
            $"O valor '{valor}' não é válido para o campo {campo}.");

    mensagens.SetValueIsInvalidAccessor(
        valor => $"O valor '{valor}' é inválido.");

    mensagens.SetValueMustBeANumberAccessor(
        campo => $"O campo {campo} deve ser um número.");
});

// Obtém a string de conexão configurada no appsettings.json.
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "A connection string 'DefaultConnection' não foi configurada.");

// Registra o contexto do Entity Framework Core utilizando SQL Server.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var culturaBrasileira = new CultureInfo("pt-BR");

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culturaBrasileira),
    SupportedCultures = [culturaBrasileira],
    SupportedUICultures = [culturaBrasileira]
};

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
