using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalPaymentSolutions.Helper
{
    public class appSettings
    {
        public string MerchantId { get; set; }

        public string AccountId { get; set; }

        public string SharedSecret { get; set; }

        public string ServiceUrl { get; set; }

        public string Version { get; set; }
        public string PaymentButtonText { get; set; }
    }
}
