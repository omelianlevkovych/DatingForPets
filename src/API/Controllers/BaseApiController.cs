using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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