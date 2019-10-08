using DataAccessLayer.DAL;
using DataAccessLayer.Interfaces;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLEmpresa : Interfaces.IBLEmpresa
    {
        private IDALEmpresa _dal;

        public BLEmpresa()
        {
            _dal = new DALEmpresa();
        }

        public void addEmpresa(SEmpresa emp)
        {
            _dal.addEmpresa(emp);
        }
    }
}
