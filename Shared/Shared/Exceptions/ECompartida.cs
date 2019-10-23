using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class ECompartida : SystemException
    {
        public ECompartida()
        {

        }

        public ECompartida(string msg)
            : base(String.Format(msg))
        {

        }
    }
}
