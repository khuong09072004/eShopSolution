using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<string> Authencate(LoginRequest request)
        {
            // 1. Check user tồn tại
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return "USER_NOT_FOUND";
            }

            // 2. Check mật khẩu
            var result = await _signInManager.PasswordSignInAsync(
                user,
                request.Password,
                request.RememberMe,
                lockoutOnFailure: false
            );

            if (!result.Succeeded)
            {
                return "WRONG_PASSWORD";
            }

            // 3. Lấy roles
            var roles = await _userManager.GetRolesAsync(user);

            // 4. Tạo claims
            var claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Email, user.Email ?? ""),
        new Claim(ClaimTypes.GivenName, user.FirstName ?? ""),
        new Claim(ClaimTypes.Name, user.UserName ?? ""),
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // 5. Tạo key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Tokens:Key"])
            );
            // 6. Tạo cred
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // 7. Tạo token
            var token = new JwtSecurityToken(
                issuer: _config["Tokens:Issuer"],
                audience: _config["Tokens:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<bool> Register(ViewModels.System.Users.RegisterRequest request)
        {
            var user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }


    }
}
