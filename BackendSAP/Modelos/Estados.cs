using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos
{
    public class Estados
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public ICollection<Ciudades> Ciudades { get; } = new List<Ciudades>(); // Collection navigation containing dependents
    }
}
