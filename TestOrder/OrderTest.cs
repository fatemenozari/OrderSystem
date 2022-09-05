using FluentAssertions;
using OrderSystem;
using OrderSystem.Exceptions;
using System.Collections.Generic;
using TestOrder.Builders;
using Xunit;

namespace TestOrder
{
    public class OrderTest
    {
        [Fact]
        public void Order_Should_Be_Created()
        {
            var orderItem = new OrderItemBuilder().Build();
            var order = new OrderBuilder()
                .WithUserId(2)
                .WithOrderItem(orderItem)
                .Build();
            List<OrderItem> expectdeOrderItems = new() { orderItem };

            order.OrderItems.Should().BeEquivalentTo(expectdeOrderItems);
            Assert.Equal(2, order.UserId);
        }

        [Fact]
        public void Order_State_Should_Be_Created_When_Order_Create()
        {
            var orderItem = new OrderBuilder().Build();

            orderItem.State.Should().Be(StateType.Created);
        }

        [Fact]
        public void OrderItem_Should_Be_Deleted()
        {
            var orderItem1 = new OrderItemBuilder().WithName("book").WithCount(1).Build();
            var orderItem2 = new  OrderItemBuilder().WithName("pen").WithCount(1).Build();
            List<OrderItem> orderItems = new()
            {
                orderItem1,
                orderItem2
            };
            var order = new OrderBuilder().WithOrderItems(orderItems).Build();


            order.RemoveItem(orderItem1);

            Assert.Equal(orderItems, order.OrderItems);
        }

        [Fact]
        public void OrderItem_Should_Be_Added()
        {
            var orderItem = new OrderItemBuilder().WithName("book").WithCount(1).Build();
            List<OrderItem> orderItems = new()
            {
                orderItem
            };
            List<OrderItem> expectedOrderItems = new()
            {
                orderItem,
                orderItem
            };
            var order = new OrderBuilder().WithOrderItems(orderItems).Build();

            order.AddItem(orderItem);

            Assert.Equal(expectedOrderItems, order.OrderItems);
        }

        [Fact]
        public void Should_Throw_EmptyListException_When_OrderItems_Is_Null()
        {
            var orderBuilder = new OrderBuilder().WithOrderItems(null);

            var order = () => orderBuilder.Build();

            order.Should().Throw<EmptyOrNullOrderItemsException>();
        }


        [Fact]
        public void Should_Throw_OutOfRangeRemoveItemException_When_Count_Of_OrderItems_Is_Less_Than_One()
        {
            var orderItem = new OrderItemBuilder().Build();
            var orderBuilder = new OrderBuilder()
                 .WithOrderItem(orderItem);
            var order = orderBuilder.Build();

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
            var orderItem = new OrderItemBuilder().Build();
            var orderBuilder = new OrderBuilder()
                .WithUserId(1)
                .WithOrderItem(orderItem);
            var order = orderBuilder.Build();

            order.Finalized();
            var result = () => order.AddItem(orderItem);

            result.Should().Throw<InvalidAddItemException>();
        }
        [Fact]
        public void AddItem_Should_Throw_InvalidAddItemException_When_State_Is_Shipped()
        {
            var orderItem = new OrderItemBuilder().Build();
            var orderBuilder = new OrderBuilder()
                .WithUserId(1)
                .WithOrderItem(orderItem);
            var order = orderBuilder.Build();

            order.Finalized();
            order.Shipped();
            var result = () => order.AddItem(orderItem);

            result.Should().Throw<InvalidAddItemException>();
        }

        [Fact]
        public void Order_State_Should_Be_Finalized()
        {
            var order = new OrderBuilder().Build();

            order.Finalized();

            order.State.Should().Be(StateType.Finalized);
        }

        [Fact]
        public void Order_State_Should_Be_Shipped()
        {
            var order = new OrderBuilder().Build();


            order.Finalized();
            order.Shipped();

            order.State.Should().Be(StateType.Shipped);
        }

        [Fact]
        public void Finalized_Should_Throw_ChangeStateToFinalizeException_When_State_Is_Shipped()
        {
            var order = new OrderBuilder().Build();

            order.Finalized();
            order.Shipped();
            var result = () => order.Finalized();

            result.Should().Throw<ChangeStateToFinalizeException>();
        }

        [Fact]
        public void Shipped_Should_Throw_ChangeStateToShippedException_When_State_Is_Created()
        {
            var order = new OrderBuilder().Build();


            var result = () => order.Shipped();

            result.Should().Throw<ChangeStateToShippedException>();
        }

        [Fact]
        public void AddItem_Should_Throw_NullOrderItemException_When_OrderItem_Is_Null()
        {
            var order = new OrderBuilder().Build();

            var result = () => order.AddItem(null);

            result.Should().Throw<NullOrderItemException>();
        }

        [Fact]
        public void RemoveItem_Should_Throw_NullOrderItemException_When_OrderItem_Is_Null()
        {
            var order = new OrderBuilder().Build();

            var result = () => order.RemoveItem(null);

            result.Should().Throw<NullOrderItemException>();
        }
    }
}
