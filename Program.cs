using CopilotAPI.AppDataContext;
using CopilotAPI.Interfaces;
using CopilotAPI.Middleware;
using CopilotAPI.Models;
using CopilotAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Add  This to in the Program.cs file
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings")); // Add this line
builder.Services.AddSingleton<TodoDbContext>(); // Add this line


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // Add this line

builder.Services.AddProblemDetails();  // Add this line

builder.Services.AddScoped<ITodoServices, TodoServices>();

// Adding of login 
builder.Services.AddLogging();  //  Add this line


var app = builder.Build();

{
    using var scope = app.Services.CreateScope(); // Add this line
    var context = scope.ServiceProvider; // Add this line
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseExceptionHandler();
app.UseExceptionHandler("/Error", createScopeForErrors: true);
app.UseAuthorization();

app.MapControllers();

app.Run();
