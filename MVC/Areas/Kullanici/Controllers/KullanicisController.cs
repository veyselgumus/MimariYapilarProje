#nullable disable
using AppCore.Results.Bases;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;
using System.Security.Claims;

namespace MVC.Areas.Kullanici.Controllers
{
	[Area("Kullanici")]
    public class KullanicisController : Controller
    {
        // Add service injections here
        private readonly IHesapService _hesapService;

        public KullanicisController(IHesapService hesapService)
        {
			_hesapService = hesapService;
        }

        // GET: Kullanici/Kullanicis/Login
        public IActionResult Login(string returnUrl)
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            HesapLoginModel kullanici = new HesapLoginModel()
            {
                ReturnUrl = returnUrl,
            };

			return View(kullanici);
        }

        // POST: Kullanici/Kullanicis/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(HesapLoginModel kullanici)
        {
            if (ModelState.IsValid)
            {
                KullaniciModel userLoginModel = new KullaniciModel();

                Result result = _hesapService.Login(kullanici, userLoginModel);

                if(result.IsSuccessful)
                {
                    List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name,userLoginModel.UserName),
                            new Claim(ClaimTypes.Role,userLoginModel.RolModel.Adi),
                            new Claim(ClaimTypes.Sid,userLoginModel.Id.ToString())
                        };

                    var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (!string.IsNullOrWhiteSpace(kullanici.ReturnUrl))
                    {
                        return Redirect(kullanici.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home", new { area = "" });
                    
                }

				ModelState.AddModelError("", result.Message);
			}
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        public async Task<IActionResult> Logout()
        { 
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult AccessDenied()
        {
            return View("Error","Access is Denied for This Page!");
        }
	}
}
