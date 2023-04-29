using MyCrudApi.models;
using Microsoft.EntityFrameworkCore;

namespace MyCrudApi.Data
{
	public class MyCrudContext : DbContext
	{
		protected override void OnModelCreating(ModelBuilder builder)
		{
			
			builder.Entity<User>(entity =>
			{
				entity.ToTable("user");
			});
		}
		public MyCrudContext(DbContextOptions<MyCrudContext> options) 
		: base(options) 
		{
		}

		public DbSet<User> User { get; set; } = null!;
	}
}
