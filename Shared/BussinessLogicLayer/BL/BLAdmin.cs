using DataAccessLayer.DAL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLAdmin
    {
        private DALAdmin _dal;

        public BLAdmin()
        {
            _dal = new DALAdmin();
        }

        public bool add(string email, string role)
        {
            try
            {
                return _dal.add(email, role);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EstadisticaDTO> trayectoPaquete()
        {
            try
            {
                return _dal.trayectoPaquete();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EstadisticaDTO> paquetesIngresadosEntregados(string tipo)
        {
            try
            {
                return _dal.paquetesIngresadosEntregados(tipo);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
