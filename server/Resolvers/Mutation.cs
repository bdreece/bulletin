using Serilog;

namespace Bulletin.Server.Resolvers;

public partial class Mutation
{
    private readonly ILogger _logger = Log.Logger.ForContext<Mutation>();
}