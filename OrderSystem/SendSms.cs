using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    internal class SendSms : ISmsService
    {
        public void SendMessage(string message, string phoneNumber)
        {
            Console.WriteLine(" done !");
        }
    }
}
