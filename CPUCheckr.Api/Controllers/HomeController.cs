using Microsoft.AspNetCore.Mvc;

namespace CPUCheckr.Controllers;

public sealed class HomeController : BaseController
{
    public string Get() => "CpuCheckr API";
}