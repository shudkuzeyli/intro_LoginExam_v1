using intro_LoginExam_v1.Context;
using intro_LoginExam_v1.Models;
using Microsoft.AspNetCore.Mvc;

namespace intro_LoginExam_v1.Controllers
{
	public class KullaniciController : Controller
	{

		private readonly DataContext _dataContext;

		public KullaniciController(Context.DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public IActionResult Index()
		{
			var kullanici = _dataContext.Kullanici.ToList();

			return View(kullanici);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Kullanici k)
		{
			if (k is null)
			{
				//return RedirectToAction("Index");
				return RedirectToAction(nameof(Index));
			}

			if (ModelState.IsValid == false)
			{
				return View(k);
			}
			//Eğer kullanici tarafından gönderilen Kullanici modelindeki email bilgisi veritabanında var mı? kontrolü yapıyoruz.
			//Linq -> nesneler içerisinde arama/sorgulama yapmamıza yarayan betik işlemler kümesi.

			var kullaniciListe = _dataContext.Kullanici.Where(t => t.EMail == k.EMail ).FirstOrDefault();
			if (kullaniciListe != null)
			{
				//gönerilen mail adresi zaten veritabanında varmış.
				ModelState.AddModelError("EMail", "Zaten bu mail adresi kullanılıyor");
				return View(k);
			}

			var kullaniciListe1 = _dataContext.Kullanici.Where(t => t.KullaniciKodu == k.KullaniciKodu).FirstOrDefault();
			if (kullaniciListe1 != null)
			{
				//gönerilen mail adresi zaten veritabanında varmış.
				ModelState.AddModelError("KullaniciKodu", "Bu kullanıcı kodu kullanılıyor");
				return View(k);
			}

			//_dataContext.Kullanici.Add(k);
			_dataContext.Add(k);
			_dataContext.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

	}
}
