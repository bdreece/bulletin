namespace Bulletin.Server;

public class BadTokenException : ArgumentException
{
    public BadTokenException(TokenType tokenType)
        : base($"Malformed {tokenType.ToString().ToLower()} token!", "token") { }
}