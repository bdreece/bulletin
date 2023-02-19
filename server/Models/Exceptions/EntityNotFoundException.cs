namespace Bulletin.Server;

public class EntityNotFoundException : NullReferenceException
{
    public EntityNotFoundException(Type entityType) : base($"{entityType.Name} not found!") { }
}