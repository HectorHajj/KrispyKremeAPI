using KrispyKreme.Application.DTO;
using KrispyKreme.Application.Helpers;
using KrispyKreme.Application.Services;
using Microsoft.AspNetCore.Mvc;

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

            var result = await customerService.AddCustomerAsync(customerDto);
            if (result)
            {
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Logs in an existing customer.
        /// </summary>
        /// <param name="loginDto">The login data transfer object.</param>
        /// <returns>An <see cref="ActionResult"/> containing the JWT token if successful.</returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Find user by email
            var user = await customerService.GetCustomerByEmail(loginDto.Email);

            // if user is not found
            if (user == null)
            {
                return Unauthorized();
            }

            // Check if password is correct
            if (user.Password == loginDto.Password)
            {
                return StatusCode(200, new
                {
                    token = jwtGenerator.GenerateToken(user)
                });
            }
            else
            {
                return StatusCode(200, new
                {
                    token = "Invalid credentials"
                });
            }
        }
    }
}