using Microsoft.EntityFrameworkCore;
using Restaurant.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Kết nối PostgreSQL và trỏ Migrations về Infrastructure
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Restaurant.Infrastructure")
    ));

// 2. Mở CORS cho Frontend (Vite cổng 5173)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()); // Cần thiết cho SignalR Hubs
});

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();