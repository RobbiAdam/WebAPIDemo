using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

builder.Services.AddHttpClient("CarsApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7085/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("AuthorityApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7085/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddSession(option =>
{
    option.Cookie.HttpOnly = true;
    option.IdleTimeout = TimeSpan.FromHours(1);
    option.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IWebApiExecute, WebApiExecute>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
