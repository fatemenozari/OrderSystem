using OrderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOrder.Builders
{
    public class OrderBuilder
    {
        private int _userId;
        private List<OrderItem> _orderItems = new();
        OrderItem orderItem = new OrderItemBuilder().Build();

        public OrderBuilder()
        {
            _userId = 1;
            _orderItems.Add(orderItem);
        }

        public OrderBuilder WithUserId(int user)
        {
            _userId = user;
            return this;
        }
        public OrderBuilder WithOrderItems(List<OrderItem> orderItem)
        {
            _orderItems = orderItem;
            return this;
        }
        public OrderBuilder WithOrderItem(OrderItem orderItem)
        {
            this.orderItem = orderItem;
            return this;
        }

        public Order Build()
        {
            var order = new Order(_userId, _orderItems);
            return order;
        }
    }
}
