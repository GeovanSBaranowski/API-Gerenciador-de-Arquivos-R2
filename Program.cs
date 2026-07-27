using Microsoft.EntityFrameworkCore;
using UploadImagemR2.Configurations;
using UploadImagemR2.Data;
using UploadImagemR2.Middlewares;
using UploadImagemR2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<R2Settings>()
    .BindConfiguration("R2Settings")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IR2StorageService, R2StorageService>();

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