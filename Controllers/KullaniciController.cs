using intro_LoginExam_v1.Context;
using intro_LoginExam_v1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

		/// <summary>
		/// Edit işleminin yapılacağı sayfayı son kullanıcının önüne çıkarmak için gerekli fonksiyon
		/// </summary>
		/// <param name="id">Editlenecek kullanıcının primary Key'i</param>
		/// <returns>ekrana, web sayfası içinde kullanıcı bilgilerini atması/fırlatması gerekiyor.</returns>
		public IActionResult Edit(int? id)
		{
			if (id is null)
				return RedirectToAction(nameof(Index));

			var edit = _dataContext.Kullanici.Where(e => e.Id == id).FirstOrDefault();
			if (edit is null)
				return RedirectToAction(nameof(Index));

			return View(edit);
		}

		[HttpPost]
		public IActionResult Edit(Kullanici k, int? id)
		{

			#region 1. YOl
			//if (id is null)
			//	return RedirectToAction(nameof(Index));

			//if (k is null)
			//{
			//	return RedirectToAction(nameof(Index));
			//}

			//if (ModelState.IsValid == false)
			//{
			//	return View(k);
			//}

			//var kullaniciListe = _dataContext.Kullanici.Where(t => t.EMail == k.EMail).FirstOrDefault();
			//if (kullaniciListe != null)
			//{
			//	//gönerilen mail adresi zaten veritabanında varmış.
			//	ModelState.AddModelError("EMail", "Zaten bu mail adresi kullanılıyor");
			//	return View(k);
			//}

			//var kullaniciListe1 = _dataContext.Kullanici.Where(t => t.KullaniciKodu == k.KullaniciKodu).FirstOrDefault();
			//if (kullaniciListe1 != null)
			//{
			//	//gönerilen mail adresi zaten veritabanında varmış.
			//	ModelState.AddModelError("KullaniciKodu", "Bu kullanıcı kodu kullanılıyor");
			//	return View(k);
			//}

			//var dbuser = _dataContext.Kullanici.Where()

			//return View(edit); 
			#endregion

			if (id is null)
				return RedirectToAction(nameof(Index));

			if (k.Id != id)
				return RedirectToAction(nameof(Index));

			if (_dataContext.Kullanici.Any(e => e.Id == id) == false)
				return RedirectToAction(nameof(Index));

			if (_dataContext.Kullanici.Any(t => t.EMail == k.EMail.Trim() && t.Id != k.Id))
			{
				ModelState.AddModelError("EMail", "Güncellemek istediğiniz email daha önce kayıt edilmiş.");
				return View(k);
			}

			if (ModelState.IsValid)
			{
				try
				{
					var dbdekiData = _dataContext.Kullanici.FirstOrDefault(e => e.Id == id);

					dbdekiData.AdSoyad = k.AdSoyad;
					dbdekiData.EMail = k.EMail;
					dbdekiData.KullaniciKodu = k.KullaniciKodu;
					dbdekiData.KullaniciSifre = k.KullaniciSifre;
					dbdekiData.Yas = k.Yas;
					dbdekiData.Aktif = k.Aktif;

					_dataContext.Update(dbdekiData);
					_dataContext.SaveChanges();
				}
				catch (Exception e)
				{
					//loga yazılacak.
					ModelState.AddModelError("AdSoyad", $"Inner Exep. : {e.Message}");
					return View(k);
				}
			}

			return RedirectToAction(nameof(Index));
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

			var kullaniciListe = _dataContext.Kullanici.Where(t => t.EMail == k.EMail).FirstOrDefault();
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

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id is null)
				return RedirectToAction(nameof(Index));

			var edit = _dataContext.Kullanici.Where(e => e.Id == id).FirstOrDefault();
			if (edit is null)
				return RedirectToAction(nameof(Index));

			return View(edit);
		}

		[HttpPost, ActionName("DeleteConfirm")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirm(int? id)
		{
			if (id != null)

			{
				try
				{
					var dbdekiData = _dataContext.Kullanici.FirstOrDefault(e => e.Id == id);
					if (dbdekiData is null)
					{
						return RedirectToAction(nameof(Index));
					}

					_dataContext.Remove(dbdekiData);
					_dataContext.SaveChanges();

					return RedirectToAction(nameof(Index));
				}
				catch (Exception e)
				{
					//loga yazılacak.
					ModelState.AddModelError("AdSoyad", $"Inner Exep. : {e.Message}");
					return View();
				}
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Detail(int? id)
		{
			if (id is null)
				return RedirectToAction(nameof(Index));

			var edit = _dataContext.Kullanici.Where(e => e.Id == id).FirstOrDefault();
			if (edit is null)
				return RedirectToAction(nameof(Index));

			return View(edit);
		}
	}
}
