using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces;

/// <summary>
/// خدمة إدارة الكتالوج والمنتجات
/// Catalog Management Service Interface
/// </summary>
public interface ICatalogService
{
    // Category Management
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int categoryId);
    Task<Category> CreateCategoryAsync(string name, string? description = null, int? parentId = null);
    Task<bool> UpdateCategoryAsync(int categoryId, string name, string? description = null, int? parentId = null);
    Task<bool> DeleteCategoryAsync(int categoryId);
    Task<IEnumerable<Category>> GetChildCategoriesAsync(int parentId);
    
    // Product Management
    Task<IEnumerable<Product>> GetProductsAsync(int pageNumber = 1, int pageSize = 20, string? categorySlug = null, string? searchTerm = null);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product?> GetProductBySlugAsync(string slug);
    Task<Product> CreateProductAsync(string name, string slug, decimal price, string? description = null, int? categoryId = null);
    Task<bool> UpdateProductAsync(int productId, string name, string slug, decimal price, string? description = null, int? categoryId = null);
    Task<bool> DeleteProductAsync(int productId);
    Task<bool> UpdateProductPricingAsync(int productId, decimal price, decimal? costPrice = null);
    
    // Product Variants
    Task<IEnumerable<ProductVariant>> GetProductVariantsAsync(int productId);
    Task<ProductVariant> CreateProductVariantAsync(int productId, string sku, decimal price, decimal? discountedPrice = null);
    Task<bool> UpdateProductVariantAsync(int variantId, string sku, decimal price, decimal? discountedPrice = null);
    Task<bool> DeleteProductVariantAsync(int variantId);
    
    // Product Attributes
    Task<IEnumerable<Domain.Entities.Attribute>> GetAttributesAsync();
    Task<Domain.Entities.Attribute> CreateAttributeAsync(string name);
    Task<AttributeValue> AddAttributeValueAsync(int attributeId, string value);
    Task<bool> AssignAttributeToVariantAsync(int variantId, int attributeId, int attributeValueId);
    
    // Media Management
    Task<MediaAsset> AddProductImageAsync(int productId, string url, string? alt = null);
    Task<bool> RemoveProductImageAsync(int imageId);
    Task<IEnumerable<MediaAsset>> GetProductImagesAsync(int productId);
    
    // Inventory Integration
    Task<int> GetProductStockAsync(int productId, int? warehouseId = null);
    Task<bool> CheckProductAvailabilityAsync(int productId, int quantity, int? warehouseId = null);
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10);
    
    // Bulk Operations
    Task<bool> BulkImportProductsAsync(IEnumerable<object> products);
    Task<bool> BulkUpdatePricingAsync(Dictionary<int, decimal> productPrices);
    Task<bool> BulkUpdateCategoryAsync(IEnumerable<int> productIds, int newCategoryId);
    
    // Search & Filtering
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm, decimal? minPrice = null, decimal? maxPrice = null, 
        IEnumerable<int>? categoryIds = null, int pageNumber = 1, int pageSize = 20);
    Task<IEnumerable<Product>> GetFeaturedProductsAsync(int count = 10);
    Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId, int count = 5);
    
    // Reviews Integration
    Task<double> GetProductAverageRatingAsync(int productId);
    Task<int> GetProductReviewCountAsync(int productId);
}