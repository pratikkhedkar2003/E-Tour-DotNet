
using etour_api.Models;
using etour_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Services.Impl;

public class TourCategoryServiceImpl : ITourCategoryService
{
    private readonly AppDbContext _appDbContext;

    public TourCategoryServiceImpl(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<TourCategory>> GetAllTourCategories()
    {
        return await _appDbContext.TourCategories.ToListAsync();
    }
}