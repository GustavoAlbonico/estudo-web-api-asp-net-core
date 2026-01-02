using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MinimalApiLivros.AppServicesEntensions;
using MinimalApiLivros.Context;
using MinimalApiLivros.Endpoints;
using MinimalApiLivros.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

builder.Services.AddTransient<ILivroService, LivroService>();

var app = builder.Build();

app.MapGroup("/identity/").MapIdentityApi<IdentityUser>();
app.RegisterLivrosEndpoints();

var environment = app.Environment;
app.UseExceptionHandling(environment)
   .UseSwaggerMiddleware()
   .UseAppCors();

app.UseHttpsRedirection();

app.Run();