using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Services
    {
        private readonly ISmsService _smsService;
        private readonly IOrderRepository _orderRepository;

        public Services(ISmsService smsService , IOrderRepository orderRepository )
        {
            _smsService = smsService;
            _orderRepository = orderRepository;
        }
        public void GetOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            IsInvalidOrderId(order);
        }
        private static void IsInvalidOrderId(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);
        }

        public void Completed()
        {
            _smsService.SendMessage("Done !", "0919");
        }
    }
}
