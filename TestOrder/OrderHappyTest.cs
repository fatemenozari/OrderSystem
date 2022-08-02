using Xunit;
using OrderSystem;
using System.Collections.Generic;
using FluentAssertions;

namespace TestOrder
{
    public class OrderHappyTest
    {

        [Fact]
        public void Order_Should_Be_Create()
        {
            var orderItem = new OrderItem("book", 2);
            var orderList = new List<OrderItem>();
            orderList.Add(orderItem);

            var order = new Order(1, orderList);

            order.Equals(order);
        }

        [Fact]
        public void Order_TypeState_Should_Be_Created_When_Order_Created()
        {
            var orderItem = new OrderItem("book", 2);
            var orderList = new List<OrderItem>();
            orderList.Add(orderItem);
            var order = new Order(1, orderList);

            order.State.Should().Be(StateType.Created);
        }

        [Fact]
        public void Order_Deleted_When_State_Is_Created()
        {
            var orderList = new List<OrderItem>();

            var orderItem = new OrderItem("book", 2);
            orderList.Add(orderItem);

            var orderItem2 = new OrderItem("knif", 3);
            orderList.Add(orderItem2);

            var order = new OrderSystem.Order(1, orderList);

            order.Remove(orderItem);
            order.Equals(orderList);
        }

        [Fact]
        public void Order_Added_When_State_Is_Created()
        {
            var orderItem = new OrderItem("book", 1);
            var orderList = new List<OrderItem>();
            orderList.Add(orderItem);
            var order = new OrderSystem.Order(1, orderList);

            order.Add(orderList);
            order.Equals(orderList);
        }




        


    }
}
