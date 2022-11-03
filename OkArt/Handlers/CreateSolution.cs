using JetBrains.Annotations;
using MediatR;
using FileInfo = OkArt.Integrations.GlobalContracts.FileInfo;

namespace OkArt.Handlers;

[PublicAPI]
public class CreateSolution
{
    [PublicAPI]
    public class Request : IRequest<Response>
    {
        public string Name { get; set; } = null!;
        public string UserLogin { get; set; } = null!;
        public string? Description { get; set; }
        public FileInfo FileInfo { get; set; } = null!;
    }
    
    [PublicAPI]
    public class Response
    {
        public long Id { get; set; }
    }
}