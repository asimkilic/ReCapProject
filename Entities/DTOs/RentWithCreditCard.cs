using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
   public class RentWithCreditCard:IDto
    {
        public Rental Rental { get; set; }
        public string CardHoldersName { get; set; }
        public string CardNumber { get; set; }
        public int CardExpirationMonth { get; set; }
        public int CardExpirationYear { get; set; }
        public int CardCvcNumber { get; set; }
        public int TotalPrice { get; set; }
    }
}
