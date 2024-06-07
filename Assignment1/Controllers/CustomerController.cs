using BAL.ModelView;
using BAL.Service.Implements;
using BAL.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text.RegularExpressions;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [AllowAnonymous]
        [HttpPost("/api/v1/customer/signin")]
        public IActionResult Login([FromBody] LoginInfoView loginInfo)
        {
            IConfiguration config = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                            .Build();
            string _email = config["account:defaultAccount:email"];
            string _password = config["account:defaultAccount:password"];
            if (_email.Equals(loginInfo.EmailAddress) && _password.Equals(loginInfo.Password))
            {
                return Ok("Hello Admin");
            }
            if (loginInfo.EmailAddress == null) {
                return BadRequest("Email Address cannot empty");
            }
            else if(loginInfo.Password == null)
            {
                return BadRequest("Password cannot empty");
            }
            //IActionResult response = Unauthorized();
            var accountCustomer = _customerService.AuthenticateUser(loginInfo);
            if(accountCustomer == null)
            {
                return NotFound("Customer not found, please check email and password.");
            }
            if(accountCustomer.CustomerStatus == 1)
            {
                return Ok(accountCustomer);
                //var token = _customerService.GenerateTokens(accountCustomer);
                //response = Ok(new { accessToken = token.accessToken, refreshToken = token.refreshToken });
            }
            else
            {
                return BadRequest("Your account is locked");
            }
            //return response;
        }

        //[Authorize(Policy = "RequireAdminRole")]
        [HttpGet("/api/v1/customer/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var cus = _customerService.GetCusByID(id);
            if(cus == null)
            {
                return NotFound("Customer not found");
            }
            return Ok(cus);
        }

        //[Authorize(Policy = "RequireAdminRole")]
        [HttpGet("/api/v1/customer")]
        public IActionResult GetAllCustomer()
        {
            var cusList = _customerService.GetAllActive();
            if(cusList == null)
            {
                return NotFound("Customer List is empty");
            }
            return Ok(cusList);
        }

        //[AllowAnonymous]
        [HttpPost("/api/v1/customer/register")]
        public IActionResult Register([FromBody] RegisterInfoView registerInfo)
        {
            if (string.IsNullOrEmpty(registerInfo.CustomerFullName))
            {
                return BadRequest("Full Name is required");
            }
            if (registerInfo.CustomerFullName.Length > 100)
            {
                return BadRequest("Full Name must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(registerInfo.EmailAddress))
            {
                return BadRequest("Email is required");
            }
            if (!IsValidEmail(registerInfo.EmailAddress))
            {
                return BadRequest("Invalid email address");
            }

            
            

            if (string.IsNullOrEmpty(registerInfo.Telephone))
            {
                return BadRequest("Phone number is required");
            }
            if (!Regex.IsMatch(registerInfo.Telephone, @"^[0-9]{10}$"))
            {
                return BadRequest("Phone number must be exactly 10 digits");
            }

            if (string.IsNullOrEmpty(registerInfo.Password))
            {
                return BadRequest("Password is required");
            }
            
            if (_customerService.IsExistedEmail(registerInfo.EmailAddress))
            {
                return BadRequest("Email you entered has already existed");
            }
            var customer = _customerService.GetAccountByUserName(registerInfo.CustomerFullName);
            if (customer == null)
            {
                bool checkRegister = _customerService.CreateAccountCustomer(registerInfo);
                if (checkRegister)
                {
                    return Ok("Create success");
                }
                else
                {
                    return BadRequest("Create fail");
                }
            }
            else
            {
                return BadRequest("Existed username");
            }
        }

        //[Authorize(Policy = "RequireCustomerRole")]
        [HttpPut("/api/v1/customer/{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] RegisterInfoView registerInfo)
        {
            if (string.IsNullOrEmpty(registerInfo.CustomerFullName))
            {
                return BadRequest("Full Name is required");
            }
            if (registerInfo.CustomerFullName.Length > 100)
            {
                return BadRequest("Full Name must be 100 characters or less");
            }

            if (string.IsNullOrEmpty(registerInfo.EmailAddress))
            {
                return BadRequest("Email is required");
            }
            if (!IsValidEmail(registerInfo.EmailAddress))
            {
                return BadRequest("Invalid email address");
            }




            if (string.IsNullOrEmpty(registerInfo.Telephone))
            {
                return BadRequest("Phone number is required");
            }
            if (!Regex.IsMatch(registerInfo.Telephone, @"^[0-9]{10}$"))
            {
                return BadRequest("Phone number must be exactly 10 digits");
            }

            if (string.IsNullOrEmpty(registerInfo.Password))
            {
                return BadRequest("Password is required");
            }

            if (_customerService.IsExistedEmail(registerInfo.EmailAddress))
            {
                return BadRequest("Email you entered has already existed");
            }
            var customer = _customerService.GetAccountByUserName(registerInfo.CustomerFullName);
            if (customer == null)
            {
                bool checkRegister = _customerService.UpdateCustomer(id, registerInfo);
                if (checkRegister)
                {
                    return Ok("Update success");
                }
                else
                {
                    return BadRequest("Update fail");
                }
            }
            else
            {
                return BadRequest("Existed username");
            }
        }

        //[Authorize(Policy = "RequireAdminRole")]
        [HttpPut("/api/v1/Customer/setStatus/{id}")]
        public IActionResult SetStatusRoom(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var check = _customerService.isSetStatusCustomer(id);
            if (check)
            {
                return Ok("Set status successfully!");
            }
            else
            {
                return BadRequest("Room is not available");
            }

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
