using GlobalPaymentSolutions.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GlobalPaymentSolutions.Helper
{
    public static class HelperClass
    {
        public static Dictionary<string, string> responseMsg = new Dictionary<string, string>();

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        //public static string GetSha1HashCode(OrderDetails details,appSettings _config ,string timeStamp)
        //{
        //    string code = string.Format("{0}.{1}.{2}.{3}.{4}", timeStamp, _config.MerchantId, details.OrderId, details.Amount, details.Currency);
        //    string Sha1HashCode = GetSha1(code);
        //    string code1 = string.Format("{0}.{1}", code, _config.SharedSecret);
        //    Sha1HashCode = GetSha1(code1);
        //    return Sha1HashCode;
        //}
        public static string GetSha1(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var hashData = new SHA1Managed().ComputeHash(data);
            var hash = string.Empty;
            foreach (var b in hashData)
            {
                hash += b.ToString("X2");
            }
            return hash;
        }
        
        public static bool ValidateRequest(OrderDetails details, ref string result)
        {
            bool Rescode = true;
            try
            {
                Rescode = validateOrderDetaiulsMembers(details.Amount, "Amount", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.Currency, "Currency", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                //Rescode = validateOrderDetaiulsMembers(details.HppLang, "HppLang", ref result);
                //if (!Rescode)
                //{
                //    return Rescode;
                //}

                Rescode = validateOrderDetaiulsMembers(details.FirstName, "FirstName", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.LastName, "LastName", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.PhoneNumber, "PhoneNumber", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.Email, "Email", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.Address, "Address", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.City, "City", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.State, "State", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.PostalCode, "PostalCode", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }

                Rescode = validateOrderDetaiulsMembers(details.CardNumber, "CardNumber", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.CardExpMonth, "CardExpMonth", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.CardExpYear, "CardExpYear", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.CardHolderName, "CardHolderName", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }
                Rescode = validateOrderDetaiulsMembers(details.CardCvn, "CardCvn", ref result);
                if (!Rescode)
                {
                    return Rescode;
                }

                Rescode = validateOrderDetaiulsMembers(details.PaymentMethod, "PaymentMethod", ref result);
                if (!Rescode)
                {
                    return Rescode;

                }
                //Rescode = validateOrderDetaiulsMembers(details.BillingCountry, "BillingCountry", ref result);
                //if (!Rescode)
                //{
                //    return Rescode;
                //}
                //Rescode = validateOrderDetaiulsMembers(details.ShippingStreet1, "ShippingStreet1", ref result);
                //if (!Rescode)
                //{
                //    return Rescode;
                //}
                //Rescode = validateOrderDetaiulsMembers(details.ShippingCity, "ShippingCity", ref result);
                //if (!Rescode)
                //{
                //    return Rescode;
                //}
                //Rescode = validateOrderDetaiulsMembers(details.ShippingState, "ShippingState", ref result);
                //if (!Rescode)
                //{
                //    return Rescode;
                //}
                //Rescode = validateOrderDetaiulsMembers(details.ShippingCountry, "ShippingCountry", ref result);
                //if (!Rescode)
                //{
                //    return Rescode;
                //}
                //Rescode = validateOrderDetaiulsMembers(details.ShippingPostalCode, "ShippingPostalCode", ref result);
                //if (!Rescode)
                //{
                //    return Rescode;
                //}
                //Rescode = validateOrderDetaiulsMembers(details.OrderId, "OrderId", ref result);
                //if (!Rescode)
                //{
                //    return Rescode;
                //}
                //Rescode = validateOrderDetaiulsMembers(details.SecretKey, "SecretKey", ref result);
                //if (!Rescode)
                //{
                //    return Rescode;
                //}
            }
            catch (Exception ex)
            {
                result = JsonConvert.SerializeObject(ex);
            }
            return Rescode;
        }
        public static bool validateOrderDetaiulsMembers(string value, string type, ref string responseError)
        {
            bool Rescode = true;
            try
            {
                if (value == "string" || string.IsNullOrEmpty(value))
                {
                    Rescode = false;
                    responseMsg["message"] = "Bad Request - " + type + " value should not be empty or invalid";
                    responseMsg["code"] = "400";
                    responseError = JsonConvert.SerializeObject(responseMsg);

                }
            }
            catch (Exception ex)
            {
                Rescode = false;
                responseError = JsonConvert.SerializeObject(ex);
            }

            return Rescode;
        }

    }
}
