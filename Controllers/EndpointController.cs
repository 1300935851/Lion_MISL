using Lion.MISL;
using Microsoft.AspNetCore.Mvc;
using KAIFA_Api.Models;
using KAIFA_Api.BLL;
using Lion.MISL.BLL;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace KAIFA_App.Controllers
{
    [ApiController]
    public class EndpointController : Controller
    {
        public EndpointController(JWTAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        private readonly JWTAuthenticationHelper authenticationHelper;
        private MeterInfo? meterInfo = null;

        [HttpGet("/api/Endpoint/Connect")]
        public async Task<IActionResult> Connect(string meterNumber, string ip, bool isSubmeter)
        {
            if (string.IsNullOrEmpty(meterNumber))
            {
                return BuildResponseData(false, "Invalid meter number");
            }

            if (string.IsNullOrEmpty(ip))
            {
                return BuildResponseData(false, "IP is empty");
            }

            //if (!(await KaifaService.ContainsMeterKey(meterNumber)))
            //{
            //    return BuildResponseData(false, "The meter key does not exist");
            //}

            MeterInfo meterInfo = new MeterInfo();
            meterInfo.MeterNumber = meterNumber;
            meterInfo.IP = ip;
            meterInfo.IsSubmeter = isSubmeter;

            string token = authenticationHelper.BuildToken(meterInfo);
            var response = new
            {
                status = true,
                token = token
            };

            return BuildResponseData(true, JsonConvert.SerializeObject(response));
        }

        [HttpGet("/api/Endpoint/CreditStatus")]
        public async Task<IActionResult> CreditStatus(string transactionId)
        {
            CreditStatus obj = new CreditStatus();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                transactionId
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/RelayControlStatus")]
        public async Task<IActionResult> RelayControlStatus(string meterNumber)
        {
            RelayControlStatus obj = new RelayControlStatus();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/MeterTime")]
        public async Task<IActionResult> MeterTime(string meterNumber)
        {
            MeterTime obj = new MeterTime();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/CreditValue")]
        public async Task<IActionResult> CreditValue(string meterNumber)
        {
            CreditValue obj = new CreditValue();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/AddingCredit")]
        public async Task<IActionResult> AddingCredit(string meterNumber, int creditAmount, string transactionId)
        {
            AddingCredit obj = new AddingCredit();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber,
                creditAmount,
                transactionId
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/DeductCredit")]
        public async Task<IActionResult> DeductCredit(string meterNumber, int amount, string transactionId)
        {
            DeductCredit obj = new DeductCredit();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber,
                amount,
                transactionId
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/AddEmergencyCredit")]
        public async Task<IActionResult> AddEmergencyCredit(string meterNumber, int amount)
        {
            AddEmergencyCredit obj = new AddEmergencyCredit();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber,
                amount
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/MeterPowerLimit")]
        public async Task<IActionResult> MeterPowerLimit(string meterNumber, int powerLimit)
        {
            MeterPowerLimit obj = new MeterPowerLimit();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber,
                powerLimit
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/MeterActiveReactive")]
        public async Task<IActionResult> MeterActiveReactive(string meterNumber)
        {
            MeterActiveReactive obj = new MeterActiveReactive();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/MeterSignalStrength")]
        public async Task<IActionResult> MeterSignalStrength(string meterNumber)
        {
            MeterSignalStrength obj = new MeterSignalStrength();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/MeterTamperReset")]
        public async Task<IActionResult> MeterTamperReset(string meterNumber)
        {
            MeterTamperReset obj = new MeterTamperReset();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/ReadTariff")]
        public async Task<IActionResult> ReadTariff(string meterNumber)
        {
            ReadTariff obj = new ReadTariff();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/SetTariff")]
        public async Task<IActionResult> SetTariff(string meterNumber)
        {
            SetTariff obj = new SetTariff();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/MeterRelayControl")]
        public async Task<IActionResult> MeterRelayControl(string meterNumber, int command)
        {
            MeterRelayControl obj = new MeterRelayControl();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber,
                command
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/MeterMonthlyConsumption")]
        public async Task<IActionResult> MeterMonthlyConsumption(string meterNumber)
        {
            MeterMonthlyConsumption obj = new MeterMonthlyConsumption();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/FirmwareVersion")]
        public async Task<IActionResult> FirmwareVersion(string meterNumber)
        {
            FirmwareVersion obj = new FirmwareVersion();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/TamperStatus")]
        public async Task<IActionResult> TamperStatus(string meterNumber)
        {
            TamperStatus obj = new TamperStatus();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        [HttpGet("/api/Endpoint/MeterArchives")]
        public async Task<IActionResult> MeterArchives(string meterNumber)
        {
            MeterArchives obj = new MeterArchives();
            if (!JWTTokenAuthentication(obj))
            {
                return BuildResponseData(false, "Unauthorized");
            }

            var parameters = new
            {
                meterNumber
            };

            string str = JsonConvert.SerializeObject(parameters);
            if (await obj.Invoke(str))
            {
                return BuildResponseData(true, obj.Result);
            }
            else
            {
                return BuildResponseData(false, obj.ErrorMessage);
            }
        }

        private IActionResult BuildResponseData(bool result, string message)
        {
            if (result)
            {
                return Content(message, "application/json");
            }
            else
            {
                var data = new
                {
                    status = false,
                    message = message
                };

                return BadRequest(data);
            }
        }

        private bool JWTTokenAuthentication(BaseClass obj)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            meterInfo = authenticationHelper.ValidateToken(token)!;
            if (meterInfo == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(meterInfo.MeterNumber) || string.IsNullOrEmpty(meterInfo.IP))
            {
                return false;
            }

            obj.MeterNumber = meterInfo!.MeterNumber;
            obj.IPAddress = meterInfo.IP;
            obj.IsSubmeter = meterInfo.IsSubmeter;
            obj.IPPort = 4059;
            obj.Timeout = 60000;

            return true;
        }
    }
}
