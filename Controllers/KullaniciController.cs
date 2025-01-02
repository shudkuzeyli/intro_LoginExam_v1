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
	}
}
