using Common.Mapping;
using Common.Options;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();
});

builder.Services.AddDbContext<DatabaseContext>(opt =>
{
opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection(AuthOptions.Position));
builder.Services.Configure<Common.Options.PassOptions>(builder.Configuration.GetSection(Common.Options.PassOptions.Position));

var issuer = builder.Configuration["Auth:Issuer"];
var audience = builder.Configuration["Auth:Audience"];
var key = Encoding.UTF8.GetBytes(builder.Configuration["Auth:AccessSecret"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.RequireHttpsMetadata = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidIssuer = issuer,
          ValidateAudience = true,
          ValidAudience = audience,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
      };
  });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseCors(opt => opt
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
