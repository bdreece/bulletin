global using ILogger = Serilog.ILogger;

using Serilog;
using Serilog.Formatting.Compact;

namespace Bulletin.Server;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureBulletin(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                formatter: new RenderedCompactJsonFormatter(),
                path: "logs/log.clef",
                rollingInterval: RollingInterval.Hour)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();

        var logger = Log.Logger.ForContext("SourceContext", nameof(WebApplicationBuilderExtensions));
        logger.Verbose("Initializing services...");
        builder.Services
            .AddBulletinOptions(builder.Configuration)
            .AddBulletinAuth()
            .AddBulletinDatabase()
            .AddBulletinGraphQL();

        logger.Verbose("Services initialized!");
        return builder;
    }
}