using MvcClienteApiManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string urlemp = builder.Configuration.GetValue<string>("ApiUrls:ApiEmpleados");
string urldept = builder.Configuration.GetValue<string>("ApiUrls:ApiDepartamentos");
ServiceApiManagement service =
    new ServiceApiManagement(urlemp, urldept);
builder.Services.AddTransient<ServiceApiManagement>(x => service);

builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
