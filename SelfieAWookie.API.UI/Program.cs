using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Repositories;
using SelfieAWookie.API.UI.ExtensionMethods;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SelfiesContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SelfiesDatabase"), sqlOptions =>{});
    });

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    //options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<SelfiesContext>();

// Instancier une instance d'interface <Interface, instance>
builder.Services.AddCustomOptions(builder.Configuration);
builder.Services.AddInjections();
builder.Services.AddCustomSecurity(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(SecurityMethods.DEFAULT_POLICY);

app.MapControllers();

app.Run();
