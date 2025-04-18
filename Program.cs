using Microsoft.EntityFrameworkCore;
using SAAU.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SAAU_DATA_PASSWORD_PROD");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<AgendamentoRepository>();
builder.Services.AddScoped<AlunoRepository>();
builder.Services.AddScoped<AtendimentoRepository>();
builder.Services.AddScoped<CoordenadorRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cria o banco se não existir
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.UseDeveloperExceptionPage();

Console.WriteLine($"Connection string: {connectionString}");

app.Run();
