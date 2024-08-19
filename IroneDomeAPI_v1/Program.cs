using IroneDomeAPI_v1.Middlewares.Global;
using IroneDomeAPI_v1.Models;
using IroneDomeAPI_v1.Services;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);


// Add logging services
// builder.Logging.ClearProviders();
// builder.Logging.AddConsole(); // Add console logging

builder.Services.AddSingleton<IDbService<Attack>, DbService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseMiddleware<GlobalLoggingMiddleware>();

app.MapControllers();

app.Run();

