using AutoMapper;
using CB.Application.Abstractions.Services;
using CB.Application.Abstractions.Services.Authentication;
using CB.Application.Models.User.Authentication;
using CB.Domain.Entities.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CB.Web.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;
        private readonly IMapper _mapper;
        private readonly IWorkContext _workContext;

        public AuthenticationController(IMapper mapper, ILoginService loginService, IRegistrationService registrationService, IWorkContext workContext)
        {
            _mapper = mapper;
            _loginService = loginService;
            _registrationService = registrationService;
            _workContext = workContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser appUser = await _loginService.GetAvaibleUserAsync(loginViewModel.Email);
            if (appUser == null)
                return View();

            var signInResult = await _loginService.SignInAsync(appUser, loginViewModel.Password, loginViewModel.RememberMe);
            if (signInResult.Succeeded)
                return RedirectToAction("Index", "Dashboard");
            else
                return View();
        }

        public IActionResult Logout()
        {
            _loginService.SignOutAsync();
            return RedirectToAction("Login", "Authentication");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser appUser = _mapper.Map<AppUser>(registerViewModel);
            IdentityResult identityResult = await _registrationService.RegisterAsync(appUser);
            if (identityResult.Succeeded)
                return RedirectToAction("Login", "Authentication");
            else
                return View();
        }

        public async Task<IActionResult> Confirmation(string token)
        {
            AppUser appUser = await _workContext.GetCurrentUserAsync();
            if (appUser == null)
                return RedirectToAction("Login", "Authentication");

            IdentityResult identityResult = await _registrationService.EmailConfirmationAsync(appUser, token);
            if (identityResult.Succeeded)
                return RedirectToAction("Index", "Dashboard");
            else
                return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Reconfirmation()
        {
            AppUser appUser = await _workContext.GetCurrentUserAsync();

            if (appUser == null)
                return Json(new { success = false });

            await _registrationService.SendConfirmationAsync(appUser);
            return Json(new { success = true });
        }
    }
}
