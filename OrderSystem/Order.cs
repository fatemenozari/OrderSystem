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
        List<OrderItem> OrderItems = new List<OrderItem>();

        public Order(int userId, List<OrderItem> orderItems)
        {
            CheckList();
            UserId = userId;
            OrderItems = orderItems;
            State = StateType.Created;
        }
        public void CheckList()
        {
            if (OrderItems == null || OrderItems.Count < 0)
                throw new Exceptions.EmptyListException();
        }

        public void ChangeState(StateType state)
        {
            if (state == StateType.Finalized)
                if (State == StateType.Created)
                    State = StateType.Finalized;
                else
                    throw new Exceptions.ChangeStateToFinalizeException();

            else if (state == StateType.Shipped)
                if (State == StateType.Finalized)
                    State = StateType.Shipped;
                else 
                    throw new Exceptions.ChangeStateToShippedException();
        }

        public void Add(List<OrderItem> orderItems)
        {
            if (State != StateType.Created)
                throw new Exceptions.InvalidAddItemException();
            
            OrderItems.AddRange(orderItems);
        }
        public void Remove(OrderItem orderItems)
        {
           CheckList();
           if (State != StateType.Created)
                throw new Exceptions.InvalidRemoveItemException();
            if (State == StateType.Created)
                OrderItems.Remove(orderItems);
        }
    }
    public enum StateType
    {
        Created = 1,
        Shipped = 2,
        Finalized = 3
    }

}
