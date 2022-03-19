using Microsoft.EntityFrameworkCore;
using TodoListWebApiWithControllers.Models;

// builder
var builder = WebApplication.CreateBuilder(args);

// register DbContext into dependency injection (DI) container
// The DI container provides service to controllers
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

// build app
var app = builder.Build();

// run only if dev env
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();