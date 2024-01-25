using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Examen_DAW.Models;

namespace Examen_DAW.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		public DbSet<Comenzi> Comenzi { get; set; }
		public DbSet<Produse> Produse { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Comenzi>().HasOne(ac => ac.Produse).WithMany(ac => ac.Comenzi).HasForeignKey(ac => ac.ProduseId);
		}
	}
}