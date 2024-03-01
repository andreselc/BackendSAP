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

        //Métodos para buscar ciudades en estados y buscar ciudad por nombre
        ICollection<Ciudades> GetCiudadesEnEstado(int ciuId);
        ICollection<Ciudades> BuscarCiudad(string nombre);
        bool Guardar();
    }
}
