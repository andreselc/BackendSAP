using BackendSAP.Data;
using BackendSAP.Modelos;
using BackendSAP.Repositorios.IRepositorios;

namespace BackendSAP.Repositorios
{
    public class EstadoRepositorio: IEstadoRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public EstadoRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarEstado(Estados estado)
        {
            _bd.Estados.Update(estado);
            return Guardar();
        }

        public bool BorrarEstado(Estados estado)
        {
            _bd.Estados.Remove(estado);
            return Guardar();
        }

        public bool CrearEstado(Estados estado)
        {
            _bd.Estados.Add(estado);
            return Guardar();
        }

        public bool ExisteEstado(string nombre)
        {
            bool valor = _bd.Estados.Any(c => c.Nombre.ToLower().Trim() == nombre);
            return valor;
        }

        public bool ExisteEstado(int estadoId)
        {
            return _bd.Estados.Any(c => c.Id == estadoId);
        }

        public Estados GetEstado(int estadoId)
        {
            return _bd.Estados.FirstOrDefault(c => c.Id == estadoId);
        }

        public ICollection<Estados> GetEstados()
        {
            return _bd.Estados.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
