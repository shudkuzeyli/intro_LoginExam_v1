using intro_LoginExam_v1.Context;
using intro_LoginExam_v1.Models;

namespace intro_LoginExam_v1.Business
{
	public class Userservices
	{
		private readonly DataContext _dataContext;

		public Userservices(Context.DataContext dataContext)
		{
			_dataContext = dataContext;


		}
		public Kullanici GetKullanici(Kullanici user)
		{
			Kullanici kullanici = new Kullanici();
			kullanici = _dataContext.Kullanici.Where(x => x.KullaniciKodu == user.KullaniciKodu && x.KullaniciSifre == user.KullaniciSifre).FirstOrDefault();
			return kullanici;
		}
	}
}

