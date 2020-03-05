using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GlobalPaymentSolutions.Helper;
using GlobalPaymentSolutions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GlobalPaymentSolutions.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly appSettings _config;
        
        public PaymentController(IOptions<appSettings> appIdentitySettingsAccessor)
        {
            _config = appIdentitySettingsAccessor.Value;
        }
        //testing Get Method
        [HttpGet]
        [Route("api/user")]
        public IActionResult Get()
        {
            return Ok(new { name = "John" });
        }

        //Global Pay actual endpoint
        [HttpPost]
        [Route("api/payment")]
        public IActionResult Post([FromBody]OrderDetails details)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            try
            {
                if(details!=null)
                {
                    string orderData = JsonConvert.SerializeObject(details);
                    ProcessMessage _payment = new ProcessMessage(_config);
                    string result = string.Empty;
                    int responseCode = _payment.ProcessMessageMethod(details, ref result);
                    switch (responseCode)
                    {
                        case 202:
                            return Accepted(result);
                        case 400:
                            return BadRequest(result);
                    }

                    return Ok();
                }
                else
                {
                    response["message"] = "Body should not be empty";
                    response["code"] = "400";
                    return BadRequest(response);
                }
            }
            catch(JsonSerializationException ex)
            {
                response["message"] = "Invalid Data Format "+ ex.Message;
                response["code"] = "400";
                return BadRequest(response);
            }
            catch(Exception ex)
            {
                response["message"] = "(500) internal server error "+ex.Message;
                response["code"] = "500";
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
