using KrispyKreme.Application.DTO.DoughnutDTO;
using KrispyKreme.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KrispyKreme.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoughnutController : Controller
    {
        private readonly IDoughnutService _doughnutService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoughnutController"/> class.
        /// </summary>
        /// <param name="doughnutService">The doughnut service.</param>
        public DoughnutController(IDoughnutService doughnutService)
        {
            _doughnutService = doughnutService;
        }

        /// <summary>
        /// Gets all doughnuts.
        /// </summary>
        /// <returns>A list of doughnuts.</returns>
        /// <response code="200">Returns the list of doughnuts.</response>
        /// <response code="500">If an error occurs while retrieving doughnuts.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<GetDoughnutDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetDoughnutDto>>> GetAllDoughnuts()
        {
            try
            {
                var doughnuts = await _doughnutService.GetAllDoughnutsAsync();
                return Ok(doughnuts);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while retrieving doughnuts.", details = ex.Message });
            }
        }
    }
}