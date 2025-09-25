using SafeLink_TCC.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext para MySQL
builder.Services.AddDbContext<DbConfig>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Configura��o de autentica��o com Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.Cookie.Name = "SafeLinkAuthCookie"; // Nome mais gen�rico
        config.LoginPath = "/Autenticacao/Login";

        // CORRE��O: Defina uma p�gina diferente, ou comente/remova se n�o tiver uma.
        // Se voc� n�o definir uma, o ASP.NET Core apenas retornar� um status 403 (Forbidden).
        config.AccessDeniedPath = "/Home/AcessoNegado"; // Crie uma view em Views/Home/AcessoNegado

        config.ExpireTimeSpan = TimeSpan.FromHours(1);
        config.SlidingExpiration = true;
    });

// Adiciona suporte a Controllers e Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Rota padr�o deve ser a Home, n�o o Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Aluno}/{action=Cadastrar}/{id?}");

app.Run();
