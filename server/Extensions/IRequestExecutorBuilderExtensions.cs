using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Identity;

using Bulletin.Server.Models;
using Bulletin.Server.Models.Abstractions;
using Bulletin.Server.Resolvers;
using Bulletin.Server.Services;

namespace Bulletin.Server;

public static class IRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder ConfigureGraphQL(this IRequestExecutorBuilder builder) =>
        builder.AddApolloTracing()
            .AddAuthorization()
            .AddInterfaceType<EntityBase>()
            .AddGlobalObjectIdentification(true)
            .RegisterDbContext<DataContext>(DbContextKind.Synchronized)
            .AddQueryFieldToMutationPayloads()
            .AddMutationConventions(applyToAllMutations: true)
            .ModifyRequestOptions(o => o.IncludeExceptionDetails = true)
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .RegisterService<ITokenService>(ServiceKind.Resolver)
            .RegisterService<IPasswordHasher<User>>(ServiceKind.Resolver)
            .AddQueryType<Query>()
            .AddMutationType<Mutation>();
}