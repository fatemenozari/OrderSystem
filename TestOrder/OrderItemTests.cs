using Xunit;
using OrderSystem;
using FluentAssertions;
using OrderSystem.Exceptions;

namespace TestOrder
{
    public class OrderItemTests
    {
        [Theory]
        [InlineData(6)]
        [InlineData(0)]
        [InlineData(-1)]

        public void Should_Throw_OutOfRangeCountException_When_Count_Is_Invalid(int count)
        {
            var orderItem = () => new OrderSystem.OrderItem("book", count);

            orderItem.Should().Throw < OutOfRangeCount > ();
        }


        [Fact]
        public void OrderItem_Should_Be_Created()
        {
            var orderItem = new OrderSystem.OrderItem("book", 2);

            Assert.Equal(2, orderItem.Count);
            Assert.Equal("book", orderItem.Name);

        }


    }
}