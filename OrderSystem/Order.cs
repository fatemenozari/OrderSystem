namespace OrderSystem
{
    public class Order
    {
        public int UserId { get; private set; }
        public StateType State { get; private set; }
        public List<OrderItem> OrderItems = new();

        public Order(int userId, List<OrderItem> orderItems)
        {
            EnsureCurrectOrderItem(orderItems);

            UserId = userId;
            State = StateType.Created;
            OrderItems = orderItems;
        }
        public void EnsureCurrectOrderItem(List<OrderItem> orderItems)
        {
            if (orderItems == null || orderItems.Count == 0)
                throw new Exceptions.EmptyOrderItemsException();
        }

        public void Finalized()
        {
            if (State == StateType.Shipped)
                throw new Exceptions.ChangeStateToFinalizeException();

            State = StateType.Finalized;
        }

        public void Shipped()
        {
            if (State == StateType.Created)
                throw new Exceptions.ChangeStateToShippedException();

            State = StateType.Shipped;
        }

        public void AddItem(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new Exceptions.NullOrderItemException();

            if (State != StateType.Created)
                throw new Exceptions.InvalidAddItemException();

            OrderItems.Add(orderItem);
        }

        public void RemoveItem(OrderItem orderItem)
        {
            if (orderItem is null)
                throw new Exceptions.NullOrderItemException();

            if (State != StateType.Created)
                throw new Exceptions.InvalidRemoveItemException();

            if (OrderItems.Count == 1)
                throw new Exceptions.OutOfRangeRemoveItemException();

            OrderItems.Remove(orderItem);
        }
    }

    public enum StateType
    {
        Created = 1,
        Shipped = 2,
        Finalized = 3
    }
}
