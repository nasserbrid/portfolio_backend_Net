using Microsoft.EntityFrameworkCore;
using portfolio_backend_Csharp.Data;
using portfolio_backend_Csharp.Repositories;
using portfolio_backend_Csharp.Services;
using DotNetEnv;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

Env.Load();


var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? throw new InvalidOperationException("DATABASE_URL non trouvé dans les variables d'environnement");

string connectionString;

if (databaseUrl.StartsWith("Host="))
{
    // ? Format PostgreSQL local
    connectionString = databaseUrl;
}
else if (databaseUrl.StartsWith("postgresql://") || databaseUrl.StartsWith("postgres://"))
{
    // ? Format Render à parser
    var databaseUrlFixed = databaseUrl.Replace("postgresql://", "postgres://");

    var databaseUri = new Uri(databaseUrlFixed);
    var userInfo = databaseUri.UserInfo.Split(':');
    if (userInfo.Length != 2)
        throw new InvalidOperationException("Format USER:PASS invalide dans DATABASE_URL");

    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = databaseUri.Host,
        Port = databaseUri.Port > 0 ? databaseUri.Port : 5432,
        Username = userInfo[0],
        Password = userInfo[1],
        Database = databaseUri.AbsolutePath.TrimStart('/'),
        SslMode = SslMode.Require
        //TrustServerCertificate = true
    };

    connectionString = npgsqlBuilder.ConnectionString;
}
else
{
    throw new InvalidOperationException("Format de DATABASE_URL non reconnu.");
}


builder.Configuration["CloudinarySettings:CloudName"] = Environment.GetEnvironmentVariable("CLOUDNAME");
builder.Configuration["CloudinarySettings:ApiKey"] = Environment.GetEnvironmentVariable("API_KEY");
builder.Configuration["CloudinarySettings:ApiSecret"] = Environment.GetEnvironmentVariable("API_SECRET");
builder.Configuration["Email:Username"] = Environment.GetEnvironmentVariable("USERNAME");
builder.Configuration["Email:Password"] = Environment.GetEnvironmentVariable("PASSWORD");

builder.Configuration["ConnectionStrings:SqlDbConnection"] = connectionString;

//var connectionString = builder.Configuration.GetConnectionString("SqlDbConnection");



builder.Services.AddDbContext<ProjectBackendContext>(options =>
     options.UseNpgsql(builder.Configuration.GetConnectionString("SqlDbConnection")));

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<IContactRepository,ContactRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); //car déjà gérer par Render

app.UseAuthorization();

app.MapControllers();

app.Run();
