using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _catalogService.GetCategoriesAsync();

        return Ok(new
        {
            data = categories.Select(c => new
            {
                id = c.Id,
                name = c.Name,
                slug = c.Slug,
                description = c.Description,
                parentId = c.ParentId
            }),
            message = "تم جلب الفئات بنجاح"
        });
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var category = await _catalogService.CreateCategoryAsync(request.Name, request.Description, request.ParentId);

        return CreatedAtAction(nameof(GetCategories), new
        {
            id = category.Id,
            name = category.Name,
            slug = category.Slug,
            description = category.Description,
            parentId = category.ParentId
        });
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
    {
        var products = await _catalogService.GetProductsAsync(page, pageSize, null, search);

        return Ok(new
        {
            data = products.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                slug = p.Slug,
                price = p.Price,
                description = p.Description,
                sku = p.SKU,
                categoryId = p.CategoryId,
                isActive = p.IsActive
            }),
            page,
            pageSize,
            message = "تم جلب المنتجات بنجاح"
        });
    }

    [HttpGet("products/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _catalogService.GetProductByIdAsync(id);
        
        if (product != null)
        {
            return Ok(new 
            {
                id = product.Id,
                name = product.Name,
                slug = product.Slug,
                price = product.Price,
                costPrice = product.CostPrice,
                description = product.Description,
                sku = product.SKU,
                categoryId = product.CategoryId,
                isActive = product.IsActive,
                createdAt = product.CreatedAt
            });
        }

        return NotFound(new { message = "المنتج غير موجود" });
    }

    [HttpPost("products")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var product = await _catalogService.CreateProductAsync(
            request.Name, 
            request.Slug, 
            request.Price, 
            request.Description, 
            request.CategoryId);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new
        {
            id = product.Id,
            name = product.Name,
            slug = product.Slug,
            price = product.Price,
            description = product.Description,
            sku = product.SKU,
            categoryId = product.CategoryId
        });
    }

    [HttpGet("products/{id}/stock")]
    public async Task<IActionResult> GetProductStock(int id)
    {
        var stock = await _catalogService.GetProductStockAsync(id);
        var isAvailable = await _catalogService.CheckProductAvailabilityAsync(id, 1);

        return Ok(new
        {
            productId = id,
            stockQuantity = stock,
            isAvailable = isAvailable,
            message = "تم جلب معلومات المخزون بنجاح"
        });
    }
}

public record CreateCategoryRequest(string Name, string? Description = null, int? ParentId = null);
public record CreateProductRequest(string Name, string Slug, decimal Price, string? Description = null, int? CategoryId = null);