using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using USWalks.WebApp.Models;
using USWalks.WebApp.Models.DTO;

namespace USWalks.WebApp.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async  Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("http://localhost:5283/api/Regions");
                httpResponseMessage.EnsureSuccessStatusCode();
                 response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
               // ViewBag.Response = stringResponseBody;

            }
            catch (Exception)
            {

                throw;
            }


            return View(response);
        }

        [HttpGet]
        public  IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:5283/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };
           var httpResponseMessage =  await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if(response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var client = httpClientFactory.CreateClient();

           var httpsResponse =  await client.GetFromJsonAsync<RegionDTO>($"http://localhost:5283/api/Regions/{id.ToString()}");
           if(httpsResponse is not null)
            {
                return View(httpsResponse);
            }
            return View(httpsResponse);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(RegionDTO request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("http://localhost:5283/api/Regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

           var httpResponseMessage =  await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpRequestMessage.Content.ReadFromJsonAsync<RegionDTO>(); 

            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View(httpResponseMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.DeleteAsync("http://localhost:5283/api/Regions/{id}");

            return View(response);
        }
    }
}
