using GlobalPayments.Api;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;
using GlobalPaymentSolutions.Helper;
using GlobalPaymentSolutions.Interface;
using GlobalPaymentSolutions.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalPaymentSolutions.PaymentIntegrations
{
    public class CardPayment : IPaymentInterface
    {
        public static Dictionary<string, string> responseMsg = new Dictionary<string, string>();
        public readonly appSettings _config;
        public CardPayment(appSettings config)
        {
            _config = config;
        }
        public int ProcessPayment(OrderDetails details, ref string response)
        {
            try
            {
                //bool addressMatch = false;

                string responseError = string.Empty;
                if (!ValidateCard(details, ref responseError,_config))
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
                    return 202;
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
        }
        public static bool ValidateCard(OrderDetails details, ref string result, appSettings _config)
        {
            try
            {
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
                    Transaction GPresponse = card.Verify()
                    .Execute();
                    responseMsg["message"] = GPresponse.ResponseMessage;
                    responseMsg["code"] = GPresponse.ResponseCode;
                    responseMsg["SchemeId"] = GPresponse.SchemeId;
                    result = JsonConvert.SerializeObject(responseMsg);
                    return true;
                }

                catch (ApiException ex)
                {
                    throw new Exception(ex.Message);
                    // TODO: Add your error handling here
                }
            }
            catch (Exception ex)
            {
                result = JsonConvert.SerializeObject(ex.Message);
                return false;
            }
        }

        public int ApplePayPayment(OrderDetails details, ref string response)
        {
            try
            {
                //bool addressMatch = false;

                string responseError = string.Empty;
                if (!ValidateCard(details, ref responseError, _config))
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
                    return 202;
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
        }
    }
}
