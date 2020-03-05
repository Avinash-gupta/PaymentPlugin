using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalPaymentSolutions.Models
{
    public class OrderDetails
    {
        [Required]
        [JsonProperty("Amount", Required = Required.Always)]
        public string Amount { get; set; }

        [Required]
        [JsonProperty("Currency", Required = Required.Always)]
        public string Currency { get; set; }

        //public string AutoSettleFlag { get; set; }

        //public string Comment { get; set; }

        //[JsonProperty("HppLang", Required = Required.Always)]
        //public string HppLang { get; set; }

        [JsonProperty("FirstName", Required = Required.Always)]
        public string FirstName { get; set; }

        [JsonProperty("LastName", Required = Required.Always)]
        public string LastName { get; set; }

        [JsonProperty("Email", Required = Required.Always)]
        public string Email { get; set; }

        [JsonProperty("PhoneNumber", Required = Required.Always)]
        public string PhoneNumber { get; set; }

        [JsonProperty("Address", Required = Required.Always)]
        public string Address { get; set; }

        [JsonProperty("City", Required = Required.Always)]
        public string City { get; set; }

        [JsonProperty("PostalCode", Required = Required.Always)]
        public string PostalCode { get; set; }

        [JsonProperty("State", Required = Required.Always)]
        public string State { get; set; }

        [JsonProperty("PaymentMethod", Required = Required.Always)]
        public string PaymentMethod { get; set; }

        //[JsonProperty("BillingStreet1", Required = Required.Always)]
        //public string BillingStreet1 { get; set; }

        //public string BillingStreet2 { get; set; }

        //public string BillingStreet3 { get; set; }

        //[JsonProperty("BillingCity", Required = Required.Always)]
        //public string BillingCity { get; set; }

        //[JsonProperty("BillingPostalCode", Required = Required.Always)]
        //public string BillingPostalCode { get; set; }

        //[JsonProperty("BillingCountry", Required = Required.Always)]
        //public string BillingCountry { get; set; }

        //[JsonProperty("ShippingStreet1", Required = Required.Always)]
        //public string ShippingStreet1 { get; set; }
        //public string ShippingStreet2 { get; set; }
        //public string ShippingStreet3 { get; set; }

        //[JsonProperty("ShippingCity", Required = Required.Always)]
        //public string ShippingCity { get; set; }

        //[JsonProperty("ShippingState", Required = Required.Always)]
        //public string ShippingState { get; set; }

        //[JsonProperty("ShippingPostalCode", Required = Required.Always)]
        //public string ShippingPostalCode { get; set; }

        //[JsonProperty("ShippingCountry", Required = Required.Always)]
        //public string ShippingCountry { get; set; }

        //public string HPPAddressMatchIndicator { get; set; }

        //public string HPPChallengeRequestIndicator { get; set; }

        //[JsonProperty("OrderId", Required = Required.Always)]
        //public string OrderId { get; set; }

        //public string Token_value { get; set; }

        [JsonProperty("SecretKey", Required = Required.Always)]
        public string SecretKey { get; set; }

        [JsonProperty("CardNumber", Required = Required.Always)]
        public string CardNumber { get; set; }

        [JsonProperty("CardExpMonth", Required = Required.Always)]
        public string CardExpMonth { get; set; }

        [JsonProperty("CardExpYear ", Required = Required.Always)]
        public string CardExpYear { get; set; }

        [JsonProperty("CardHolderName", Required = Required.Always)]
        public string CardHolderName { get; set; }

        [JsonProperty("CardCvn", Required = Required.Always)]
        public string CardCvn { get; set; }
      
        public string DeveloperId { get; set; }

    }
}

//  <!-- Begin Fraud Management and Reconciliation Fields -->
//  <input type = "hidden" name="BILLING_CODE" value="59|123">
//  <input type = "hidden" name="BILLING_CO" value="GB">
//  <input type = "hidden" name="SHIPPING_CODE" value="50001|Apartment 852">
//  <input type = "hidden" name="SHIPPING_CO" value="US">
//  <input type = "hidden" name="CUST_NUM" value="6e027928-c477-4689-a45f-4e138a1f208a">
//  <input type = "hidden" name="VAR_REF" value="Acme Corporation">
//  <input type = "hidden" name="PROD_ID" value="SKU1000054">
//  <!-- End Fraud Management and Reconciliation Fields -->
//  <input type = "hidden" name="MERCHANT_RESPONSE_URL" value="https://www.example.com/responseUrl">
//  <input type = "hidden" name="CARD_PAYMENT_BUTTON" value="Pay Invoice">
//  <input type = "hidden" name="CUSTOM_FIELD_NAME" value="Custom Field Data">
//  <input type = "hidden" name="SHA1HASH" value="308bb8dfbbfcc67c28d602d988ab104c3b08d012">
//  <input type = "submit" value="Click To Pay">
//</form>