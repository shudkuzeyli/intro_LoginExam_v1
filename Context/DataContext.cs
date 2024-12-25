using Microsoft.EntityFrameworkCore;
using intro_LoginExam_v1.Models;

namespace intro_LoginExam_v1.Context
{
	//veritabanı erişim katmanımız
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}
		public DbSet<Kullanici> Kullanici { get; set; }
	}
}
