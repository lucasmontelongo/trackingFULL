using DataAccessLayer.DAL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLTrayecto
    {
        private DALTrayecto _dal;

        public BLTrayecto()
        {
            _dal = new DALTrayecto();
        }

        public STrayecto getTrayecto(int id)
        {
            try
            {
                return _dal.getTrayecto(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<STrayecto> getAll()
        {
            return _dal.getAll();
        }

        public STrayecto addTrayecto(STrayecto a)
        {
            try
            {
                //a.validation(); //falta comprobar que no esten duplicadas las agencias y eso, y probablemente mas cosas tambien xD
                return _dal.addTrayecto(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public STrayecto updateTrayecto(STrayecto a)
        {
            try
            {
                return _dal.updateTrayecto(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deleteTrayecto(int id)
        {
            try
            {
                return _dal.deleteTrayecto(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int paquetesTransito(STrayecto a)
        {
            try
            {
                return _dal.paquetesTransito((int)a.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool paquetesEnTransito(int id)
        {
            try
            {
                BLPaquete _blP = new BLPaquete();
                foreach (var item in _blP.getAll())
                {
                    if (item.FechaIngreso == item.FechaEntrega && item.IdTrayecto == id)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool validarTrayecto(int id)
        {
            try
            {
                List<SPuntoControl> puntoscontrol = new BLPuntoControl().puntosControlDeUnTrayecto(id);
                foreach (var item in puntoscontrol)
                {
                    if (item.Orden == 1 && item.IdAgencia == null)
                    {
                        return false;
                    }
                    if (item.Orden == (puntoscontrol.Count - 1) && item.IdAgencia == null)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
