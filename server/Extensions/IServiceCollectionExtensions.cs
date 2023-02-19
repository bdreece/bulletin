using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

using Bulletin.Server.Models;
using Bulletin.Server.Models.Options;
using Bulletin.Server.Services;

using static Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults;
using Path = System.IO.Path;

namespace Bulletin.Server;

public static class IServiceCollectionExtensions
{
    private static readonly ILogger _logger =
        Log.Logger.ForContext("SourceContext", nameof(IServiceCollectionExtensions));

    public static IServiceCollection AddBulletinOptions(this IServiceCollection services, IConfiguration config)
    {
        _logger.Verbose("Loading configuration options...");
        var bulletinConfig = config.GetSection(BulletinOptions.Bulletin);
        services.Configure<BulletinOptions>(bulletinConfig);
        services.Configure<AuthOptions>(bulletinConfig.GetSection(AuthOptions.Auth));
        services.Configure<TokenOptions>(bulletinConfig.GetSection(TokenOptions.Token));
        _logger.Verbose("Options loaded!");
        return services;
    }

    public static IServiceCollection AddBulletinAuth(this IServiceCollection services)
    {
        _logger.Verbose("Configuring authentication services...");
        using var sp = services.BuildServiceProvider();
        var options = sp.GetRequiredService<IOptions<TokenOptions>>().Value;

        services.AddAuthentication(AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidAudience = options.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = options.Issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = options.SecurityKey,
                };
            });

        _logger.Verbose("Authentication services configured!");
        _logger.Verbose("Configuring authorization services");
        services.AddAuthorization()
            .AddCors(o => o.AddDefaultPolicy(b => b
                .AllowCredentials()
                .AllowAnyHeader()
                .WithOrigins(new[] { "http://localhost:5173" })))
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddScoped<ITokenService, TokenService>();

        _logger.Verbose("Authorization services configured!");
        return services;
    }

    public static IServiceCollection AddBulletinDatabase(this IServiceCollection services)
    {
        _logger.Verbose("Configuring SQLite database...");
        var folder = Environment.SpecialFolder.ApplicationData;
        var path = Environment.GetFolderPath(folder);
        var source = Path.Join(path, "bulletin.db");
        var connectionString = $"Data Source={source}";

        services.AddDbContextFactory<DataContext>(o =>
            o.UseSqlite(connectionString));

        _logger.Verbose("Database configured!");
        return services;
    }

    public static IServiceCollection AddBulletinGraphQL(this IServiceCollection services)
    {
        _logger.Verbose("Configuring GraphQL request executor...");
        services.AddHttpContextAccessor()
            .AddGraphQLServer(Schema.DefaultName)
            .ConfigureGraphQL();

        _logger.Verbose("GraphQL request executor configured!");
        return services;
    }
}