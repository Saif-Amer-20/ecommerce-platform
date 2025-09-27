using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<IReadOnlyList<Category>> GetAllWithChildrenAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Categories
            .Include(c => c.Children)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}