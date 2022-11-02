using System.Data;
using Dapper;
using JetBrains.Annotations;
using MediatR;
using OkArt.Infrastructure.Constants;

namespace OkArt.Handlers;

[UsedImplicitly]
public class CreateSolutionHandler : IRequestHandler<CreateSolution.Request>
{
    private readonly IDbConnection _dbConnection;

    public CreateSolutionHandler(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Unit> Handle(CreateSolution.Request request, CancellationToken cancellationToken)
    {
        var query = $@"INSERT INTO {PgTables.Solution} (name, owner_login, file_name) VALUES (@Name, @UserLogin, @Name);";
        await _dbConnection.ExecuteAsync(query, request);
        
        return Unit.Value;
    }
}