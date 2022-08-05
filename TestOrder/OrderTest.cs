using Xunit;
using OrderSystem;
using System.Collections.Generic;
using FluentAssertions;
using OrderSystem.Exceptions;

namespace TestOrder
{
    public class OrderTest
    {

        [Fact]
        public void Order_Should_Be_Create()
        {
            var orderItem = new OrderItem("book",1);
            var expectedOrderItem = new List<OrderItem>
             { 
                orderItem
             };

            var orderResult = new Order(1, expectedOrderItem);

            Assert.Equal(expectedOrderItem, orderResult.OrderItems);
            Assert.Equal(1, orderResult.UserId);
           
        }

        [Fact]
        public void Order_State_Should_Be_Created_When_Order_Created()
        {
            var orderItem = new OrderItem("knif",2);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(2, orderItems);

            order.State.Should().Be(StateType.Created);
        }

        [Fact]
        public void Order_Item_Should_Be_Deleted()
        {
            var orderItem1 = new OrderItem("book", 1);
            var orderItem2 = new OrderItem("knif", 2);

            var orderList = new List<OrderItem>
            {
                orderItem1,
                orderItem2
            };
            var order = new Order(3, orderList);

            order.RemoveItem(orderItem1);
            Assert.Equal(orderList , order.OrderItems);
        }

        [Fact]
        public void Order_Added_Item_When_State_Is_Created()
        {
            var orderItem1 = new OrderItem("pen",1);
            var orderItem2 = new OrderItem("polish", 1);

            var orderList = new List<OrderItem>
            {
                orderItem1,
                orderItem2
            };
            List<OrderItem> expectedOrderItem = orderList;
            expectedOrderItem.Add(orderItem1);
            var order = new Order(4, orderList);

            order.AddItem(orderItem1);

            Assert.Equal(expectedOrderItem, order.OrderItems);

        }

        [Fact]
        public void Should_Throw_EmptyListException_When_Want_To_Add_An_Item_to_Null_List()
        {
            var order = () => new OrderSystem.Order(1, null);

            order.Should().Throw<EmptyListException>();
        }

        [Fact]
        public void Should_Throw_OutOfRangeRemoveItemException_When_CountOf_OrderItem_Is_Less_Than_One()
        {
            var orderItem = new OrderItem("book", 1);
            var orderList = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderList);

            var result = () => order.RemoveItem(orderItem);

            result.Should().Throw<OutOfRangeRemoveItemException>();
        }

        [Fact]
        public void OrderItem_Should_Throw_InvalidRemoveItemException_When_State_Is_Finalized()
        {
            var orderItem1  = new OrderItem("book", 1);
            var orderItem2 = new OrderItem("pen", 2);

            var orderList = new List<OrderItem>
            {
                orderItem1,
                orderItem2

            };
            var order = new Order(1, orderList);

            order.OrderStateToFinalized();
            var result = () => order.RemoveItem(orderItem2);

            result.Should().Throw<InvalidRemoveItemException>();
        }

        [Fact]
        public void OrderItem_Should_Throw_InvalidRemoveItemException_When_State_Is_Shipped()
        {
            var orderItem1 = new OrderItem("book", 1);
            var orderItem2 = new OrderItem("pen", 2); 
            var orderList = new List<OrderItem>
            {
                orderItem1,
                orderItem2

            };
            var order = new Order(1, orderList);

            order.OrderStateToFinalized();
            order.OrderStateToShipped();
            var result = () => order.RemoveItem(orderItem2);

            result.Should().Throw<InvalidRemoveItemException>();
        }

        [Fact]
        public void OrderItem_Should_Throw_InvalidAddItemException_When_State_Is_Finalized()
        {
            var orderItem = new OrderItem("book", 1);
            var orderList = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderList);

            order.OrderStateToFinalized();
            var result = () => order.AddItem(orderItem);

            result.Should().Throw<InvalidAddItemException>();
        }
        [Fact]
        public void OrderItem_Should_Throw_InvalidAddItemException_When_State_Is_Shipped()
        {
            var orderItem = new OrderItem("book", 1);
            var orderList = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderList);

            order.OrderStateToFinalized();
            order.OrderStateToShipped();
            var result = () => order.AddItem(orderItem);

            result.Should().Throw<InvalidAddItemException>();
        }
    }
}
