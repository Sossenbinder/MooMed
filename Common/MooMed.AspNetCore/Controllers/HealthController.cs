using Microsoft.AspNetCore.Mvc;

namespace MooMed.AspNetCore.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HealthController : ControllerBase
	{
		[HttpGet]
		[Route("Ping")]
		public StatusCodeResult Ping()
		{
			return Ok();
		}
	}
}
