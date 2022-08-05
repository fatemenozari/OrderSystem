using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class OrderItem
    { 
        public string Name { get; private set; }
        public int Count { get; private set; }

        public OrderItem(string name, int count)
        {
            CheckerCount(count);
            Name = name;
            Count = count;
        }

        public static void  CheckerCount(int count)
        {
            if (count > 3 || count <= 0)
                throw new Exceptions.OutOfRangeCount();
        }
    }
}
