using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace eShopSolution.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UserApiClient(IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
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

        public async Task<PagedResult<UserVm>> GetUsersPagings(GetUserPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", request.BearerToken);
            var response = await client.GetAsync($"/api/users/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine("BODY API TRẢ VỀ: " + body);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("API ERROR: " + response.StatusCode);
                return new PagedResult<UserVm> { Items = new List<UserVm>(), TotalRecord = 0 };
            }

            var users = JsonConvert.DeserializeObject<PagedResult<UserVm>>(body);
            return users ?? new PagedResult<UserVm> { Items = new List<UserVm>(), TotalRecord = 0 };
        }

        public async Task<bool> RegisterUser(RegisterRequest registerRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            

            var json = JsonConvert.SerializeObject(registerRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/users", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            

            return response.IsSuccessStatusCode;
        }
    }
}
