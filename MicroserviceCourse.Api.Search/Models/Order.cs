﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Search.Models
{ 
    public class Order 
    {
        public int id { get; set; }

        public DateTime orderDate { get; set; }

        public OrderItem[] items { get; set; }

        public decimal total { get; set; }
    }
}
