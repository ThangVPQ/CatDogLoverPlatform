using CatDogLover_Repository.DAO;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSession();
builder.Services.AddDataProtection();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDbContext<CatDogLoverDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://thang-swp.vercel.app");
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});
var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.UseSession();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Land API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

