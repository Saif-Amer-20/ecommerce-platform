using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<IReadOnlyList<Product>> SearchAsync(string? search, int? categoryId, string? sort, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = _dbContext.Products.Include(p => p.Variants).ThenInclude(v => v.Attributes);
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search) || p.Slug.Contains(search));
        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);
        // sorting
        query = sort switch
        {
            "price-asc" => query.OrderBy(p => p.Price),
            "price-desc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };
        return await query.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetWithVariantsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .Include(p => p.Variants)
                .ThenInclude(v => v.Attributes)
                    .ThenInclude(a => a.Attribute)
            .Include(p => p.Images)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}