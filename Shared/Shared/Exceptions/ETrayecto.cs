﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class ETrayecto : SystemException
    {
        public ETrayecto()
        {

        }

        public ETrayecto(string msg)
            :base(String.Format(msg))
        {

        }
    }
}
