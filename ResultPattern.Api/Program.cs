using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ResultPattern.Api.Abstractions;
using ResultPattern.Api.Database.Options;
using ResultPattern.Api.Database;
using ResultPattern.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("DatabaseConnection"));

builder.Services.AddDbContext<ApplicationDbContext>((sp, optionsBuilder) =>
{
    var options = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;

    optionsBuilder.UseNpgsql(options.GetConnectionString());

});

builder.Services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddEndpoints(assembly);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(assembly));

builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();