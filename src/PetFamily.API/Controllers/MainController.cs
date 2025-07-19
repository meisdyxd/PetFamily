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
            StatusCode = 200
        };
    }

    protected OkObjectResult Created<TObject>(TObject? @object)
    {
        return new OkObjectResult(Envelope.Successful(@object))
        {
            StatusCode = 201
        };
    }
}
