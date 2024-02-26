using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Choise> Choises { get; set; } = null!;

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Choise>().HasData(
				new() { Id = 1, Level = 1, Value = 1 },
				new() { Id = 2, Level = 2, Value = 2 },
				new() { Id = 3, Level = 3, Value = 3 }
			);
		}
	}
}
