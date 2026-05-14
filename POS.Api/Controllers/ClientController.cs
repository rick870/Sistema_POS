using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Product.Request;
using POS.Application.Interfaces;
using POS.Application.Services;
using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientApplication _clientApplication;

        public ClientController(IClientApplication clientApplication)
        {
            _clientApplication = clientApplication;
        }


        [HttpPost]
        public async Task<IActionResult> ListClients([FromBody] BaseFiltersRequest filters)
        {
            var response = await _clientApplication.ListClients(filters);
            return Ok(response);
        }

        [HttpGet("{clientId:int}")] // Es buena práctica añadir el tipo :int
        public async Task<IActionResult> ClientById([FromRoute] int clientId)
        {
            var response = await _clientApplication.ClientById(clientId);
            return Ok(response);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterClient([FromBody] ClientRequestDto requestDto)
        {
            var response = await _clientApplication.RegisterClient(requestDto);
            return Ok(response);
        }


        [HttpPut("Edit/{clientId:int}")]
        public async Task<IActionResult> EditClient(int clientId, [FromBody] ClientRequestDto requestDto)
        {
            var response = await _clientApplication.EditClient(clientId, requestDto);
            return Ok(response);
        }


        [HttpPut("Remove/{clientId:int}")]
        public async Task<IActionResult> RemoveClient(int clientId)
        {
            var response = await _clientApplication.RemoveClient(clientId);
            return Ok(response);
        }
    }
}
