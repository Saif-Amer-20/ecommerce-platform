using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

/// <summary>
/// الكيانات الأساسية الموحدة للنظام
/// Core unified entities for the system
/// </summary>

// Legacy Catalog entity for compatibility
public class Catalog : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}

// Core Product Entities
public class Category : BaseEntity
{
    public required string Name { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public Category? Parent { get; set; }
    public List<Category> Children { get; } = new();
    public List<Product> Products { get; } = new();
}

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public required decimal Price { get; set; }
    public decimal? CostPrice { get; set; }
    public string? Description { get; set; }
    public string? SKU { get; set; }
    public bool IsActive { get; set; } = true;
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<ProductVariant> Variants { get; } = new();
    public List<MediaAsset> Images { get; } = new();
}

public class ProductVariant : BaseEntity
{
    public required string Sku { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public List<ProductVariantAttribute> Attributes { get; } = new();
}

// Product Attributes
public class Attribute : BaseEntity
{
    public required string Name { get; set; }
    public List<AttributeValue> Values { get; } = new();
}

public class AttributeValue : BaseEntity
{
    public int AttributeId { get; set; }
    public Attribute Attribute { get; set; } = default!;
    public required string Value { get; set; }
}

public class ProductVariantAttribute
{
    public int ProductVariantId { get; set; }
    public ProductVariant Variant { get; set; } = default!;
    public int AttributeId { get; set; }
    public Attribute Attribute { get; set; } = default!;
    public int AttributeValueId { get; set; }
    public AttributeValue AttributeValue { get; set; } = default!;
}

// Media Assets
public class MediaAsset : BaseEntity
{
    public required string Url { get; set; }
    public string? Alt { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
}