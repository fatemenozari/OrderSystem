using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Exceptions
{
    public class ChangeStateToShippedException: Exception
    {
        public override string Message => " This order Can not be Shipped.";

    }
}
