using MediatR;
using Microsoft.AspNetCore.Mvc;
using OkArt.Handlers;

namespace OkArt.Controllers;

[ApiController]
[Route("solution")]
public class SolutionController : ControllerBase
{
    private readonly IMediator _mediator;

    public SolutionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("/create")]
    public async Task CreateSolution(CreateSolution.Request request, CancellationToken token)
        => await _mediator.Send(request, token);
}