using Microsoft.AspNetCore.Mvc;

namespace src.API.Controllers
{
    /// <summary>
    /// The base API controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
    }
}