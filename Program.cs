using Microsoft.EntityFrameworkCore;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Data;
using SimplifiedBankingApi.Middlewares;
using SimplifiedBankingApi.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<BankContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SimplifiedBankingDB"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<Middleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
