using BackendSAP.Modelos;

namespace BackendSAP.Repositorios.IRepositorios
{
    public interface ITrastornoRepositorio
    {
        ICollection<TrastornoPsicologico> GetTrastorno();
        TrastornoPsicologico GetTrastorno(int trastornoId);
        bool ExisteTrastorno(string nombre);
        bool ExisteTrastorno(int id);
        bool CrearTrastorno(TrastornoPsicologico trastorno);
        bool ActualizarTrastorno(TrastornoPsicologico trastorno);
        bool BorrarTrastorno(TrastornoPsicologico trastorno);
        bool Guardar();
    }
}
