using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Entities;

/// <summary>
/// الطلبات - Orders
/// </summary>
public class Order : BaseEntity
{
    public required string OrderNumber { get; set; }
    public int? CustomerId { get; set; }
    public User? Customer { get; set; }
    
    // Address & Contact Info
    public required string ShippingFullName { get; set; }
    public required string ShippingPhone { get; set; }
    public required string ShippingCity { get; set; }
    public required string ShippingZone { get; set; }
    public required string ShippingStreet { get; set; }
    public string? ShippingNotes { get; set; }
    
    // Order Details
    public decimal SubtotalAmount { get; set; }
    public decimal ShippingAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public required string Currency { get; set; } = "IQD";
    
    // Status & Tracking
    public required OrderStatus Status { get; set; } = OrderStatus.Pending;
    public required PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public string? PaymentReference { get; set; }
    public string? CouponCode { get; set; }
    public string? Notes { get; set; }
    
    // Timestamps
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ConfirmedAt { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    
    // Additional properties for controller compatibility
    public decimal Total => TotalAmount;
    public int? UserId => CustomerId;
    
    // Navigation Properties
    public List<OrderItem> Items { get; } = new();
    public List<OrderStatusHistory> StatusHistory { get; } = new();
    public List<Payment> Payments { get; } = new();
    public List<Shipment> Shipments { get; } = new();
}

/// <summary>
/// عناصر الطلب - Order Items
/// </summary>
public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = default!;
    
    public required string Sku { get; set; }
    public required string ProductName { get; set; }
    public string? VariantName { get; set; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
    public required decimal TotalPrice { get; set; }
}

/// <summary>
/// تاريخ حالة الطلب - Order Status History
/// </summary>
public class OrderStatusHistory : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public required OrderStatus FromStatus { get; set; }
    public required OrderStatus ToStatus { get; set; }
    public required string Reason { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// المدفوعات - Payments
/// </summary>
public class Payment : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    
    public required string PaymentReference { get; set; }
    public required PaymentMethod Method { get; set; }
    public required PaymentStatus Status { get; set; }
    public required decimal Amount { get; set; }
    public required string Currency { get; set; } = "IQD";
    
    public string? ProviderReference { get; set; }
    public string? ProviderResponse { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? FailureReason { get; set; }
}

/// <summary>
/// الشحنات - Shipments
/// </summary>
public class Shipment : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public int? CourierId { get; set; }
    public User? Courier { get; set; }
    
    public required string TrackingNumber { get; set; }
    public required ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;
    public decimal ShippingCost { get; set; }
    public string? CourierNotes { get; set; }
    public string? CustomerNotes { get; set; }
    
    // Additional properties for compatibility
    public string Carrier { get; set; } = string.Empty;
    public DateTime? EstimatedDelivery { get; set; }
    
    // Delivery Proof
    public string? ProofOfDeliveryImage { get; set; }
    public string? RecipientSignature { get; set; }
    public string? DeliveryLatitude { get; set; }
    public string? DeliveryLongitude { get; set; }
    
    // Timestamps
    public DateTime? PickedUpAt { get; set; }
    public DateTime? OutForDeliveryAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? FailedAt { get; set; }
    
    public List<ShipmentStatusHistory> StatusHistory { get; } = new();
}

/// <summary>
/// تاريخ حالة الشحنة - Shipment Status History
/// </summary>
public class ShipmentStatusHistory : BaseEntity
{
    public int ShipmentId { get; set; }
    public Shipment Shipment { get; set; } = default!;
    public required ShipmentStatus FromStatus { get; set; }
    public required ShipmentStatus ToStatus { get; set; }
    public required string Notes { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// سلة التسوق - Shopping Cart
/// </summary>
public class Cart : BaseEntity
{
    public int? UserId { get; set; }
    public User? User { get; set; }
    public required string SessionId { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<CartItem> Items { get; } = new();
}

/// <summary>
/// عناصر سلة التسوق - Cart Items
/// </summary>
public class CartItem : BaseEntity
{
    public int CartId { get; set; }
    public Cart Cart { get; set; } = default!;
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = default!;
    public required int Quantity { get; set; }
}

/// <summary>
/// المراجعات والتقييمات - Reviews
/// </summary>
public class Review : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
    
    public required int Rating { get; set; } // 1-5
    public required string Title { get; set; }
    public required string Content { get; set; }
    public bool IsVerifiedPurchase { get; set; } = false;
    public bool IsApproved { get; set; } = false;
    public DateTime? ApprovedAt { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// قائمة الأمنيات - Wishlist
/// </summary>
public class WishlistItem : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
}

/// <summary>
/// كوبونات الخصم - Coupons
/// </summary>
public class Coupon : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public required CouponType Type { get; set; }
    public required decimal Value { get; set; }
    public decimal? MinimumAmount { get; set; }
    public decimal? MaximumDiscount { get; set; }
    
    public int? UsageLimit { get; set; }
    public int UsageCount { get; set; } = 0;
    public int? UsageLimitPerCustomer { get; set; }
    
    public DateTime? StartsAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    public List<CouponUsage> Usages { get; } = new();
}

/// <summary>
/// استخدام الكوبونات - Coupon Usage
/// </summary>
public class CouponUsage : BaseEntity
{
    public int CouponId { get; set; }
    public Coupon Coupon { get; set; } = default!;
    public int? UserId { get; set; }
    public User? User { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public required decimal DiscountAmount { get; set; }
}

/// <summary>
/// حالات الطلب - Order Status Enum
/// </summary>
public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
    Returned = 6,
    Refunded = 7
}

/// <summary>
/// طرق الدفع - Payment Methods
/// </summary>
public enum PaymentMethod
{
    COD = 0,        // الدفع عند الاستلام
    ZainCash = 1,   // زين كاش
    AsiaHawala = 2, // آسيا حوالة
    BankTransfer = 3, // تحويل مصرفي
    CreditCard = 4    // بطاقة ائتمان
}

/// <summary>
/// حالات الدفع - Payment Status
/// </summary>
public enum PaymentStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4,
    Refunded = 5
}

/// <summary>
/// حالات الشحن - Shipment Status
/// </summary>
public enum ShipmentStatus
{
    Pending = 0,
    PickedUp = 1,
    InTransit = 2,
    OutForDelivery = 3,
    Delivered = 4,
    Failed = 5,
    Returned = 6
}

/// <summary>
/// أنواع الكوبونات - Coupon Types
/// </summary>
public enum CouponType
{
    FixedAmount = 0,    // مبلغ ثابت
    Percentage = 1      // نسبة مئوية
}

/// <summary>
/// نتيجة عملية الدفع - Payment Result
/// </summary>
public class PaymentResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? TransactionId { get; set; }
    public string? ErrorCode { get; set; }
}