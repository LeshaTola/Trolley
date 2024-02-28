using Microsoft.EntityFrameworkCore;
using TrolleyAPI.DataLayer.DTO;

namespace TrolleyAPI.DataLayer
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Choice> Choices { get; set; } = null!;

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

	}
}
