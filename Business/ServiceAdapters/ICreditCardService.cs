using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ServiceAdapters
{
    public interface ICreditCardService
    {
        bool ValidatePayment(RentWithCreditCard rentWithCreditCard); // provizyon numarasıda döndürebiliriz.
    }
}
