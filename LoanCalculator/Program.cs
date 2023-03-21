

using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;



services.AddControllersWithViews().AddSessionStateTempDataProvider();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseRouting();

app.UseRequestLocalization();



app.UseStaticFiles();


app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute("default", "{controller=Home}/{action=Calculate}/{id?}");
});


app.Run();
