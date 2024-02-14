using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Abstractions;

[ApiController]
[Route("api/")]
[Produces("application/json")]
public abstract class ApiController(ISender _sender) : ControllerBase;