namespace Bulletin.Server.Models.Options;

public class AuthOptions
{
    public const string Auth = "Auth";

    public string RefreshTokenName { get; set; } = "BulletinRefresh";
    public IEnumerable<string> CorsOrigins { get; set; } = Enumerable.Empty<string>();
}