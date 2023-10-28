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
        public async Task<IActionResult> AddCustomerOrOrder([FromBody] Request request)
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
        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] Request request)
        {
            try
            {
                if (request != null)
                {
                    string customerId = await _customerService.CreatingClientAsync(request);
                    return Ok(customerId);
                }
                return BadRequest(string.Empty);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] Request request)
        {
            try
            {
                if (request != null)
                {
                    string orderId = await _orderService.OrderCreation(request, request.CustomerId);
                    if (orderId != null)
                    {
                        return Ok(orderId);
                    }
                    return BadRequest("Id customer not found");
                }
                return BadRequest(string.Empty);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("SearchСustomerAndOrders")]
        public async Task<IActionResult> SearchСustomerAndOrders([FromBody] Request request)
        {
            try
            {
                if (request != null)
                {
                    var resultCustomer = await _customerService.FindCustomer(request);
                    if(resultCustomer != null)
                    {
                        var resultOrders = await _orderService.FindOrders(resultCustomer.CustomerId);
                        var result = new
                        {
                            customer = resultCustomer,
                            Orders = resultOrders
                        };
                        return Ok(result);
                    }
                    return BadRequest("Klienta nie znaleziono!");
                }
                return BadRequest("Brak danych");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
