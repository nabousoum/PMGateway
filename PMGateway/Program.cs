using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration des paramètres JWT
var key = Encoding.ASCII.GetBytes("your_secret_key_here");

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
        ValidIssuer = "your_issuer_here",
        ValidAudience = "your_audience_here",
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero // optional: setting clockskew to zero removes delay in token expiration time
    };
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ajouter le service CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin() // Permet toutes les origines
            .AllowAnyHeader() // Permet tous les en-têtes
            .AllowAnyMethod() // Permet toutes les méthodes HTTP
            .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)); // Définir la durée de vie maximale pour les requêtes pré-vol
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Utiliser la politique CORS définie
app.UseCors("AllowAllOrigins");

// Utiliser l'authentification et l'autorisation
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
