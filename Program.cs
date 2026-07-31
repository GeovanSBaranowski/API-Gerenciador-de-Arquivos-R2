using Microsoft.EntityFrameworkCore;
using UploadImagemR2.Configurations;
using UploadImagemR2.Data;
using UploadImagemR2.Interfaces;
using UploadImagemR2.Middlewares;
using UploadImagemR2.Repository;
using UploadImagemR2.Services;
using UploadImagemR2.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<R2Settings>()
    .BindConfiguration("R2Settings")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IR2StorageService, R2StorageService>();

builder.Services.AddScoped<ApiKeyService>();

builder.Services.AddScoped<IAplicacaoService, AplicacaoService>();

builder.Services.AddScoped<IAplicacaoRepository, AplicacaoRepository>();

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API V1");
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();