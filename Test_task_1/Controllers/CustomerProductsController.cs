using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestTask1.Services;
using TestTask1.Interface;
using TestTask1.Models;

namespace TestTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProductsController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public CustomerProductsController(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }
        [HttpPost("AddCustomerOrOrder")]
        public async Task<IActionResult> Post([FromBody] Request request)
        {
            try
            {
                if (request != null)
                {
                    string customerID = await _customerService.CreatingClientAsync(request);

                    if (!string.IsNullOrEmpty(customerID))
                    {
                        await _orderService.OrderCreation(request, customerID);
                        return Ok("OK");
                    }
                    return BadRequest();
                }
                return BadRequest(string.Empty);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
