using BackendSAP.Modelos;

namespace BackendSAP.Repositorios.IRepositorios
{
    public interface IEstadoRepositorio
    {
        ICollection<Estados> GetEstados();
        Estados GetEstado(int estadoId);
        bool ExisteEstado(string nombre);
        bool ExisteEstado(int id);
        bool CrearEstado(Estados estado);
        bool ActualizarEstado(Estados estado);
        bool BorrarEstado(Estados estado);
        bool Guardar();
    }
}
