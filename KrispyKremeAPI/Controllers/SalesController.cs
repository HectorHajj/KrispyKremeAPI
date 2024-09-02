using KrispyKreme.Application.DTO.SaleDTO;
using KrispyKreme.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KrispyKremeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesController"/> class.
        /// </summary>
        /// <param name="saleService">The sale service.</param>
        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Gets all sales.
        /// </summary>
        /// <returns>A list of sales.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetSaleDto>>> GetAllSales()
        {
            try
            {
                var sales = await _saleService.GetAllSalesAsync();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while retrieving sales.", details = ex.Message });
            }
        }

        /// <summary>
        /// Gets a sale by the specified identifier.
        /// </summary>
        /// <param name="id">The sale identifier.</param>
        /// <returns>The sale with the specified identifier.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetSaleDto>> GetSaleById(int id)
        {
            try
            {
                var sale = await _saleService.GetSaleByIdAsync(id);
                if (sale == null)
                {
                    return NotFound(new { message = "Sale not found." });
                }
                return Ok(sale);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while retrieving the sale.", details = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="saleDto">The sale data transfer object.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateSale([FromBody] CreateSaleDto saleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdSale = await _saleService.AddSaleAsync(saleDto);

                if (createdSale == null)
                {
                    return BadRequest(new { message = "Failed to create sale." });
                }

                return Ok(new { message = "Sale created successfully", sale = createdSale });
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred while creating the sale.", details = ex.Message });
            }
        }
    }
}