using BackendSAP.Modelos;

namespace BackendSAP.Repositorios.IRepositorios
{
    public interface ICiudadRepositorio
    {
        ICollection<Ciudades> GetCiudades();
        Ciudades GetCiudad(int ciudadId);
        bool ExisteCiudad(string nombre);
        bool ExisteCiudad(int id);
        bool CrearCiudad(Ciudades ciudad);
        bool ActualizarCiudad(Ciudades ciudad);
        bool BorrarCiudad(Ciudades ciudad);
        bool Guardar();
    }
}
