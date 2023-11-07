using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using TestTask1.Interface;
using TestTask1.Models;

namespace TestTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public CustomerController(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }
        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] Customers customer)
        {
            try
            {
                if (customer != null)
                {
                    string customerId = await _customerService.CreatingClientAsync(customer);
                    return Ok(customerId);
                }
                return BadRequest(string.Empty);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("SearchСustomer")] // Wyszukiwamy klienta po ID
        public async Task<IActionResult> SearchСustomer([FromQuery] string customerId)
        {
            try
            {
                if (customerId != null)
                {
                    Customers customer = new Customers
                    {
                        CustomerId = customerId
                    };
                    var resultCustomer = await _customerService.FindCustomer(customer); // sprawdzamy czy mamy taki ID w bazie danych
                    if (resultCustomer != null) // jeżeli mamy sprawdzamy czy są zamówienia klienta
                    {
                        return Ok(resultCustomer);
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
