namespace Bulletin.Server.Models.Options;

public class AuthOptions
{
    public const string Auth = "Auth";

    public string RefreshTokenName { get; set; } = "BulletinRefresh";
}