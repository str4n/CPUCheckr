using CPUCheckr.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore(builder.Configuration);

var app = builder.Build();

app.UseCore();

app.Run();