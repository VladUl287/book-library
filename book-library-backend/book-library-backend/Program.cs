using BookLibraryApi.Configuration;
using BookLibraryApi.Services;
using BookLibraryApi.Services.Contracts;
using Common.Configuration;
using Common.Mapping;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var config = await Doopler.GetSecretsAsync<Config>();

builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();
});

builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseNpgsql(config.DBConnection);
});

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddTransient<IBookmarkService, BookmarkService>();
builder.Services.AddTransient<ICollectionService, CollectionService>();
builder.Services.AddSingleton<Config>(config);

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.AccessSecret));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.RequireHttpsMetadata = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidIssuer = config.Issuer,
          ValidateAudience = true,
          ValidAudience = config.Audience,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key,
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
    .WithOrigins("http://localhost:8080")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
