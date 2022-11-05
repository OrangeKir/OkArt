using JetBrains.Annotations;
using MediatR;
using FileInfo = OkArt.Integrations.GlobalContracts.FileInfo;

namespace OkArt.Handlers;

[PublicAPI]
public class GetSolution
{
    [PublicAPI]
    public record Request(long Id) : IRequest<Response>;

    [PublicAPI]
    public class Response
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public FileInfo FileInfo { get; set; } = null!;
    }
    
    internal class ResponseDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string FileName { get; set; } = null!;
    }
}