using GlobalPaymentSolutions.Helper;
using GlobalPaymentSolutions.Interface;
using GlobalPaymentSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalPaymentSolutions.PaymentIntegrations
{
    public class PayPalPayment : IPaymentInterface
    {
        public readonly appSettings _config;
        public PayPalPayment(appSettings config)
        {
            _config = config;
        }
        public int ProcessPayment(OrderDetails details, ref string response)
        {
            throw new NotImplementedException();
        }
    }
}
