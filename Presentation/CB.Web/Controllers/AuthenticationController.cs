using AutoMapper;
using CB.Application.Abstractions.Services;
using CB.Application.Abstractions.Services.Authentication;
using CB.Application.Abstractions.Services.Html;
using CB.Application.Abstractions.Services.Membership;
using CB.Application.Abstractions.Services.Security;
using CB.Application.Models.User.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CB.Domain.Entities.Membership;

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
        private readonly IEncryptionService _encryptionService;
        private readonly IUserService _userService;

        public AuthenticationController(IMapper mapper, ILoginService loginService, IRegistrationService registrationService, IWorkContext workContext, IHtmlNotificationService htmlNotificationService, IEncryptionService encryptionService, IUserService userService)
        {
            _mapper = mapper;
            _loginService = loginService;
            _registrationService = registrationService;
            _workContext = workContext;
            _htmlNotificationService = htmlNotificationService;
            _encryptionService = encryptionService;
            _userService = userService;
        }

        public async Task<IActionResult> Login()
        {
            if (await _workContext.GetCurrentUserAsync() != null)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            User user = await _userService.GetAvaibleUserByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                _htmlNotificationService.ErrorNotification("Kullanıcı adı veya şifre yanlış. Lütfen tekrar deneyiniz.");
                return View();
            }    
            var signInResult = await _loginService.SignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe);
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
            User user = await _workContext.GetCurrentUserAsync();

            if (user == null)
                return RedirectToAction("Login", "Authentication");

            await _loginService.SignOutAsync();
            return RedirectToAction("Login", "Authentication");
        }

        public async Task<IActionResult> Register()
        {
            if (await _workContext.GetCurrentUserAsync() != null)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            User user = _mapper.Map<User>(registerViewModel);
            IdentityResult identityResult = await _registrationService.RegisterAsync(user);
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
            User user = await _workContext.GetCurrentUserAsync();
            if (user == null)
            {
                _htmlNotificationService.ErrorNotification("Email adresinizi doğrulamak için önce giriş yapmalısınız");
                return RedirectToAction("Login", "Authentication");
            }

            IdentityResult identityResult = await _registrationService.EmailConfirmationAsync(user, token);
            if (identityResult.Succeeded)
                _htmlNotificationService.SuccessNotification("Email adresiniz başarıyla doğrulandı.");
            else
                _htmlNotificationService.ErrorNotification(identityResult.Errors.FirstOrDefault().Description);

                return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Reconfirmation()
        {
            User user = await _workContext.GetCurrentUserAsync();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _registrationService.SendConfirmationAsync(user);
            _htmlNotificationService.SuccessNotification("Email doğrulama mesajı gönderildi! Email adresinizi kontrol ediniz.");

            string returnUrl = HttpContext.Request.Query["returnUrl"];
            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> ForgotPassword()
        {
            if (await _workContext.GetCurrentUserAsync() != null)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("Email", "Email adresi boş geçilemez");
                return View();
            }

            User user = await _userService.GetAvaibleUserByEmailAsync(email);

            if (user == null)
            {
                _htmlNotificationService.ErrorNotification("Bu mail adresine kayıtlı kullanıcı bulunamadı.");
                return View();
            }
                
            await _registrationService.SendPasswordRecoveryAsync(user);

            _htmlNotificationService.SuccessNotification("Şifre sıfırlama bağlantısı gönderildi. Lütfen email adresinizi kontrol ediniz.");

            return View();
        }

        public async Task<IActionResult> PasswordRecovery(string token,string uid)
        {
            PasswordRecoveryViewModel passwordRecoveryViewModel = new()
            {
                EncryptedUserId = uid,
                PasswordRecoveryToken = token,
            };
            return View(passwordRecoveryViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> PasswordRecovery(PasswordRecoveryViewModel passwordRecoveryViewModel)
        {
            if (!ModelState.IsValid)
                return View(passwordRecoveryViewModel);

            int userId = Convert.ToInt32(_encryptionService.Decrypt(passwordRecoveryViewModel.EncryptedUserId));
            User user = await _userService.GetAvaibleUserByIdAsync(userId);
            if(user == null)
            {
                _htmlNotificationService.ErrorNotification("Kullanıcı bulunamadı. Lütfen tekrar şifre sıfırlama isteği alınız!");
                return RedirectToAction("ForgotPassword", "Authentication");
            }
            IdentityResult identityResult = await _registrationService.PasswordRecoveryAsync(user, passwordRecoveryViewModel.PasswordRecoveryToken, passwordRecoveryViewModel.Password);
            if(identityResult.Succeeded)
            {
                _htmlNotificationService.SuccessNotification("Şifre sıfırlama başarılı! Lütfen yeni şifreniz ile giriş yapınız.");
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                _htmlNotificationService.ErrorNotification(identityResult.Errors.FirstOrDefault().Description);
                return View(passwordRecoveryViewModel);
            }
        }

    }
}
