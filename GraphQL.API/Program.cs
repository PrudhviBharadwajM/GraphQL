using GraphQL.API.Schema.Mutations;
using GraphQL.API.Schema.Queries;
using GraphQL.API.Schema.Subscriptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

var app = builder.Build();
app.UseWebSockets();
app.MapGraphQL();
app.Run();
