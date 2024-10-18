using biblioteca.Model;
using biblioteca.ORM;
using biblioteca.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicione servi�os ao cont�iner.

builder.Services.AddControllers();

// Adicione o contexto do banco de dados
builder.Services.AddDbContext<BdBibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicione os reposit�rios
builder.Services.AddScoped<CategoriaR>();
builder.Services.AddScoped<EmprestimoR>();
builder.Services.AddScoped<LivroR>();
builder.Services.AddScoped<FuncionarioR>();
builder.Services.AddScoped<ReservaR>();
builder.Services.AddScoped<UsuarioR>();
builder.Services.AddScoped<MembroR>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoWebAPI", Version = "v1" });

    // Configura o Swagger para usar o Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato **Bearer {seu_token}**",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configura��o de autentica��o JWT
var key = "A1B2C3D4E5F6G7H8I9J0K1L2M3N4O5P6"; // 32 caracteres para 256 bits
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:7025", // O emissor deve corresponder ao que voc� definiu no token
        ValidAudience = "http://localhost:7025", // A audi�ncia deve corresponder ao que voc� definiu no token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // Usando a chave
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
