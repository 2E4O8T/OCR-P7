using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Repositories;
using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;
using P7CreateRestApi.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add Logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute, outputTemplate: "{Timestamp:G} {Message}{NewLine:1}{Exception:1}")
    .Enrich.WithThreadId()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .CreateLogger();
Log.Information("Application Starting Up");

builder.Host.UseSerilog();

builder.Services.AddLogging();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PostTradesDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:UsersDatabase").Value);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 12;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
})  .AddEntityFrameworkStores<UsersDbContext>()
    .AddDefaultTokenProviders();

// En suspens !
// ML
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(key: "JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  //ML
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection(key: "JwtConfig:Secret").Value);

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,  // For Dev
        ValidateAudience = false,  // For Dev
        RequireExpirationTime = false,  // For Dev
        ValidateLifetime = true
    };
});

// En suspens !
// Service Interfaces
//builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped <ICurvePointRepository, CurvePointRepository>();
builder.Services.AddScoped <IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRuleNameRepository, RuleNameRepository>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    //option.SwaggerDoc("V1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new List<string>()
        }
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();