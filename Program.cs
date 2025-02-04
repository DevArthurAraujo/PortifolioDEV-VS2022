using Microsoft.EntityFrameworkCore;
using PortifolioDEV.Repositorios;
using PortifolioDEV.ORM;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Registrar o DbContext se necessário
builder.Services.AddDbContext<BdPortfolioDevContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o repositório (UsuarioRepositorio)
builder.Services.AddScoped<UsuarioRepositorio>();  // Ou AddTransient ou AddSingleton dependendo do caso
// Registrar o repositório (ServicoRepositorio)
builder.Services.AddScoped<ServicoRepositorio>();  // Ou AddTransient ou AddSingleton dependendo do caso
// Registrar o repositório (AgendamentoRepositorio)
builder.Services.AddScoped<AgendamentoRepositorio>();  // Ou AddTransient ou AddSingleton dependendo do caso
// Registrar o repositório (RelatorioRepositorio)
builder.Services.AddScoped<RelatorioRepositorio>();  // Ou AddTransient ou AddSingleton dependendo do caso

// Registrar o IHttpContextAccessor para acessar o HttpContext dentro de repositórios
builder.Services.AddHttpContextAccessor();

// Adicionar suporte a sessões
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Defina o tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // Garante que o cookie de sessão não seja acessível via JavaScript
    options.Cookie.IsEssential = true; // O cookie é essencial para a operação do site
});

// Adicionar suporte a autenticação com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login";  // Caminho para a página de login
        options.LogoutPath = "/Usuario/Logout";  // Caminho para a página de logout
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Tempo de expiração do cookie
    });

// Registrar outros serviços, como controllers com views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar o pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Adicionar o middleware de sessão
app.UseSession();

// Colocar a ordem correta dos middlewares de autenticação e autorização
app.UseRouting();

// Middleware de autenticação: permite identificar quem é o usuário
app.UseAuthentication();  // Coloque antes de UseAuthorization()

// Middleware de autorização: verifica se o usuário tem permissão para acessar a rota
app.UseAuthorization();   // Deve ser chamado depois de UseAuthentication()

// Configurar as rotas dos controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Rodar a aplicação
app.Run();
