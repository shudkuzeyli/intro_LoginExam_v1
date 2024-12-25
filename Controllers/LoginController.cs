using intro_LoginExam_v1.Context;
using intro_LoginExam_v1.Models;
using Microsoft.AspNetCore.Mvc;

namespace intro_LoginExam_v1.Controllers
{
	public class LoginController : Controller
	{
		private readonly DataContext _dataContext;

		public LoginController(Context.DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(Kullanici model)
		{
			var kullanici = _dataContext.Kullanici.Where(t => t.KullaniciKodu == model.KullaniciKodu && t.KullaniciSifre == model.KullaniciSifre).FirstOrDefault();
			if (kullanici == null)
			{
				ModelState.AddModelError(nameof(Kullanici.KullaniciSifre), "Kullanıcı kodu veya şifreniz hatalı");
				return View(model);
			}

			//return RedirectToAction("Index", "Home");

			return RedirectToAction("Index", new RouteValueDictionary(new { controller="Home", action="Index", id=model.Id}));
		}

	}
}
