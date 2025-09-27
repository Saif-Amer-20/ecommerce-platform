using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = await _orderService.CreateOrderAsync(
            request.UserId, 
            request.AddressId, 
            request.PaymentMethod, 
            request.Notes);

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, new
        {
            id = order.Id,
            status = order.Status.ToString(),
            total = order.Total,
            paymentMethod = order.PaymentMethod,
            createdAt = order.CreatedAt
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        // In a real implementation, get user ID from JWT token
        var userId = 1;
        var order = await _orderService.GetOrderDetailsAsync(id, userId);
        
        if (order != null)
        {
            return Ok(new 
            {
                id = order.Id,
                status = order.Status.ToString(),
                total = order.Total,
                paymentMethod = order.PaymentMethod,
                notes = order.Notes,
                createdAt = order.CreatedAt
            });
        }

        return NotFound(new { message = "الطلب غير موجود" });
    }

    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        // In a real implementation, get user ID from JWT token
        var userId = 1;
        var orders = await _orderService.GetUserOrdersAsync(userId, page, pageSize);

        return Ok(new
        {
            data = orders.Select(o => new
            {
                id = o.Id,
                status = o.Status.ToString(),
                total = o.Total,
                paymentMethod = o.PaymentMethod,
                createdAt = o.CreatedAt
            }),
            page,
            pageSize,
            message = "تم جلب الطلبات بنجاح"
        });
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        // In a real implementation, get user ID from JWT token
        var userId = 1;
        var success = await _orderService.CancelOrderAsync(id, userId);

        if (success)
        {
            return Ok(new { message = "تم إلغاء الطلب بنجاح" });
        }

        return BadRequest(new { message = "فشل في إلغاء الطلب" });
    }

    [HttpGet("admin/all")]
    public async Task<IActionResult> GetAllOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var orders = await _orderService.GetAllOrdersAsync(page, pageSize);

        return Ok(new
        {
            data = orders.Select(o => new
            {
                id = o.Id,
                userId = o.UserId,
                status = o.Status.ToString(),
                total = o.Total,
                paymentMethod = o.PaymentMethod,
                createdAt = o.CreatedAt
            }),
            page,
            pageSize,
            message = "تم جلب جميع الطلبات بنجاح"
        });
    }
}

public record CreateOrderRequest(int UserId, int AddressId, string PaymentMethod, string? Notes = null);