
using etour_api.Exceptions;
using etour_api.Models;
using etour_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Services.Impl;

public class TourSubCategoryServiceImpl : ITourSubCategoryService
{
    private readonly AppDbContext _appDbContext;

    public TourSubCategoryServiceImpl(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<TourSubcategory>> GetAllTourSubcategories()
    {
        return await _appDbContext.TourSubcategories.ToListAsync();
    }

    public async Task<List<TourSubcategory>> GetTourSubCategoriesByTourCategoryId(ulong tourCategoryId)
    {
        TourCategory tourCategory = await _appDbContext.TourCategories.FirstOrDefaultAsync(t => t.Id == tourCategoryId) ?? throw new ApiException("Tour Category not found.");
        return await _appDbContext.TourSubcategories.Where(t => t.TourCategory.Id == tourCategoryId).ToListAsync();
    }
}