using CPUCheckr.Core.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CPUCheckr.Controllers;

public sealed class HomeController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultContentType("text/plain")]
    public string Get() => "CpuCheckr API";
}