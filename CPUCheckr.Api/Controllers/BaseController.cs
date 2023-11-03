using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace CPUCheckr.Controllers;

[ApiController]
[Route("/api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        if (model is IEnumerable & model is not string)
        {
            if (!((IEnumerable)model).GetEnumerator().MoveNext())
            {
                return NotFound();
            }
        }
            
        return Ok(model);
    }
}