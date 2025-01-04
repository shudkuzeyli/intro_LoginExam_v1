using intro_LoginExam_v1.Business;
using intro_LoginExam_v1.Context;
using intro_LoginExam_v1.Models;
using Microsoft.AspNetCore.Mvc;

namespace intro_LoginExam_v1.Controllers
{
	public class LoginController : Controller
	{
		private readonly DataContext _dataContext;
		//private readonly Userservices _userservices;
		public LoginController(Context.DataContext dataContext)
		//public LoginController(Context.DataContext dataContext, Userservices userservices)
		{
			_dataContext = dataContext;
			//_userservices = userservices;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(Kullanici model)
		{
			var kullanici = _dataContext.Kullanici.Where(t => t.KullaniciKodu == model.KullaniciKodu && t.KullaniciSifre == model.KullaniciSifre).FirstOrDefault();
			//Userservices Mahmut = new Userservices(_dataContext);
			//var kullanici = _userservices.GetKullanici(model);
			if (kullanici == null)
			{
				ModelState.AddModelError(nameof(Kullanici.KullaniciSifre), "Kullanıcı kodu veya şifreniz hatalı");
				return View(model);
			}
			//return RedirectToAction("Index", "Home");
			return RedirectToAction("Index", new RouteValueDictionary(new { controller="kullanici", action="Index", id=model.Id}));
		}
	}
}
