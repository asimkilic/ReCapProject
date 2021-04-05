using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ServiceAdapters
{
    public class CreditCardServiceAdapter : ICreditCardService
    {
        public bool ValidatePayment(RentWithCreditCard rentWithCreditCard)
        {
            DateTime dateTime = DateTime.Now;
            if (dateTime.Minute%2==0)
            {
                return true;
            }
            return false;
            
        }
    }
}
