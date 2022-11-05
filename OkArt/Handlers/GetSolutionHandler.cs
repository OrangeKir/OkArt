using System.Data;
using Dapper;
using JetBrains.Annotations;
using MediatR;
using OkArt.Infrastructure.Constants;
using OkArt.Integrations;

namespace OkArt.Handlers;

[UsedImplicitly]
public class GetSolutionHandler : IRequestHandler<GetSolution.Request, GetSolution.Response>
{
    private readonly IDbConnection _dbConnection;
    private readonly IFsContext _fsContext;

    public GetSolutionHandler(
        IDbConnection dbConnection,
        IFsContext fsContext)
    {
        _dbConnection = dbConnection;
        _fsContext = fsContext;
    }

    public async Task<GetSolution.Response> Handle(GetSolution.Request request, CancellationToken cancellationToken)
    {
        var query = @$"
                    SELECT
                        name,
                        file_name   as FileName,
                        description
                    FROM {PgTables.Solution}
                    WHERE id = @Id";

        var response = await _dbConnection.QuerySingleAsync<GetSolution.ResponseDto>(query, request);

        var file = await _fsContext.ReadFile(response.FileName, cancellationToken);

        return new GetSolution.Response
        {
            Name = response.Name,
            Description = response.Description,
            FileInfo = file
        };
    }
}