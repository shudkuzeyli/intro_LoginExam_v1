using System.ComponentModel.DataAnnotations;

namespace intro_LoginExam_v1.Models
{
	public class Kullanici : BaseObject
	{
		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[Display(Name = "Ad Soyad")]//grid de görünecek ya da edit sayfasında görünecek label üzerinde yazan yazı
		[StringLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir")]
		public string AdSoyad { get; set; }

		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[Display(Name = "E-Mail")]
		[StringLength(50, ErrorMessage = "{0} adresi en fazla {1} karakterden oluşabilir.")]
		//	[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string EMail { get; set; }

		[Display(Name = "Kullanıcı Kodu")]
		[Required(ErrorMessage = "{0} alanı boş olamaz.")]
		[StringLength(10, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir.")]

		public string KullaniciKodu { get; set; }

		[Display(Name = "Şifre")]
		[Required(ErrorMessage = "{0} alanı boş olamaz.")]
		[DataType(DataType.Password)]
		//en az 2 rakam, maks 10 elamn, min 1 tane özel , min 1 büyük harf
		[RegularExpression(@"^(?=(.*\d){2,})(?=.*[\W_])(?=.*[A-Z]).{1,10}$",ErrorMessage ="Kkullanıcı şifre hatalı")]
		[StringLength(10, ErrorMessage ="{0} alanı {1} karakterden az olamaz.")]
		public string KullaniciSifre { get; set; }

		[RegularExpression("^[0-9]+$", ErrorMessage ="{0} alanına sadece sayısal değer yazabilirsiniz.")]
		public string Yas { get; set; }//565464654


	}
}
