using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        public UserController(IUserApiClient userApiClient ,IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var sessions = HttpContext.Session.GetString("Token");
            if (string.IsNullOrWhiteSpace(sessions))
            {
                return RedirectToAction("Index", "Login");
            }

            var request = new GetUserPagingRequest()
            {
                BearerToken = sessions,
                Keyword = "",
                PageIndex = 1,
                PageSize = 10
            };
            var data = await _userApiClient.GetUsersPagings(request);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var result = await _userApiClient.RegisterUser(request);
            if(result)
            {
               return RedirectToAction("Index");
            }
            return View(request);
        }


        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
        


    }
}
