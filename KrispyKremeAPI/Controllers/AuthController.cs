using KrispyKreme.Application.DTO;
using KrispyKreme.Application.Helpers;
using KrispyKreme.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KrispyKreme.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly JwtGenerator jwtGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="_customerService">The customer service.</param>
        /// <param name="_jwtGenerator">The JWT generator.</param>
        public AuthController(
            ICustomerService _customerService,
            JwtGenerator _jwtGenerator
            )
        {
            customerService = _customerService;
            jwtGenerator = _jwtGenerator;
        }

        /// <summary>
        /// Registers a new customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await customerService.AddCustomerAsync(customerDto);
                if (result)
                {
                    return Ok(new { Message = "User registered successfully" });
                }

                return BadRequest(new { Message = "Failed to register user" });
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while registering the user.", details = ex.Message });
            }
        }

        /// <summary>
        /// Logs in an existing customer.
        /// </summary>
        /// <param name="loginDto">The login data transfer object.</param>
        /// <returns>An <see cref="ActionResult"/> containing the JWT token if successful.</returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                // Find user by email
                var user = await customerService.GetCustomerByEmail(loginDto.Email);

                // if user is not found
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Check if password is correct
                if (user.Password == loginDto.Password)
                {
                    return Ok(new
                    {
                        token = jwtGenerator.GenerateToken(user),
                        customerId = user.Id
                    });
                }
                else
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while logging in.", details = ex.Message });
            }
        }
    }
}