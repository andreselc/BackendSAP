using BackendSAP.Data;
using BackendSAP.Modelos;
using BackendSAP.Repositorios.IRepositorios;

namespace BackendSAP.Repositorios
{
    public class CalificacionesRepositorio : ICalificacionesRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public CalificacionesRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCalificacion(Calificaciones calificacion)
        {
            _bd.Calificaciones.Update(calificacion);
            return Guardar();
        }

        public bool BorrarCalificacion(Calificaciones calificacion)
        {
            _bd.Calificaciones.Remove(calificacion);
            return Guardar();
        }

        public bool CrearCalificacion(Calificaciones calificacion)
        {
            _bd.Calificaciones.Add(calificacion);
            return Guardar();
        }

        public bool ExisteCalificacion(int id)
        {
            return _bd.Estados.Any(c => c.Id == id);
        }

        public Calificaciones GetCalificacion(int calificacionId)
        {
            return _bd.Calificaciones.FirstOrDefault(c => c.Id == calificacionId);
        }

        public ICollection<Calificaciones> GetCalificaciones()
        {
            return _bd.Calificaciones.OrderBy(c => c.puntaje).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
