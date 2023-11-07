using DSCC.CW1._9713.API.Db;
using DSCC.CW1._9713.API.Models;
using DSCC.CW1._9713.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("http://example.com",
        "http://ec2-13-51-193-110.eu-north-1.compute.amazonaws.com/")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddScoped<IService<Customer>, CustomerService>();
builder.Services.AddScoped<IService<Order>, OrderService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
        options => options.EnableRetryOnFailure(10));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseHsts();

app.UseAuthorization();

app.MapControllers();

app.Run();
