using eShopSolution.ViewModels.System.Users;
using Newtonsoft.Json;

namespace eShopSolution.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Authencate(LoginRequest request)
        {
            var json=JsonConvert.SerializeObject(request);
            var httpContent=new StringContent(json,System.Text.Encoding.UTF8,"application/json");

            var client =_httpClientFactory.CreateClient();
            client.BaseAddress= new Uri("https://localhost:7251");
            var response = await client.PostAsync("/api/users/authenticate", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<dynamic>(result);
            return (string)obj.token;    // trả về chỉ token
        }
    }
}
