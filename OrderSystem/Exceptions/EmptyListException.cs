﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Exceptions
{
    public class EmptyListException: Exception
    {
        public override string Message => " List Is Null Or Empty .";

    }
}
