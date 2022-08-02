using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Exceptions
{
    public class OutOfRangeRemoveItemException: Exception
    {
        public override string Message => " The order quantity is less than one item";

    }
}
