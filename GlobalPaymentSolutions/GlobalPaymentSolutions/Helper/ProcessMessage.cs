using GlobalPayments.Api;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;
using GlobalPayments.Api.Services;
using GlobalPaymentSolutions.Interface;
using GlobalPaymentSolutions.Models;
using GlobalPaymentSolutions.PaymentIntegrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GlobalPaymentSolutions.Helper
{
    public class ProcessMessage
    {
        public readonly appSettings _config;
        public static Dictionary<string, string> responseMsg = new Dictionary<string, string>();

        public ProcessMessage(appSettings config)
        {
            _config = config;
        }

        //public int Payment(OrderDetails details,ref string response)
        //{
        //    try
        //    {
        //        bool addressMatch = false;

        //        string responseError = string.Empty;
        //        if (!ValidateRequest(details,ref responseError))
        //        {
        //            response = responseError;
        //            return 400;
        //        }
        //        if (!string.IsNullOrEmpty(details.HPPAddressMatchIndicator) && details.HPPAddressMatchIndicator.ToLower() == "true")
        //        {
        //            bool.TryParse(details.HPPAddressMatchIndicator, out addressMatch);
        //        }
        //        // configure client, request and HPP settings
        //        var service = new HostedService(new GatewayConfig
        //        {
        //            MerchantId = _config.MerchantId,
        //            AccountId = _config.AccountId,
        //            SharedSecret = _config.SharedSecret,
        //            ServiceUrl = _config.ServiceUrl,
        //            SecretApiKey = details.SecretKey,
        //            HostedPaymentConfig = new HostedPaymentConfig
        //            {
        //                Version = _config.Version,
        //                PaymentButtonText = _config.PaymentButtonText
        //            }
        //        });

        //        // Add 3D Secure 2 Mandatory and Recommended Fields
        //        var hostedPaymentData = new HostedPaymentData
        //        {
        //            CustomerEmail = details.CustomerEmail,
        //            CustomerPhoneMobile = details.CustomerPhoneNum,
        //            AddressesMatch = addressMatch
        //        };

        //        var billingAddress = new Address
        //        {
        //            StreetAddress1 = details.BillingStreet1,
        //            StreetAddress2 = details.BillingStreet2,
        //            StreetAddress3 = details.BillingStreet3,
        //            City = details.BillingCity,
        //            PostalCode = details.BillingPostalCode,
        //            Country = details.BillingCountry
        //        };

        //        var shippingAddress = new Address
        //        {
        //            StreetAddress1 = details.ShippingStreet1,
        //            StreetAddress2 = details.ShippingStreet2,
        //            StreetAddress3 = details.ShippingStreet3,
        //            City = details.ShippingCity,
        //            State = details.ShippingState,
        //            PostalCode = details.ShippingPostalCode,
        //            Country = details.ShippingCountry,
        //        };

        //        try
        //        {
        //            String timeStamp = GetTimestamp(new DateTime());
        //            decimal amount = 0.0m;
        //            decimal.TryParse(details.Amount, out amount);
        //            string sha1HashCode = GetSha1HashCode(details,_config, timeStamp);
                    
        //            var responseJson = service.Charge(amount)
        //               .WithCurrency(details.Currency)
        //               .WithHostedPaymentData(hostedPaymentData)
        //               .WithAddress(billingAddress, AddressType.Billing)
        //               .WithAddress(shippingAddress, AddressType.Shipping)
        //               .WithTimestamp(timeStamp)
        //               .WithTransactionId(sha1HashCode)
        //               .Serialize();
        //            response = responseJson;
        //            return 200; 
        //        }

        //        catch (ApiException ex)
        //        {
        //            throw new Exception(ex.Message);
        //            // TODO: Add your error handling here
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    //return null;
        //}
        public int ProcessMessageMethod(OrderDetails details, ref string response)
        {
            int responseCode = 0;
            string responseError = string.Empty;
            try
            {
                if (!HelperClass.ValidateRequest(details, ref responseError))
                {
                    response = responseError;
                    return 400;
                }
                switch (details.PaymentMethod.ToUpper())
                {
                    case "CARD":
                        CardPayment cp = new CardPayment(_config);
                        return cp.ProcessPayment(details, ref response);
                    case "PAYPAL":
                        PayPalPayment ppp = new PayPalPayment(_config);
                        return ppp.ProcessPayment(details, ref response);
                    //case "APPLE-PAY":
                    //    CardPayment cp = new CardPayment(_config);
                    //    return cp.ApplePay(details, ref response);

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return responseCode;
        }
        public int ProcessPayment(OrderDetails details, ref string response)
        {
            try
            {
                //bool addressMatch = false;

                string responseError = string.Empty;
                if (!HelperClass.ValidateRequest(details, ref responseError))
                {
                    response = responseError;
                    return 400;
                }
                //if (!string.IsNullOrEmpty(details.HPPAddressMatchIndicator) && details.HPPAddressMatchIndicator.ToLower() == "true")
                //{
                //    bool.TryParse(details.HPPAddressMatchIndicator, out addressMatch);
                //}
                // configure client & request settings

                ServicesContainer.ConfigureService(new GatewayConfig
                {
                    MerchantId = _config.MerchantId,
                    AccountId = _config.AccountId,
                    SharedSecret = _config.SharedSecret,
                    ServiceUrl = _config.ServiceUrl
                });

                int expYear = 0;
                int expMonth = 0;
                int.TryParse(details.CardExpYear, out expYear);
                int.TryParse(details.CardExpMonth, out expMonth);
                // create the card object
                var card = new CreditCardData
                {
                    Number = details.CardNumber,
                    ExpMonth = expMonth,
                    ExpYear = expYear,
                    Cvn = details.CardCvn,
                    CardHolderName = details.CardHolderName
                };
                try
                {
                    // process an auto-capture authorization
                    decimal amount = 0.0m;
                    decimal.TryParse(details.Amount, out amount);
                    Transaction GPresponse = card.Charge(amount)
                       .WithCurrency(details.Currency)
                       .Execute();

                    responseMsg["message"] = GPresponse.ResponseMessage;
                    responseMsg["code"] = GPresponse.ResponseCode;
                    responseMsg["OrderId"] = GPresponse.OrderId;
                    responseMsg["AuthorizationCode"] = GPresponse.AuthorizationCode;
                    responseMsg["TransactionId"] = GPresponse.TransactionId;
                    responseMsg["SchemeId"] = GPresponse.SchemeId;
                    response = JsonConvert.SerializeObject(responseMsg);
                    return 200;
                }

                catch (ApiException ex)
                {
                    throw new Exception(ex.Message);
                    // TODO: Add your error handling here
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //return null;
        }
         }
}
