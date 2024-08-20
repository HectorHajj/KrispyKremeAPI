using KrispyKreme.Application.DTO.SaleDTO;
using KrispyKreme.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales);
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
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
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

            var createdSale = await _saleService.AddSaleAsync(saleDto);

            if (createdSale == null)
            {
                return BadRequest();
            }

            return Ok(new { message = "Sale created successfully", sale = createdSale });
        }
    }
}