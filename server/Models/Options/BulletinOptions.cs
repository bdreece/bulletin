namespace Bulletin.Server.Models.Options;

public class BulletinOptions
{
    public const string Bulletin = "Bulletin";

    public AuthOptions Auth { get; set; } = default!;
    public TokenOptions Token { get; set; } = default!;
}