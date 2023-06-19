using Core;
using Core.Behavior;
using Core.Services;
using FluentValidation;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services;
using MediatR;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CoreEntryPoint).Assembly, typeof(InfrastructureEntryPoint).Assembly));

//Validator
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CoreEntryPoint).Assembly, includeInternalTypes: true);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

