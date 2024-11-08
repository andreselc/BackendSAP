using BackendSAP.Modelos;

namespace BackendSAP.Repositorios.IRepositorios
{
    public interface ICalificacionesRepositorio
    {
        ICollection<Calificaciones> GetCalificaciones();
        Calificaciones GetCalificacion(int calificacionId);

        public ICollection<Calificaciones> GetCalificacionesPorPsicologo(string psicologoId);
        bool ExisteCalificacion(int id); 
        bool ExisteCalificacionDuplicada(string idUsuario, string idPsicologo);
        bool PuntajeEsCorrecto(int puntaje);
        bool CrearCalificacion(Calificaciones calificacion);
        bool ActualizarCalificacion(Calificaciones calificacion);
        bool BorrarCalificacion(Calificaciones calificacion);
        bool Guardar();
    }
}
