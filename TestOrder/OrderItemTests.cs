using FluentAssertions;
using OrderSystem;
using OrderSystem.Exceptions;
using TestOrder.Builders;
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
            var orderItemBuilder = new OrderItemBuilder().WithCount(count);
            
            var OrderItem = () => orderItemBuilder.Build();

            OrderItem.Should().Throw<OutOfRangeCount>();
        }

        [Fact]
        public void OrderItem_Should_Be_Created()
        {
            var orderItemBuilder = new OrderItemBuilder()
                .WithCount(1)
                .WithName("book");

            var OrderItem = orderItemBuilder.Build();

            Assert.Equal(1,OrderItem.Count);
            Assert.Equal("book", OrderItem.Name);
        }
    }
}