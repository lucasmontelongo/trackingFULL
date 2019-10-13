using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IDALUsuario
    {
        string addUsuario(SUsuario u);
    }
}
