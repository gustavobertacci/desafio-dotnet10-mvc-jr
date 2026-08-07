var builder = WebApplication.CreateBuilder(args);

// Registro dos serviços do MVC (Controllers + Views).
builder.Services.AddControllersWithViews();

// -----------------------------------------------------------------------------
// ATENÇÃO CANDIDATO(A)
//
// O acesso a dados NÃO está configurado de propósito.
// Escolha a abordagem que preferir (Entity Framework Core, Dapper ou ADO.NET),
// registre-a aqui e justifique a decisão no README.
// -----------------------------------------------------------------------------

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
