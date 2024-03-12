using BackendSAP.Data;
using BackendSAP.Modelos;
using BackendSAP.Modelos.Dtos.Trastornos;
using BackendSAP.Repositorios.IRepositorios;

namespace BackendSAP.Repositorios
{
    public class TrastornoRepositorio : ITrastornoRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public TrastornoRepositorio (ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarTrastorno(TrastornoPsicologico trastorno)
        {
            var trastornoExistente = _bd.TrastornoPsicologico.FirstOrDefault(t => t.Id == trastorno.Id);

            // Actualiza solo los campos proporcionados en el DTO
            if (!string.IsNullOrEmpty(trastorno.Nombre))
            {
                trastornoExistente.Nombre = trastorno.Nombre;
            }

            if (!string.IsNullOrEmpty(trastorno.Descripcion))
            {
                trastornoExistente.Descripcion = trastorno.Descripcion;
            }

            if (!string.IsNullOrEmpty(trastorno.Causas))
            {
                trastornoExistente.Causas = trastorno.Causas;
            }

            if (!string.IsNullOrEmpty(trastorno.Sintomas))
            {
                trastornoExistente.Sintomas = trastorno.Sintomas;
            }

            trastornoExistente.FechaPublicacion = trastorno.FechaPublicacion;

            _bd.TrastornoPsicologico.Update(trastornoExistente);
            return Guardar();
        }

        public bool BorrarTrastorno(TrastornoPsicologico trastorno)
        {
            _bd.TrastornoPsicologico.Remove(trastorno);
            return Guardar();
        }

        public bool CrearTrastorno(TrastornoPsicologico trastorno)
        {
            trastorno.FechaPublicacion = DateTime.Now;
            _bd.TrastornoPsicologico.Add(trastorno);
            return Guardar();
        }

        public bool ExisteTrastorno(string nombre)
        {
            bool valor = _bd.TrastornoPsicologico.Any(t => t.Nombre.ToLower().Trim() == nombre);
            return valor;
        }

        public bool ExisteTrastorno(int id)
        {
            return _bd.TrastornoPsicologico.Any(t => t.Id == id);
        }

        public ICollection<TrastornoPsicologico> GetTrastornos()
        {
            return _bd.TrastornoPsicologico.OrderBy(t => t.Nombre).ToList();
        }

        public TrastornoPsicologico GetTrastorno(int trastornoId)
        {
            return _bd.TrastornoPsicologico.FirstOrDefault(t => t.Id == trastornoId);
        }

        public ICollection<TrastornoPsicologico> BuscarTrastorno(string nombre)
        {
            IQueryable<TrastornoPsicologico> query = _bd.TrastornoPsicologico;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(t => t.Nombre.Contains(nombre));
            }
            return query.ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
