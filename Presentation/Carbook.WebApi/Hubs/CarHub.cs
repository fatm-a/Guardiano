using Microsoft.AspNetCore.SignalR;

namespace Carbook.WebApi.Hubs
{
    public class CarHub: Hub// SignalR Hub sınıfı: CarHub, istemcilerle gerçek zamanlı iletişim sağlar.
    {
        private readonly IHttpClientFactory _httpClientFactory;// HTTP istekleri yapmak için kullanılan IHttpClientFactory bağımlılığı.

        public CarHub(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task SendCarCount()//sendcarcount resevecarcount ile erişim // İstemcilerden çağrılabilecek bir SignalR metodu.
                                        // Amaç: API'den araç sayısını çekip tüm bağlı istemcilere göndermek.
        {
            var client = _httpClientFactory.CreateClient();// API'ye GET isteği gönderilir.
            var responseMessage = await client.GetAsync("https://localhost:7007/api/Statistics/GetCarCount");
            var value = await responseMessage.Content.ReadAsStringAsync();// Gelen içerik (araç sayısı) okunur.
            await Clients.All.SendAsync("ReceiveCarCount", value); // Tüm bağlı istemcilere "ReceiveCarCount" olayı ile veri gönderilir.
        }
    }
}