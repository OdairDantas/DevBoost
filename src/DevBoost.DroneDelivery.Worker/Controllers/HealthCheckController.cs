using Microsoft.AspNetCore.Mvc;
using System;

namespace DevBoost.DroneDelivery.Worker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"{DateTime.UtcNow:o}"); 
        }
    }
}