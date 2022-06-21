using Microsoft.EntityFrameworkCore;

namespace DeltaTre.Service.WordApi.Data
{
	public class WordContext : DbContext
	{
		public virtual DbSet<Word> Words { get; set; }

		public WordContext() { }

		public WordContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Word>().HasKey(b => b.Id);
		}
	}
}

