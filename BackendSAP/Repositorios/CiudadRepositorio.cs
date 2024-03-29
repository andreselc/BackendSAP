﻿using BackendSAP.Data;
using BackendSAP.Modelos;
using BackendSAP.Repositorios.IRepositorios;
using Microsoft.EntityFrameworkCore;

namespace BackendSAP.Repositorios
{
    public class CiudadRepositorio : ICiudadRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public CiudadRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCiudad(Ciudades ciudad)
        {
            _bd.Ciudades.Update(ciudad);
            return Guardar();
        }

        public bool BorrarCiudad(Ciudades ciudad)
        {
            _bd.Ciudades.Remove(ciudad);
            return Guardar();
        }

        public bool CrearCiudad(Ciudades ciudad)
        {
            _bd.Ciudades.Add(ciudad);
            return Guardar();
        }

        public bool ExisteCiudad(string nombre)
        {
            bool valor = _bd.Ciudades.Any(c => c.Nombre.ToLower().Trim() == nombre);
            return valor;
        }

        public bool ExisteCiudad(int ciudadId)
        {
            return _bd.Ciudades.Any(c => c.Id == ciudadId);
        }

        public Ciudades GetCiudad(int ciudadId)
        {
            return _bd.Ciudades.FirstOrDefault(c => c.Id == ciudadId);
        }

        public ICollection<Ciudades> GetCiudades()
        {
            return _bd.Ciudades.OrderBy(c => c.Nombre).ToList();
        }

        public ICollection<Ciudades> GetCiudadesEnEstado(int estId)
        {
            return _bd.Ciudades.Include(es => es.Estados).Where(est => est.Id== estId).ToList();
        }

        public ICollection<Ciudades> BuscarCiudad(string nombre)
        {
            IQueryable<Ciudades> query = _bd.Ciudades;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Nombre.Contains(nombre));
            }
            return query.ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
