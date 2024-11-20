﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderBookAPI.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public double Price { get; set; }
        public string nameOrder { get; set; }
        public Type Type { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public PickupPlace PickupPlace { get; set; }       
        public string PickupPlaceAsString { get; set; }
        public bool Delivered { get; set; }
        public string Image { get; set; }
        public Status Status { get; set; }    
        public TimeSpan? DeliveryTime {get; set;}
        public int CustomerID { get; set; }


        // Navigation Property for ApplicationUser
        public Customer Customer { get; set; }


    }
}
