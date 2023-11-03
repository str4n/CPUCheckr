using Microsoft.AspNetCore.Mvc;

namespace CPUCheckr.Controllers;

public sealed class HomeController : BaseController
{
    [HttpGet]
    public string Get() => "CpuCheckr API";
}