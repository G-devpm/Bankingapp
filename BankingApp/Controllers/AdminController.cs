using Microsoft.AspNetCore.Http;
using BankingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BankingApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ITransferService _transferService;
        public AdminController(ITransferService transferService)
        {
            _transferService = transferService;
        }
        [HttpGet("transfers")]
        public async Task<IActionResult> GetAllTransfers()
        {
            var transfers = await _transferService.GetAllTransfersAsync();
            return Ok(transfers);
        }
    }
}
