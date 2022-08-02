using Xunit;
using OrderSystem;
using System.Collections.Generic;
using FluentAssertions;
using OrderSystem.Exceptions;
using OrderSystem;

namespace TestOrder
{
    public class OrderUnHappyTest
    {
        [Fact]
        public void Should_Throw_EmptyListException_When_Want_To_Add_An_Item_to_The_Null_List()
        {
            var order =() => new OrderSystem.Order(1,null);
            order.Should().Throw<EmptyListException>();
        }

       
    }
}
