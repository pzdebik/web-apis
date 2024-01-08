using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FitnessTrackingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ExternalApiController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetExternalData()
        {
            try
            {
                // Pobierz dane uwierzytelniające z konfiguracji
                var externalApiBaseUrl = _configuration["ExternalApi:BaseUrl"];
                var externalApiToken = _configuration["ExternalApi:Token"];

                // Ustaw nagłówki uwierzytelniające
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", externalApiToken);

                // Wykonaj zapytanie GET do innego projektu API
                var response = await _httpClient.GetAsync($"{externalApiBaseUrl}/api/Course");

                // Sprawdź status odpowiedzi
                if (response.IsSuccessStatusCode)
                {
                    // Pobierz zawartość odpowiedzi
                    var data = await response.Content.ReadAsStringAsync();
                    return Ok(data);
                }
                else
                {
                    // Obsłuż błędy
                    var errorMessage = $"External API returned status code: {response.StatusCode}";
                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                // Obsłuż wyjątki
                return StatusCode(500, ex.Message);
            }
        }
    }
}
