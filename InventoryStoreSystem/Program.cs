using InventoryApi.Middleware;
using InventoryApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenAuthentication").GetValue<string>("SecretKey", "GappSrtKey"))),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetSection("TokenAuthentication").GetValue<string>("Issuer", "GunStoreIssuer"),
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("TokenAuthentication").GetValue<string>("Audience", "APIForMob"),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<InventoryApi.Model.Settings>(Opt => {
    Opt.ConnectionString = builder.Configuration.GetSection("DbConnection:ConnectionString").Value;
    Opt.Database = builder.Configuration.GetSection("DbConnection:Database").Value;
});
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IinventoryRepository, InventoryRepository>();
builder.Services.AddControllers().AddJsonOptions(Opt => {
    Opt.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(Options => Options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
app.UseAuthentication();
app.UseMiddleware<TokenProviderMiddleware>(Options.Create(new TokenProviderOptions
{
    Path = builder.Configuration.GetSection("TokenAuthentication").GetValue<string>("TokenPath", "/api/Token"),
    Audience = builder.Configuration.GetSection("TokenAuthentication").GetValue<string>("Audience", "APIForMob"),
    Issuer = builder.Configuration.GetSection("TokenAuthentication").GetValue<string>("Issuer", "GunStoreIssuer"),
    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenAuthentication").GetValue<string>("SecretKey", "GappSrtKey"))), SecurityAlgorithms.HmacSha256),
}));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

