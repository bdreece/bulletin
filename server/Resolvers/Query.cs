global using UseFilteringAttribute = HotChocolate.Data.UseFilteringAttribute;
global using UseSortingAttribute = HotChocolate.Data.UseSortingAttribute;

using Serilog;

namespace Bulletin.Server.Resolvers;

public partial class Query
{
    private readonly ILogger _logger = Log.Logger.ForContext<Query>();
}