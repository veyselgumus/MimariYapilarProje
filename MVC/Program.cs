using AppCore.DataAccess.EntityFramework.Bases;
using Business.Services;
using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MVC.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region  IoC Container : Inversion of Control Container (Ba��ml�l�klar�n Y�netimi)

string connectionString = builder.Configuration.GetConnectionString("MimariYapilarProjeDb");// appsettings.json veya appsettings.Development.json dosyalar�ndaki isim �zerinden atanan
																					   // veritaban� ba�lant� string'ini d�ner.

builder.Services.AddDbContext<MimariYapilarContext>(context =>context.UseSqlServer(connectionString));// projede herhangi bir class'ta MimariProjeContext tipinde 
																									  // constructor injection yap�ld���nda MimariProjeContext objesini new'leyerek
																									  // o class'a enjekte eder.

builder.Services.AddScoped(typeof(RepoBase<>), typeof(Repo<>)); // projede herhangi bir class'ta entity tipindeki RepoBase tipinde constructor injection yap�ld���nda
																// entity tipindeki Repo objesini new'leyerek o class'a enjekte eder.

builder.Services.AddScoped<IYapiService,YapiService>();
builder.Services.AddScoped<ITurService,TurService>();
builder.Services.AddScoped<IYapiDetayService,YapiDetayService>();
builder.Services.AddScoped<IMimarService,MimarService>();
builder.Services.AddScoped<IHesapService,HesapService>();
builder.Services.AddScoped<IKullaniciService,KullaniciService>();
builder.Services.AddScoped<IRolService,RolService>();
builder.Services.AddScoped<IReportService, ReportService>();
#endregion

builder.Services.AddControllersWithViews();

#region authentication

builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

	.AddCookie(config =>
	{
		//Kullan�c� giri�i yap�ld�ktan sonra y�nlendirilecegi sayfay� belirler.
		config.LoginPath = "/Kullanici/Kullanicis/Login";

		// Kullan�c�n�n yetkisi olmayan sayfalara girmesini engeller.
		config.AccessDeniedPath = "/Kullanici/Kullanicis/AccessDenied";


		//Sisteme giri� yapt�ktan belli bir �re sonra ��k�� yapmas�n� saglar.
		config.ExpireTimeSpan = TimeSpan.FromMinutes(30);


		//Sistemde her i�lem yap�ld�g�nda ExpireTimeSpan'�n yenilenmesini sa�lar.
		config.SlidingExpiration = true;
	});

#endregion

#region Session

builder.Services.AddSession(config =>
{
	config.IdleTimeout = TimeSpan.FromMinutes(30);

	config.IOTimeout = Timeout.InfiniteTimeSpan;
});

#endregion

#region AppSettings
var section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());

#endregion
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

app.UseAuthentication();//Sen kimsin?

app.UseAuthorization();//Sen i�lem i�in yetkili misin?

#region Session
app.UseSession();

#endregion

#region Area

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});

#endregion

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
