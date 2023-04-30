using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BloodNetwork.Data;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Clinics");
    options.Conventions.AuthorizeFolder("/Appointments");
    options.Conventions.AllowAnonymousToPage("/Appointments/Index");
    options.Conventions.AllowAnonymousToPage("/Appointments/Details");
    options.Conventions.AllowAnonymousToPage("/Clinics/Index");
    options.Conventions.AllowAnonymousToPage("/Clinics/Details");
    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Adresses", "AdminPolicy");

});


builder.Services.AddDbContext<BloodNetworkContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BloodNetworkContext") ?? throw new InvalidOperationException("Connection string 'BloodNetworkContext' not found.")));

builder.Services.AddDbContext<ClinicIdentityContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("BloodNetworkContext") ?? throw new InvalidOperationException("Connection string 'BloodNetworkContext' not found.")));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
options.SignIn.RequireConfirmedAccount = true)
 .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<ClinicIdentityContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
