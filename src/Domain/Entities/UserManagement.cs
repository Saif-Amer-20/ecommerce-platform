using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Entities;

/// <summary>
/// نظام إدارة المستخدمين والأدوار والصلاحيات
/// User Management System with Roles and Permissions
/// </summary>
public class User : BaseEntity
{
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string FullName { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;
    public bool EmailVerified { get; set; } = false;
    public bool PhoneVerified { get; set; } = false;
    public bool MfaEnabled { get; set; } = false;
    public DateTime? LastLoginAt { get; set; }
    public string? SessionId { get; set; }
    public int LoyaltyPoints { get; set; } = 0;
    public int? DefaultAddressId { get; set; }
    
    // Additional properties for compatibility
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserType UserType { get; set; } = UserType.Customer;
    
    // Navigation Properties
    public List<UserRole> UserRoles { get; } = new();
    public List<Address> Addresses { get; } = new();
    public UserPreferences? Preferences { get; set; }
    public List<Order> Orders { get; } = new();
    public List<Review> Reviews { get; } = new();
    public List<AuditLog> AuditLogs { get; } = new();
}

/// <summary>
/// الأدوار - Roles (Super Admin, Catalog Manager, etc.)
/// </summary>
public class Role : BaseEntity
{
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }
    public bool IsSystemRole { get; set; } = false;
    
    // Navigation Properties
    public List<UserRole> UserRoles { get; } = new();
    public List<RolePermission> RolePermissions { get; } = new();
}

/// <summary>
/// الصلاحيات الذرية - Atomic Permissions (products.read, orders.write, etc.)
/// </summary>
public class Permission : BaseEntity
{
    public required string Name { get; set; }
    public required string Resource { get; set; }
    public required string Action { get; set; }
    public string? Description { get; set; }
    
    // Navigation Properties
    public List<RolePermission> RolePermissions { get; } = new();
}

/// <summary>
/// ربط المستخدمين بالأدوار - User-Role Junction
/// </summary>
public class UserRole
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public int RoleId { get; set; }
    public Role Role { get; set; } = default!;
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public int? AssignedBy { get; set; }
}

/// <summary>
/// ربط الأدوار بالصلاحيات - Role-Permission Junction
/// </summary>
public class RolePermission
{
    public int RoleId { get; set; }
    public Role Role { get; set; } = default!;
    public int PermissionId { get; set; }
    public Permission Permission { get; set; } = default!;
}

/// <summary>
/// تفضيلات المستخدم - User Preferences
/// </summary>
public class UserPreferences : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public string Language { get; set; } = "ar";
    public string Currency { get; set; } = "IQD";
    public bool MarketingOptIn { get; set; } = false;
    public bool SmsNotifications { get; set; } = true;
    public bool EmailNotifications { get; set; } = true;
    public string? Theme { get; set; } = "light";
}

/// <summary>
/// عناوين المستخدمين - User Addresses
/// </summary>
public class Address : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public required string Label { get; set; } // المنزل، العمل، إلخ
    public required string CityId { get; set; }
    public required string ZoneId { get; set; }
    public required string Street { get; set; }
    public string? Note { get; set; }
    public bool IsDefault { get; set; } = false;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    
    // Additional properties for compatibility
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}

/// <summary>
/// سجل التدقيق - Audit Log
/// </summary>
public class AuditLog : BaseEntity
{
    public int? UserId { get; set; }
    public User? User { get; set; }
    public required string Action { get; set; }
    public required string Resource { get; set; }
    public string? ResourceId { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public required string IpAddress { get; set; }
    public required string UserAgent { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// أنواع المستخدمين المدعومة
/// Supported User Types
/// </summary>
public enum UserType
{
    Guest = 0,
    Customer = 1,
    SuperAdmin = 2,
    CatalogManager = 3,
    OrderManager = 4,
    WarehouseManager = 5,
    Courier = 6,
    Finance = 7,
    Support = 8,
    Marketing = 9,
    Vendor = 10,
    Auditor = 11,
    ApiClient = 12
}

/// <summary>
/// أنواع المستخدمين المدعومة (ثوابت للتوافق مع الإصدارات السابقة)
/// Supported User Types (constants for backward compatibility)
/// </summary>
public static class UserTypes
{
    public const string Guest = "guest";
    public const string Customer = "customer";
    public const string SuperAdmin = "super_admin";
    public const string CatalogManager = "catalog_manager";
    public const string OrderManager = "order_manager";
    public const string WarehouseManager = "warehouse_manager";
    public const string Courier = "courier";
    public const string Finance = "finance";
    public const string Support = "support";
    public const string Marketing = "marketing";
    public const string Vendor = "vendor";
    public const string Auditor = "auditor";
    public const string ApiClient = "api_client";
}

/// <summary>
/// أذونات النظام - System Permissions
/// </summary>
public static class SystemPermissions
{
    // Catalog Permissions
    public const string CategoriesRead = "categories.read";
    public const string CategoriesWrite = "categories.write";
    public const string CategoriesDelete = "categories.delete";
    public const string ProductsRead = "products.read";
    public const string ProductsWrite = "products.write";
    public const string ProductsDelete = "products.delete";
    public const string VariantsRead = "variants.read";
    public const string VariantsWrite = "variants.write";
    public const string AttributesRead = "attributes.read";
    public const string AttributesWrite = "attributes.write";
    public const string MediaRead = "media.read";
    public const string MediaWrite = "media.write";
    public const string PricingRead = "pricing.read";
    public const string PricingWrite = "pricing.write";
    
    // Inventory Permissions
    public const string InventoryRead = "inventory.read";
    public const string InventoryWrite = "inventory.write";
    public const string InventoryMovementsWrite = "inventory_movements.write";
    public const string PackingWrite = "packing.write";
    
    // Orders & Checkout
    public const string CartsRead = "carts.read";
    public const string CartsWrite = "carts.write";
    public const string OrdersRead = "orders.read";
    public const string OrdersWrite = "orders.write";
    public const string ReturnsRead = "returns.read";
    public const string ReturnsWrite = "returns.write";
    public const string ShipmentsRead = "shipments.read";
    public const string ShipmentsWrite = "shipments.write";
    public const string ShipmentsUpdateStatus = "shipments.updateStatus";
    
    // Payments
    public const string PaymentsRead = "payments.read";
    public const string PaymentsWrite = "payments.write";
    public const string RefundsWrite = "refunds.write";
    
    // Users & RBAC
    public const string UsersRead = "users.read";
    public const string UsersWrite = "users.write";
    public const string RolesRead = "roles.read";
    public const string RolesWrite = "roles.write";
    public const string PermissionsRead = "permissions.read";
    public const string PermissionsWrite = "permissions.write";
    
    // Analytics & Reports
    public const string AnalyticsRead = "analytics.read";
    public const string ReportsRead = "reports.read";
    
    // Settings & Integrations
    public const string SettingsRead = "settings.read";
    public const string SettingsWrite = "settings.write";
    public const string IntegrationsRead = "integrations.read";
    public const string IntegrationsWrite = "integrations.write";
    
    // Marketing & Campaigns
    public const string CouponsRead = "coupons.read";
    public const string CouponsWrite = "coupons.write";
    public const string CouponsLimited = "coupons.limited";
    public const string CampaignsRead = "campaigns.read";
    public const string CampaignsWrite = "campaigns.write";
    public const string SegmentsRead = "segments.read";
    public const string SegmentsWrite = "segments.write";
    
    // Audit & Compliance
    public const string AuditRead = "audit.read";
    public const string AuditWrite = "audit.write";
    
    // Super Admin - All permissions
    public const string All = "ALL.*";
}