using Serilog;

namespace Bulletin.Server;

public static class WebApplicationExtensions
{
    private static readonly ILogger _logger =
        Log.Logger.ForContext("SourceContext", nameof(WebApplicationBuilderExtensions));

    public static WebApplication UseBulletin(this WebApplication app)
    {
        _logger.Verbose("Configuring application...");

        app.UseCors();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapGraphQL();

        _logger.Verbose("Application configured!");
        return app;
    }
}