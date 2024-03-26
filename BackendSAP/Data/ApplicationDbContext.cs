using BackendSAP.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
            builder.Entity<EspecialidadPsicologo>().HasKey(e => new { e.psicologoId, e.trastornoId });

            builder.Entity<Calificaciones>().HasOne(c => c.UsuariosCalificadores)
            .WithMany(u => u.CalificacionesHechas)
            .OnDelete(DeleteBehavior.Restrict); // Especifica la acción en caso de eliminación

            builder.Entity<Calificaciones>().HasOne(c => c.UsuariosPsicologos)
            .WithMany(u => u.CalificacionesRecibidas)
            .OnDelete(DeleteBehavior.Restrict); // Especifica la acción en caso de eliminación

            builder.Entity<Calificaciones>()
            .HasIndex(c => new { c.usuarioId, c.psicologoId })
            .IsUnique();

            builder.Entity<Calificaciones>()
            .HasCheckConstraint("CK_Usuario_Psicologo_Calificacion", "usuarioId <> psicologoId");

            builder.Entity<Calificaciones>().ToTable(c => c.HasCheckConstraint("CK_puntaje", "[puntaje]  IN (1, 2, 3, 4, 5)"));
        }

        //Agregar los modelos aquí
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Ciudades> Ciudades { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<TrastornoPsicologico> TrastornoPsicologico { get; set; }
        public DbSet<Calificaciones> Calificaciones { get; set; }
    }
}
