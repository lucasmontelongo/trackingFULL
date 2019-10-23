using DataAccessLayer.DAL;
using QRCoder;
using Shared.Entities;
using Shared.Exceptions;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLPaquete
    {
        private DALPaquete _dal;

        public BLPaquete()
        {
            _dal = new DALPaquete();
        }

        public SPaquete getPaquete(int id)
        {
            try
            {
                return _dal.getPaquete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPaquete> getAll()
        {
            return _dal.getAll();
        }

        public SPaquete addPaquete(SPaquete a)
        {
            try
            {
                a.Id = 0;
                a.FechaIngreso = DateTime.Now;
                a.FechaEntrega = DateTime.Now;
                a.CodigoConfirmacion = Randoms.RandomString(6);
                a.Codigo = "";
                a.Borrado = false;
                a.ListaPaquetePuntoControl = null;
                SPaquete p = _dal.addPaquete(a);
                if (p != null)
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(p.Id.ToString(), QRCodeGenerator.ECCLevel.Q);
                    Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                    p.Codigo = qrCode.GetGraphic(20);
                    return _dal.updatePaquete(p);
                }
                throw new ECompartida("Algun error raro en a;adir el paquete"); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SPaquete updatePaquete(SPaquete a)
        {
            try
            {
                return _dal.updatePaquete(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deletePaquete(int id)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
