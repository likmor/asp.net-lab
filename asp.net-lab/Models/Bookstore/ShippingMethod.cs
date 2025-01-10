﻿using System;
using System.Collections.Generic;

namespace asp.net_lab.Models.Bookstore;

public partial class ShippingMethod
{
    public int MethodId { get; set; }

    public string? MethodName { get; set; }

    public double? Cost { get; set; }

    public virtual ICollection<CustOrder> CustOrders { get; set; } = new List<CustOrder>();
}