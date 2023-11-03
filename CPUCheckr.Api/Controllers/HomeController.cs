using Microsoft.AspNetCore.Mvc;

namespace CPUCheckr.Controllers;

public sealed class HomeController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string Get() => "CpuCheckr API";
}