using FluentAssertions;
using OrderSystem;
using OrderSystem.Exceptions;
using System.Collections.Generic;
using Xunit;

namespace TestOrder
{
    public class OrderTest
    {
        [Fact]
        public void Order_Should_Be_Created()
        {
            var orderItem = new OrderItem("book", 1);
            var expectedOrderItems = new List<OrderItem>
            {
                orderItem
            };
            var orderResult = new Order(1, expectedOrderItems);

            Assert.Equal(expectedOrderItems, orderResult.OrderItems);
            Assert.Equal(1, orderResult.UserId);
        }

        [Fact]
        public void Order_State_Should_Be_Created_When_Order_Create()
        {
            var orderItem = new OrderItem("knife", 2);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(2, orderItems);

            order.State.Should().Be(StateType.Created);
        }

        [Fact]
        public void OrderItem_Should_Be_Deleted()
        {
            var orderItem1 = new OrderItem("book", 1);
            var orderItem2 = new OrderItem("knife", 2);
            var orderItems = new List<OrderItem>
            {
                orderItem1,
                orderItem2
            };
            var order = new Order(3, orderItems);

            order.RemoveItem(orderItem1);

            Assert.Equal(orderItems, order.OrderItems);
        }

        [Fact]
        public void OrderItem_Should_Be_Added()
        {
            var orderItem = new OrderItem("pen", 1);
            List<OrderItem> orderItems = new()
            {
                orderItem
            };
            List<OrderItem> expectedOrderItems = new()
            {
                orderItem,
                orderItem
            };
            var order = new Order(4, orderItems);

            order.AddItem(orderItem);

            Assert.Equal(expectedOrderItems, order.OrderItems);
        }

        [Fact]
        public void Should_Throw_EmptyListException_When_OrderItems_Is_Null()
        {
            var order = () => new Order(1, null);

            order.Should().Throw<EmptyOrderItemsException>();
        }

        [Fact]
        public void Should_Throw_EmptyListException_When_OrderItems_Is_Empty()
        {
            var orderItems = new List<OrderItem>();
            var order = () => new Order(1, orderItems);

            order.Should().Throw<EmptyOrderItemsException>();
        }

        [Fact]
        public void Should_Throw_OutOfRangeRemoveItemException_When_Count_Of_OrderItems_Is_Less_Than_One()
        {
            var orderItem = new OrderItem("book", 1);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderItems);

            var result = () => order.RemoveItem(orderItem);

            result.Should().Throw<OutOfRangeRemoveItemException>();
        }

        [Fact]
        public void RemoveItem_Should_Throw_InvalidRemoveItemException_When_State_Is_Finalized()
        {
            var orderItem1 = new OrderItem("book", 1);
            var orderItem2 = new OrderItem("pen", 2);
            var orderItems = new List<OrderItem>
            {
                orderItem1,
                orderItem2
            };
            var order = new Order(1, orderItems);

            order.Finalized();
            var result = () => order.RemoveItem(orderItem2);

            result.Should().Throw<InvalidRemoveItemException>();
        }

        [Fact]
        public void RemoveItem_Should_Throw_InvalidRemoveItemException_When_State_Is_Shipped()
        {
            var orderItem1 = new OrderItem("book", 1);
            var orderItem2 = new OrderItem("pen", 2);
            var orderItems = new List<OrderItem>
            {
                orderItem1,
                orderItem2
            };
            var order = new Order(1, orderItems);

            order.Finalized();
            order.Shipped();
            var result = () => order.RemoveItem(orderItem2);

            result.Should().Throw<InvalidRemoveItemException>();
        }

        [Fact]
        public void AddItem_Should_Throw_InvalidAddItemException_When_State_Is_Finalized()
        {
            var orderItem = new OrderItem("book", 1);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderItems);

            order.Finalized();
            var result = () => order.AddItem(orderItem);

            result.Should().Throw<InvalidAddItemException>();
        }
        [Fact]
        public void AddItem_Should_Throw_InvalidAddItemException_When_State_Is_Shipped()
        {
            var orderItem = new OrderItem("book", 1);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderItems);

            order.Finalized();
            order.Shipped();
            var result = () => order.AddItem(orderItem);

            result.Should().Throw<InvalidAddItemException>();
        }

        [Fact]
        public void Order_State_Should_Be_Finalized()
        {
            var orderItem = new OrderItem("book", 1);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderItems);

            order.Finalized();

            order.State.Should().Be(StateType.Finalized);
        }

        [Fact]
        public void Order_State_Should_Be_Shipped()
        {
            var orderItem = new OrderItem("book", 1);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderItems);

            order.Finalized();
            order.Shipped();

            order.State.Should().Be(StateType.Shipped);
        }

        [Fact]
        public void Finalized_Should_Throw_When_State_Is_Shipped()
        {
            var orderItem = new OrderItem("book", 1);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderItems);

            order.Finalized();
            order.Shipped();
            var result = () => order.Finalized();

            result.Should().Throw<ChangeStateToFinalizeException>();
        }

        [Fact]
        public void Shipped_Should_Throw_When_State_Is_Created()
        {
            var orderItem = new OrderItem("book", 1);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderItems);

            var result = () => order.Shipped();

            result.Should().Throw<ChangeStateToShippedException>();
        }

        [Fact]
        public void AddItem_Should_Throw_NullOrderItemException_When_OrderItem_Is_Null()
        {
            var orderItem = new OrderItem("book", 1);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderItems);

            var result = () => order.AddItem(null);

            result.Should().Throw<NullOrderItemException>();
        }

        [Fact]
        public void RemoveItem_Should_Throw_NullOrderItemException_When_OrderItem_Is_Null()
        {
            var orderItem = new OrderItem("book", 1);
            var orderItems = new List<OrderItem>
            {
                orderItem
            };
            var order = new Order(1, orderItems);

            var result = () => order.RemoveItem(null);

            result.Should().Throw<NullOrderItemException>();
        }
    }
}
