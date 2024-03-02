using BackendSAP.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BackendSAP.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Agregar los modelos aquí
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Ciudades> Ciudades { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
