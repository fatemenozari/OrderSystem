using OrderSystem;

namespace TestOrder.Builders
{
    public class OrderItemBuilder
    {
        private int _count;
        private string _name;

        public OrderItemBuilder()
        {
            _count = 2;
            _name = "book";
        }

        public OrderItemBuilder WithCount(int count)
        {
            _count = count;
            return this;
        }

        public OrderItemBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public OrderItem Build()
        {
            var orderItem = new OrderItem(_name, _count);
            return orderItem;
        }
    }
}