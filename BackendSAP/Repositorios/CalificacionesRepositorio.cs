﻿using BackendSAP.Data;
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
            var calificacionExistente = _bd.Calificaciones.FirstOrDefault(c => c.Id == calificacion.Id);

            if (!string.IsNullOrEmpty(calificacion.observacion))
            {
                calificacionExistente.observacion = calificacion.observacion;
            }

            if (!string.IsNullOrEmpty(calificacion.puntaje.ToString()))
            {
                calificacionExistente.puntaje = calificacion.puntaje;
            }

            if (!string.IsNullOrEmpty(calificacion.psicologoId))
            {
                calificacionExistente.psicologoId = calificacion.psicologoId;
            }

            if (!string.IsNullOrEmpty(calificacion.usuarioId))
            {
                calificacionExistente.usuarioId = calificacion.usuarioId;
            }
            _bd.Calificaciones.Update(calificacionExistente);
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
            return _bd.Calificaciones.Any(c => c.Id == id);
        }

        public bool ExisteCalificacionDuplicada(string idUsuario, string idPsicologo)
        {
            return _bd.Calificaciones.Any(c => c.usuarioId == idUsuario && c.psicologoId == idPsicologo);
        }

        public bool PuntajeEsCorrecto(int puntaje)
        {
            return puntaje >= 1 && puntaje <= 5;
        }

        public Calificaciones GetCalificacion(int calificacionId)
        {
            return _bd.Calificaciones.FirstOrDefault(c => c.Id == calificacionId);
        }

        public ICollection<Calificaciones> GetCalificacionesPorPsicologo(string psicologoId)
        {
            return _bd.Calificaciones.Where(c => c.psicologoId == psicologoId).ToList();
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
