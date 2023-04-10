using AutoMapper;
using CB.Application.Abstractions.Services;
using CB.Application.Abstractions.Services.Authentication;
using CB.Application.Abstractions.Services.Html;
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
        private readonly IHtmlNotificationService _htmlNotificationService;

        public AuthenticationController(IMapper mapper, ILoginService loginService, IRegistrationService registrationService, IWorkContext workContext, IHtmlNotificationService htmlNotificationService)
        {
            _mapper = mapper;
            _loginService = loginService;
            _registrationService = registrationService;
            _workContext = workContext;
            _htmlNotificationService = htmlNotificationService;
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
            {
                _htmlNotificationService.ErrorNotification("Kullanıcı adı veya şifre yanlış. Lütfen tekrar deneyiniz.");
                return View();
            }    
            var signInResult = await _loginService.SignInAsync(appUser, loginViewModel.Password, loginViewModel.RememberMe);
            if (signInResult.Succeeded)
            {
                _htmlNotificationService.SuccessNotification("Başarıyla giriş yaptınız.");
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                if (signInResult.IsLockedOut)
                    _htmlNotificationService.ErrorNotification("Çok fazla hatalı giriş yaptınız. Lütfen kısa bir süre sonra tekrar deneyiniz.");

                //todo: Add another result error

                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            AppUser appUser = await _workContext.GetCurrentUserAsync();

            if (appUser == null)
                return RedirectToAction("Login", "Authentication");

            await _loginService.SignOutAsync();
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
            {
                _htmlNotificationService.SuccessNotification("Başarıyla kayıt oldunuz! Giriş sayfasına yönlendirildiniz.");
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                _htmlNotificationService.ErrorNotification(identityResult.Errors.FirstOrDefault().Description);
                return View();
            }
        }

        public async Task<IActionResult> Confirmation(string token)
        {
            AppUser appUser = await _workContext.GetCurrentUserAsync();
            if (appUser == null)
            {
                _htmlNotificationService.ErrorNotification("Email adresinizi doğrulamak için önce giriş yapmalısınız");
                return RedirectToAction("Login", "Authentication");
            }

            IdentityResult identityResult = await _registrationService.EmailConfirmationAsync(appUser, token);
            if (identityResult.Succeeded)
                _htmlNotificationService.SuccessNotification("Email adresiniz başarıyla doğrulandı.");
            else
                _htmlNotificationService.ErrorNotification(identityResult.Errors.FirstOrDefault().Description);

                return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Reconfirmation()
        {
            AppUser appUser = await _workContext.GetCurrentUserAsync();

            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            await _registrationService.SendConfirmationAsync(appUser);
            _htmlNotificationService.SuccessNotification("Email doğrulama mesajı gönderildi! Email adresinizi kontrol ediniz.");

            string returnUrl = HttpContext.Request.Query["returnUrl"];
            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Dashboard");
        }
    }
}
