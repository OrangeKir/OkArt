using System.Data;
using Dapper;
using JetBrains.Annotations;
using MediatR;
using OkArt.Infrastructure.Constants;
using OkArt.Integrations;

namespace OkArt.Handlers;

[UsedImplicitly]
public class CreateSolutionHandler : IRequestHandler<CreateSolution.Request, CreateSolution.Response>
{
    private readonly IDbConnection _dbConnection;
    private readonly IFsContext _fsContext;

    public CreateSolutionHandler(
        IDbConnection dbConnection,
        IFsContext fsContext)
    {
        _dbConnection = dbConnection;
        _fsContext = fsContext;
    }

    public async Task<CreateSolution.Response> Handle(CreateSolution.Request request, CancellationToken cancellationToken)
    {
        var query = $@"
                    INSERT INTO {PgTables.Solution} (name, owner_login, description, file_name) 
                    VALUES (@Name, @UserLogin, @Description, @FileName)
                    RETURNING id;";
        
        var fileName = await _fsContext.WriteFile(request.FileInfo);
        
        return await _dbConnection.QuerySingleAsync<CreateSolution.Response>(query, new {request.Name, request.UserLogin, request.Description, fileName});
    }
}