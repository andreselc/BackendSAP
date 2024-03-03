using BackendSAP.Data;
using BackendSAP.Modelos;
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
            _bd.TrastornosPsicologicos.Update(trastorno);
            return Guardar();
        }

        public bool BorrarTrastorno(TrastornoPsicologico trastorno)
        {
            _bd.TrastornosPsicologicos.Remove(trastorno);
            return Guardar();
        }

        public bool CrearTrastorno(TrastornoPsicologico trastorno)
        {
            _bd.TrastornosPsicologicos.Add(trastorno);
            return Guardar();
        }

        public bool ExisteTrastorno(string nombre)
        {
            bool valor = _bd.TrastornosPsicologicos.Any(c => c.Nombre.ToLower().Trim() == nombre);
            return valor;
        }

        public bool ExisteTrastorno(int id)
        {
            return _bd.TrastornosPsicologicos.Any(c => c.Id == id);
        }

        public ICollection<TrastornoPsicologico> GetTrastornos()
        {
            return _bd.TrastornosPsicologicos.OrderBy(c => c.Nombre).ToList();
        }

        public TrastornoPsicologico GetTrastorno(int trastornoId)
        {
            return _bd.TrastornosPsicologicos.FirstOrDefault(c => c.Id == trastornoId);
        }

        public ICollection<TrastornoPsicologico> BuscarTrastorno(string nombre)
        {
            IQueryable<TrastornoPsicologico> query = _bd.TrastornosPsicologicos;
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
