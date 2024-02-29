using System.ComponentModel.DataAnnotations;

namespace BackendSAP.Modelos
{
    public class Estados
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
