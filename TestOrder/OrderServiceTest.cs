using FluentAssertions;
using NSubstitute;
using OrderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestOrder
{
    public class OrderServiceTest
    {
        [Fact]
        public void Order_Should_Send_Sms()
        {
            var smsServiceMock = Substitute.For<ISmsService>();
            var orderService = new Services( smsServiceMock , null);

            orderService.Completed();

            smsServiceMock.Received().SendMessage("Done !", "0919");
        }

        [Fact]
        public void Order_Should_Throw_ArgumentNullException_When_OrderId_Not_Found()
        {
            var orderRepositoryStub = Substitute.For<IOrderRepository>();
            orderRepositoryStub.GetById(2).Returns(x => null);
            var orderService = new Services(null, orderRepositoryStub);

            var result = () => orderService.GetOrder(2);
            result.Should().Throw<ArgumentNullException>();
        }


    }
}
