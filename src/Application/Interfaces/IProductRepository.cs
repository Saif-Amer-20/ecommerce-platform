using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> SearchAsync(string? search, int? categoryId, string? sort, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Product?> GetWithVariantsAsync(int id, CancellationToken cancellationToken = default);
}