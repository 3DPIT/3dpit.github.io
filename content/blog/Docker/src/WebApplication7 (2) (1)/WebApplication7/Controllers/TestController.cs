using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }



        [HttpGet(Name = "GetTest")]
        public bool Test()
        {
            for (var j = 0; j < 1000; j++)
            {
                for (var i = 0; i < 20000; i++)
                {
                    Console.WriteLine($@"{j}_{i}");
                }
            }
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();

            return false;
        }
    }
}