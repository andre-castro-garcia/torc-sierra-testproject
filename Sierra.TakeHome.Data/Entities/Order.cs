using System;
using System.Collections.Generic;

namespace Sierra.TakeHome.Data.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public Guid CustomerId { get; set; }

    public int Quantity { get; set; }

    public decimal Total { get; set; }
}
