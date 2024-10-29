using MessageTestTaskServer.ExceptionHandlers;
using MessageTestTaskServer.Implementation;
using MessageTestTaskServer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddScoped<IMessageComposer, MessageComposer>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapGet("/health", () => Results.Ok("server_isready"));

app.MapControllers();

app.Run();