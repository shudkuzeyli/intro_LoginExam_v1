using Microsoft.AspNetCore.Cors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace intro_LoginExam_v1.Models
{
	public class BaseObject
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("Sıra No")]
		public int Id { get; set; }

		[DisplayName("Durum")]
		[Required(ErrorMessage = "Seçiniz..")]
		public bool Aktif { get; set; }

		[ScaffoldColumn(false)]//Tablo üzerinde referans gösterilen bir alanın üst modele bağlanmayacağını söylüyoruz.(Not: Biz istediğimiz zaman model üzerinden bu veriyi alabiliyor olacağız)
		[Browsable(false)]//bu model, bir grid ya da table a datasource oalrak bağlanmışsa arayüzde göster/gösterme demek
		public string AktifString => Aktif ? "Aktif" : "Pasif";
	}
}