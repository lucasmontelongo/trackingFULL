using DataAccessLayer.DAL;
using Shared.Entities;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLPuntoControl
    {
        private DALPuntoControl _dal;

        public BLPuntoControl()
        {
            _dal = new DALPuntoControl();
        }

        public SPuntoControl getPuntoControl(int id)
        {
            try
            {
                return _dal.getPuntoControl(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPuntoControl> getAll()
        {
            return _dal.getAll();
        }

        public SPuntoControl addPuntoControl(SPuntoControl a)
        {
            try
            {
                if (a.Orden < 1)
                {
                    throw new ECompartida("El orden debe ser mayor a cero");
                }
                List<SPuntoControl> pclist = puntosControlDeUnTrayecto((int)a.IdTrayecto);
                pclist.ForEach(x =>
                {
                    if (x.Orden == a.Orden)
                    {
                        throw new ECompartida("Ya existe un punto de control con el mismo orden");
                    }
                });
                return _dal.addPuntoControl(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SPuntoControl updatePuntoControl(SPuntoControl a)
        {
            try
            {
                if (a.Orden < 1)
                {
                    throw new ECompartida("El orden debe ser mayor a cero");
                }
                List<SPuntoControl> pclist = puntosControlDeUnTrayecto((int)a.IdTrayecto);
                pclist.ForEach(x =>
                {
                    if (x.Orden == a.Orden)
                    {
                        if (x.Id != a.Id)
                        {
                            throw new ECompartida("Ya existe un punto de control con el mismo orden");
                        }
                    }
                });
                return _dal.updatePuntoControl(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deletePuntoControl(int id)
        {
            try
            {
                return _dal.deletePuntoControl(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPuntoControl> puntosControlDeUnTrayecto(int id)
        {
            try
            {
                return _dal.puntosControlDeUnTrayecto(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
