using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Auth;

namespace RentACar.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (result.IsSuccess)
            {
                // Token ve RefreshToken'ı cookie'ye kaydet
                Response.Cookies.Append("AuthToken", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Expiration
                });

                Response.Cookies.Append("RefreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddDays(30)
                });

                ViewBag.Mail = result.Mail;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = result.ErrorMessage;
            return View();
        }
    }
}
