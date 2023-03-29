using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ToDo.Api.Services.Services.Contracts;
using ToDo.Api.Services.Services;
using ToDo.Data.Data;
using ToDo.Repositories.Repositories;
using ToDo.Repositories.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BlazorToDoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BlazorToDoDbContext")));
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
builder.Services.AddScoped<IToDoItemService, ToDoItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
policy.WithOrigins("https://localhost:7171", "http://localhost:7171")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
