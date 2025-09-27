using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces;

/// <summary>
/// خدمة إدارة الطلبات والمبيعات
/// Order Management Service Interface
/// </summary>
public interface IOrderService
{
    // Cart Management
    Task<Cart> GetUserCartAsync(int userId);
    Task<bool> AddToCartAsync(int userId, int productId, int quantity, int? productVariantId = null);
    Task<bool> UpdateCartItemAsync(int userId, int cartItemId, int quantity);
    Task<bool> RemoveFromCartAsync(int userId, int cartItemId);
    Task<bool> ClearCartAsync(int userId);
    
    // Order Creation & Management
    Task<Order> CreateOrderAsync(int userId, int addressId, string paymentMethod, string? notes = null, string? couponCode = null);
    Task<IEnumerable<Order>> GetUserOrdersAsync(int userId, int pageNumber = 1, int pageSize = 10);
    Task<Order?> GetOrderDetailsAsync(int orderId, int userId);
    Task<bool> CancelOrderAsync(int orderId, int userId);
    
    // Admin Order Management
    Task<IEnumerable<Order>> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 20, string? status = null);
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<bool> UpdateOrderStatusAsync(int orderId, string status, string? notes = null);
    Task<bool> AssignCourierAsync(int orderId, int courierId);
    
    // Payment Management
    Task<Payment?> ProcessPaymentAsync(int orderId, string paymentMethod, decimal amount);
    Task<bool> UpdatePaymentStatusAsync(int paymentId, string status);
    Task<IEnumerable<Payment>> GetOrderPaymentsAsync(int orderId);
    
    // Shipment Management
    Task<Shipment> CreateShipmentAsync(int orderId, int? courierId = null);
    Task<bool> UpdateShipmentStatusAsync(int shipmentId, string status, string? notes = null);
    Task<IEnumerable<Shipment>> GetCourierShipmentsAsync(int courierId, string? status = null);
    Task<bool> UpdateDeliveryLocationAsync(int shipmentId, decimal latitude, decimal longitude);
    
    // Courier Management
    Task<IEnumerable<Order>> GetCourierOrdersAsync(int courierId, string? status = null);
    Task<bool> AcceptDeliveryAsync(int shipmentId, int courierId);
    Task<bool> CompleteDeliveryAsync(int shipmentId, int courierId, string? deliveryNotes = null);
    
    // Reviews & Ratings
    Task<Review> AddReviewAsync(int userId, int productId, int rating, string? comment = null);
    Task<IEnumerable<Review>> GetProductReviewsAsync(int productId, int pageNumber = 1, int pageSize = 10);
    Task<bool> UpdateReviewAsync(int reviewId, int userId, int rating, string? comment = null);
    
    // Coupons
    Task<Coupon?> ValidateCouponAsync(string code, decimal orderTotal);
    Task<decimal> CalculateDiscountAsync(string couponCode, decimal orderTotal);
    
    // Reports & Analytics
    Task<object> GetSalesReportAsync(DateTime fromDate, DateTime toDate);
    Task<object> GetOrderStatusReportAsync();
    Task<object> GetCourierPerformanceAsync(int? courierId = null);
}