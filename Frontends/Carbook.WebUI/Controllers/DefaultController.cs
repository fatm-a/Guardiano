using Carbook.Dto.LocationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Carbook.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
          
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7007/api/Locations");

                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);
                List<SelectListItem> values2 = (from x in values
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.LocationID.ToString()
                                                }).ToList();
                ViewBag.v = values2;
            return View();
        }
      

        [HttpPost]
        public IActionResult Index(string book_pick_date, string book_off_date, string time_pick, string time_off, string locationID)
        {
            TempData["bookpickdate"] = book_pick_date;
            TempData["bookoffdate"] = book_off_date;
            TempData["timepick"] = time_pick;
            TempData["timeoff"] = time_off;
            TempData["locationID"] = locationID;
            return RedirectToAction("Index", "RentACarList");
        }

    }
}


//    public class DefaultController : Controller
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public DefaultController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            try
//            {
//                var client = _httpClientFactory.CreateClient();
//                var responseMessage = await client.GetAsync("https://localhost:7007/api/Locations");

//                if (responseMessage.IsSuccessStatusCode)
//                {
//                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
//                    var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);

//                    var values2 = values?.Select(x => new SelectListItem
//                    {
//                        Text = x.Name,
//                        Value = x.LocationID.ToString()
//                    }).ToList() ?? new List<SelectListItem>();

//                    ViewBag.v = values2;
//                }
//                else
//                {
//                    ViewBag.v = new List<SelectListItem>();
//                    ViewBag.Error = "Lokasyon verileri alınamadı. Sunucu hatası.";
//                }
//            }
//            catch (Exception ex)
//            {
//                ViewBag.v = new List<SelectListItem>();
//                ViewBag.Error = "Beklenmeyen bir hata oluştu: " + ex.Message;
//            }

//            return View();
//        }
//    }
//}