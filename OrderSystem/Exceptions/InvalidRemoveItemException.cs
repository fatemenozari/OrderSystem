using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Exceptions
{
    public class InvalidRemoveItemException : Exception
    {
        public override string Message => " The order cannot be deleted .";
    }
}
