using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Entities;

/// <summary>
/// إدارة المخزون - Inventory Management
/// </summary>
public class InventoryItem : BaseEntity
{
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = default!;
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = default!;
    
    public required int QuantityOnHand { get; set; }
    public required int QuantityReserved { get; set; }
    public required int QuantityAvailable { get; set; }
    public int ReorderLevel { get; set; }
    public int MaxStockLevel { get; set; }
    
    public List<InventoryMovement> Movements { get; } = new();
}

/// <summary>
/// المستودعات - Warehouses
/// </summary>
public class Warehouse : BaseEntity
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; } = false;
    
    // Manager
    public int? ManagerId { get; set; }
    public User? Manager { get; set; }
    
    public List<InventoryItem> InventoryItems { get; } = new();
    public List<InventoryMovement> Movements { get; } = new();
}

/// <summary>
/// حركات المخزون - Inventory Movements
/// </summary>
public class InventoryMovement : BaseEntity
{
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = default!;
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = default!;
    
    public required MovementType Type { get; set; }
    public required int Quantity { get; set; }
    public required string Reference { get; set; }
    public string? Notes { get; set; }
    
    // Cost Information
    public decimal? UnitCost { get; set; }
    public decimal? TotalCost { get; set; }
    
    // Related Records
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    
    public int CreatedBy { get; set; }
    public User CreatedByUser { get; set; } = default!;
}

/// <summary>
/// الموردون - Suppliers
/// </summary>
public class Supplier : BaseEntity
{
    public required string Name { get; set; }
    public required string CompanyName { get; set; }
    public string? TaxId { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public string? Website { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Banking Information (encrypted)
    public string? BankName { get; set; }
    public string? BankAccountNumber { get; set; }
    public string? BankRoutingNumber { get; set; }
    
    // Contact Person
    public string? ContactPersonName { get; set; }
    public string? ContactPersonPhone { get; set; }
    public string? ContactPersonEmail { get; set; }
    
    public List<SupplierProduct> SupplierProducts { get; } = new();
    public List<PurchaseOrder> PurchaseOrders { get; } = new();
}

/// <summary>
/// منتجات الموردين - Supplier Products
/// </summary>
public class SupplierProduct : BaseEntity
{
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = default!;
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = default!;
    
    public required string SupplierSku { get; set; }
    public required decimal SupplierPrice { get; set; }
    public required string Currency { get; set; } = "IQD";
    public int LeadTimeDays { get; set; }
    public int MinOrderQuantity { get; set; }
    public bool IsPreferred { get; set; } = false;
    public DateTime? LastPurchaseDate { get; set; }
}

/// <summary>
/// أوامر الشراء - Purchase Orders
/// </summary>
public class PurchaseOrder : BaseEntity
{
    public required string OrderNumber { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = default!;
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = default!;
    
    public required PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Draft;
    public decimal TotalAmount { get; set; }
    public required string Currency { get; set; } = "IQD";
    public DateTime? OrderDate { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public string? Notes { get; set; }
    
    public int CreatedBy { get; set; }
    public User CreatedByUser { get; set; } = default!;
    
    public List<PurchaseOrderItem> Items { get; } = new();
}

/// <summary>
/// عناصر أوامر الشراء - Purchase Order Items
/// </summary>
public class PurchaseOrderItem : BaseEntity
{
    public int PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; } = default!;
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = default!;
    
    public required int QuantityOrdered { get; set; }
    public int QuantityReceived { get; set; } = 0;
    public required decimal UnitCost { get; set; }
    public required decimal TotalCost { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// عمليات التحضير والتعبئة - Pick & Pack Operations
/// </summary>
public class PickingList : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = default!;
    
    public required PickingStatus Status { get; set; } = PickingStatus.Pending;
    public int? AssignedTo { get; set; }
    public User? AssignedUser { get; set; }
    
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Notes { get; set; }
    
    public List<PickingListItem> Items { get; } = new();
}

/// <summary>
/// عناصر قائمة التحضير - Picking List Items
/// </summary>
public class PickingListItem : BaseEntity
{
    public int PickingListId { get; set; }
    public PickingList PickingList { get; set; } = default!;
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = default!;
    
    public required int QuantityToPick { get; set; }
    public int QuantityPicked { get; set; } = 0;
    public required string Location { get; set; } // A1-B2-C3
    public bool IsCompleted { get; set; } = false;
    public string? Notes { get; set; }
}

/// <summary>
/// جرد المخزون - Stock Take
/// </summary>
public class StockTake : BaseEntity
{
    public required string Reference { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = default!;
    
    public required StockTakeStatus Status { get; set; } = StockTakeStatus.Planning;
    public DateTime? ScheduledDate { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    
    public int CreatedBy { get; set; }
    public User CreatedByUser { get; set; } = default!;
    public string? Notes { get; set; }
    
    public List<StockTakeItem> Items { get; } = new();
}

/// <summary>
/// عناصر جرد المخزون - Stock Take Items
/// </summary>
public class StockTakeItem : BaseEntity
{
    public int StockTakeId { get; set; }
    public StockTake StockTake { get; set; } = default!;
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = default!;
    
    public required int SystemQuantity { get; set; }
    public int? CountedQuantity { get; set; }
    public int? Variance { get; set; }
    public string? Notes { get; set; }
    public bool IsCompleted { get; set; } = false;
    
    public int? CountedBy { get; set; }
    public User? CountedByUser { get; set; }
    public DateTime? CountedAt { get; set; }
}

/// <summary>
/// أنواع حركات المخزون - Movement Types
/// </summary>
public enum MovementType
{
    Purchase = 0,           // شراء
    Sale = 1,              // بيع
    Adjustment = 2,        // تعديل
    Transfer = 3,          // نقل
    Return = 4,            // إرجاع
    Damage = 5,            // تلف
    StockTake = 6,         // جرد
    Production = 7,        // إنتاج
    Reservation = 8,       // حجز
    ReservationRelease = 9 // إلغاء الحجز
}

/// <summary>
/// حالات أوامر الشراء - Purchase Order Status
/// </summary>
public enum PurchaseOrderStatus
{
    Draft = 0,
    Sent = 1,
    Confirmed = 2,
    PartiallyReceived = 3,
    Received = 4,
    Cancelled = 5
}

/// <summary>
/// حالات التحضير - Picking Status
/// </summary>
public enum PickingStatus
{
    Pending = 0,
    InProgress = 1,
    Completed = 2,
    Cancelled = 3
}

/// <summary>
/// حالات الجرد - Stock Take Status
/// </summary>
public enum StockTakeStatus
{
    Planning = 0,
    InProgress = 1,
    Completed = 2,
    Cancelled = 3
}