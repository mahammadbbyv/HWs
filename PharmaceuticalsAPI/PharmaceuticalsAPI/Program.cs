using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using PharmaceuticalsAPI;
using PharmaceuticalsAPI.DBService;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Builder;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddCors(ops => ops.AddPolicy("AllowAnyOrigins", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAnyOrigins");

app.UseHttpsRedirection();
app.MapGet("/register", [AllowAnonymous] (string email, string phoneNumber, string password, string confirmPassword) =>
{
    try
    {
        DBService dbService = new();
        return dbService.RegisterPharmacy(email, phoneNumber, password, confirmPassword);
    }
    catch (Exception e)
    {
        return $"{e.Message} {e.StackTrace}";
    }
});
app.MapGet("/login", [AllowAnonymous] (string email, string password, string ipaddress) =>
{
    var issuer = builder.Configuration["Jwt:Issuer"];
    var audience = builder.Configuration["Jwt:Audience"];
    var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
    DBService dbService = new();
    return dbService.LoginPharmacy(email, password, issuer, audience, key, ipaddress);
});

app.MapGet("/loginWithToken", [AllowAnonymous] (string token) =>
{
    DBService dbService = new();
    return dbService.LoginWithToken(token);
});

app.MapGet("/addPharmaceuticalToPharmacy", (string name, string pharmacyId, string token) =>
{
    DBService dbService = new();
    return dbService.AddPharmaceuticalToPharmacy(name, pharmacyId, token);
});

app.MapGet("/removePharmaceuticalFromPharmacy", (string name, string pharmacyId, string token) =>
{
    DBService dbService = new();
    return dbService.RemovePharmaceuticalFromPharmacy(name, pharmacyId, token);
});

app.MapGet("/updatePharmacy", (string pharmacyId, string name, string phoneNumber, string address, string city, string token) =>
{
    DBService dbService = new();
    return dbService.UpdatePharmacy(pharmacyId, name, address, city, phoneNumber, token);
});

app.MapGet("/getPharmacy", (string token) =>
{
    var issuer = builder.Configuration["Jwt:Issuer"];
    var audience = builder.Configuration["Jwt:Audience"];
    var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
    DBService dbService = new();
    return dbService.GetPharmacy(token);
});

app.MapGet("/verifyEmail", [AllowAnonymous] (string email, string code) =>
{
    DBService dbService = new();
    return dbService.VerifyEmail(email, code);
});

app.MapGet("/getPharmacyPharmaceuticals/{pharmacyId}", (string pharmacyId) =>
{
    DBService dbService = new();
    return dbService.GetPharmacyPharmaceuticals(pharmacyId);
});

app.MapGet("/getProducts/{includes}", [AllowAnonymous] (string includes) =>
{
    DBService dbService = new();
    return dbService.GetPharmaceuticals(includes) as IEnumerable<Pharmaceuticals>;
});


app.MapGet("/getPharmacies/{city}/{pharmaceutical}", [AllowAnonymous] (string city, string pharmaceutical) =>
{
    DBService dbService = new();
    return dbService.GetPharmacies(city, pharmaceutical);
});

app.UseAuthentication();
app.UseAuthorization();
app.Run();