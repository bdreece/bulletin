namespace Bulletin.Server;

public class UnauthorizedException : Exception
{
    public UnauthorizedException()
        : base("The current user is unauthorized to access the requested resource") { }
}