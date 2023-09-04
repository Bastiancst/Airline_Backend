using Airline_DE.Interfaces;
using Airline_DE.Models.Email.DTOs;
using Airline_DE.Models.User.DTOs;
using Airline_DE.Services.AccountServices;
using Airline_DE.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Airline_DE.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountService;

        public AccountController(IAccountServices accountServices)
        {
            _accountService = accountServices;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequestDTO request)
        {
            try
            {
                string domainKey = Request.Headers["domainKey"];
                string ip = GenerateIPAddress();
                var result = await _accountService.AuthenticateAsync(request, ip, domainKey);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                if (ex is Microsoft.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 40615)
                {
                    return BadRequest(new ApiResponse<string>("Error in database"));
                }

                var result = new ApiResponse<AuthenticationResponseDTO>();
                result.Success = false;
                result.Errors = new List<string>() { ex.ToString() };
                return BadRequest(result);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                var origin = Request.Headers["origin"];
                var creationResponse = await _accountService.RegisterAsync(request, "");

                if (creationResponse.Success)
                {
                    //string domain = DomainSettings.ConfirmEmailRedirectDomain;
                    //string token = WebUtility.UrlEncode(creationResponse.Result.ConfirmToken.Replace("+", "%2B"));
                    //string confirmUrl = $"{domain}/api/account/confirmemail?id={creationResponse.Result.UserId}&token={token}";

                    //await _emailService.SendBasicEmailAsync(new BasicEmailRequestDTO
                    //{
                    //    Subject = "Welcome to Tesseract!",
                    //    Message = $"Hello! {creationResponse.Result.Username} Welcome to Tesseract Ecosystem! \n please confirm your account with the following URL: \n{confirmUrl}",
                    //    ReceiverEmail = $"{creationResponse.Result.Email}",
                    //    ReceiverName = $"{creationResponse.Result.Name}"
                    //});

                    return Ok(creationResponse);
                }

                return BadRequest(creationResponse);
            }
            catch (Exception ex)
            {
                var result = new ApiResponse<string>();
                result.Success = false;
                result.Errors = new List<string>() { ex.ToString() };
                return BadRequest(result);
            }
        }

        #region private methods

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        #endregion
    }
}
