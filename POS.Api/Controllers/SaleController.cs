using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Sale.Request;
using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleApplication _saleApplication;

        public SaleController(ISaleApplication saleApplication)
        {
            _saleApplication = saleApplication;
        }

        // Se cambia a HttpGet para que sea una consulta de lectura profesional
        [HttpGet]
        public async Task<IActionResult> ListSales([FromQuery] BaseFiltersRequest filters)
        {
            // [FromQuery] hace que cada propiedad de BaseFiltersRequest aparezca como una caja de texto en Swagger
            var response = await _saleApplication.ListSales(filters);
            return Ok(response);
        }

        [HttpGet("{saleId:int}")]
        public async Task<IActionResult> SaleById(int saleId)
        {
            var response = await _saleApplication.SaleById(saleId);
            return Ok(response);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterSale([FromBody] SaleRequestDto requestDto)
        {
            var response = await _saleApplication.RegisterSale(requestDto);
            return Ok(response);
        }

        [HttpPut("Cancel/{saleId:int}")]
        public async Task<IActionResult> CancelSale(int saleId)
        {
            var response = await _saleApplication.CancelSale(saleId);
            return Ok(response);
        }
    }
}