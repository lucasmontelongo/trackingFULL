using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class ECliente : SystemException
    {
        public ECliente()
        {

        }

        public ECliente(string msg)
            : base(String.Format(msg))
        {

        }
    }
}