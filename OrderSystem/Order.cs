using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Order
    {
        public int UserId { get; private set; }
        public StateType State { get; private set; }
        public List<OrderItem> OrderItems = new List<OrderItem>();

        public Order(int userId, List<OrderItem> orderItems)
        {
            OrderItems = orderItems;
            EnsureCurrectValue(OrderItems);
            UserId = userId;
            State = StateType.Created;
            
        }
        public void EnsureCurrectValue(List<OrderItem> orderItems)
        {
            if (OrderItems == null || OrderItems.Count == 0)
                throw new Exceptions.EmptyListException();
        }

        public void ChangeStateOrder(StateType state)
        {
            if (state == StateType.Finalized)
                OrderStateToFinalized();

            else if (state == StateType.Shipped)
                OrderStateToShipped();
        }
        public void OrderStateToFinalized()
        {
            if (State == StateType.Shipped)
                throw new Exceptions.ChangeStateToFinalizeException();
            State = StateType.Finalized;
        }
        public void OrderStateToShipped()
        {
            if (State == StateType.Created)
                throw new Exceptions.ChangeStateToShippedException();
            else
             State = StateType.Shipped;
        }

        public void AddItem(OrderItem orderItems)
        {
            if (State != StateType.Created)
              throw new Exceptions.InvalidAddItemException();
           
            OrderItems.Add(orderItems);
        }
        public void RemoveItem(OrderItem orderItems)
        {
            if (State == StateType.Created)
             {
               if (OrderItems.Count > 1)
                  OrderItems.Remove(orderItems);
              else
                  throw new Exceptions.OutOfRangeRemoveItemException();
             }
            else
             throw new Exceptions.InvalidRemoveItemException();
        }
    }
    public enum StateType
    {
        Created = 1,
        Shipped = 2,
        Finalized = 3
    }
}
