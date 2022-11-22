using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RequestSend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        
        private static readonly string[] ColdSummaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Brass Monkeys"
        };

        private static readonly string[] WarmSummaries = new[]
        {
         "Mild", "Warm", "Balmy", "uk"
        };


        private static readonly string[] HotSummaries = new[]
        {
        "Hot", "Sweltering", "Scorching", "Inferno"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/one")]
        public IEnumerable<WeatherForecast> GetOne()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-40, 10),
                Summary = ColdSummaries[Random.Shared.Next(ColdSummaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/two")]
        public IEnumerable<WeatherForecast> GetTwo()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(10, 20),
                Summary = WarmSummaries[Random.Shared.Next(WarmSummaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/three")]
        public IEnumerable<WeatherForecast> GetThree()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(20, 55),
                Summary = HotSummaries[Random.Shared.Next(HotSummaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/remoteone")]
        public async Task<IEnumerable<WeatherForecast>> GetRemoteOne()
        {
            IEnumerable<WeatherForecast> forecast = null; 
            
            HttpResponseMessage response = await client.GetAsync(Environment.GetEnvironmentVariable("one"));
            if (response.IsSuccessStatusCode)
            {
                forecast = await response.Content.ReadFromJsonAsync< IEnumerable<WeatherForecast>> ();
            }
            return forecast;
        }

        [HttpGet("/remotetwo")]
        public async Task<IEnumerable<WeatherForecast>> GetRemoteTwo()
        {
            IEnumerable<WeatherForecast> forecast = null;

            HttpResponseMessage response = await client.GetAsync(Environment.GetEnvironmentVariable("two"));
            if (response.IsSuccessStatusCode)
            {
                forecast = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
            }
            return forecast;
        }

        [HttpGet("/remotethree")]
        public async Task<IEnumerable<WeatherForecast>> GetRemoteThree()
        {
            
            IEnumerable<WeatherForecast> forecast = null;
            HttpResponseMessage response = null;
            
            if (Environment.GetEnvironmentVariable("threenew") == "true")
            {
                HttpClient clientthree = new HttpClient();
                clientthree.DefaultRequestHeaders.Accept.Clear();
                clientthree.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await clientthree.GetAsync(Environment.GetEnvironmentVariable("three"));
                if (response.IsSuccessStatusCode)
                {
                    forecast = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
                }
            }
            else
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(Environment.GetEnvironmentVariable("three"));
            }
            if (response.IsSuccessStatusCode)
            {
                forecast = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
            }
            return forecast;
        }
    }
}