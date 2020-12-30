using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderWithItemsandOrderingSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsandOrderingSpecification(string email) :base(o=>o.Buyeremail== email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);

        }

        public OrderWithItemsandOrderingSpecification(int id, string email)
         : base(o => o.Id ==id && o.Buyeremail ==email)
        {
             AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}