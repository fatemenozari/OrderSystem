using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public interface ISmsService
    {
        void SendMessage(string message, string phoneNumber);
    }
}
