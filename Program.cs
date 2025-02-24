using Microsoft.EntityFrameworkCore;
using learn_asp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();