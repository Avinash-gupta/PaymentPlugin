using GlobalPaymentSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalPaymentSolutions.Interface
{
    public interface IPaymentInterface
    {
        int ProcessPayment(OrderDetails details, ref string response);
    }
}
