using Microsoft.AspNetCore.Mvc;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MainController: ControllerBase
{
    protected OkObjectResult Ok<TObject>(TObject? @object = null)
        where TObject : class
    {
        return new OkObjectResult(Envelope.Successful(@object))
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    protected OkObjectResult Created<TObject>(TObject? @object)
    {
        return new OkObjectResult(Envelope.Successful(@object))
        {
            StatusCode = StatusCodes.Status201Created
        };
    }

    protected new OkObjectResult NoContent()
    {
        return new OkObjectResult(Envelope.Successful(true))
        {
            StatusCode = StatusCodes.Status204NoContent
        };
    }
}
