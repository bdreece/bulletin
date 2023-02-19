using Bulletin.Server;

await using var app = WebApplication.CreateBuilder(args)
    .ConfigureBulletin()
    .Build();

await app.UseBulletin()
    .RunAsync();
