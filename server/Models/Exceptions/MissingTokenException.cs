namespace Bulletin.Server;

public enum TokenType
{
    ACCESS = 1,
    REFRESH = 2
}

public class MissingTokenException : ArgumentNullException
{
    public MissingTokenException(TokenType tokenType)
        : base($"Missing {tokenType.ToString().ToLower()} token!") { }
}