using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDALAgencia
    {
        void addAgencia(SAgencia ag);

        void deleteAgencia(int id);

        void updateAgencia(SAgencia ag);

        List<SAgencia> getAllAgencias();

        SAgencia getAgencia(int id);
    }
}
