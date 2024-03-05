using BackendSAP.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BackendSAP.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuarios>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EspecialidadPsicologo>()
        .HasKey(e => new { e.psicologoId, e.trastornoId });
        }

        //Agregar los modelos aquí
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Ciudades> Ciudades { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<TrastornoPsicologico> TrastornoPsicologico { get; set; }
    }
}
