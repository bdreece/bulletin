namespace Bulletin.Server;

public class BadCredentialsException : ArgumentException
{
    public BadCredentialsException() : base("Bad credentials!") { }
}