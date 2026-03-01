using GraphQL.API.Schema.Mutations;
using GraphQL.API.Schema.Queries;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.MapGraphQL();
app.Run();
