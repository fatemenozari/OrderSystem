using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Exceptions
{
    public class OutOfRangeCount: Exception
    {
        public override string Message => " Orders should not be more than three or less than zero or zero  .";
    }
}
