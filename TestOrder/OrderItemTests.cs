using FluentAssertions;
using OrderSystem;
using OrderSystem.Exceptions;
using Xunit;

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
            var orderItem = () => new OrderItem("book", count);

            orderItem.Should().Throw<OutOfRangeCount>();
        }

        [Fact]
        public void OrderItem_Should_Be_Created()
        {
            var orderItem = new OrderItem("book", 1);

            Assert.Equal(1, orderItem.Count);
            Assert.Equal("book", orderItem.Name);
        }
    }
}