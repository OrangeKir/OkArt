using MediatR;

namespace OkArt.Handlers;

public class CreateSolution
{
    public class Request : IRequest
    {
        public string Name { get; set; }
        public string UserLogin { get; set; }
    }
}