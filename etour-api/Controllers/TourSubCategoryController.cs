
using etour_api.Models;
using etour_api.Payload.Response;
using etour_api.Services;
using etour_api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace etour_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourSubCategoryController : ControllerBase
{
    private readonly ITourSubCategoryService _tourSubCategoryService;

    public TourSubCategoryController(ITourSubCategoryService tourSubCategoryService)
    {
        _tourSubCategoryService = tourSubCategoryService;
    }

    [HttpGet]
    public async Task<ActionResult<Response>> GetAllTourSubCategories()
    {
        List<TourSubcategory> tourSubcategories = await _tourSubCategoryService.GetAllTourSubcategories();
        return Ok(RequestUtils.GetResponse(path: "api/tourSubCategory", code: 200, message: string.Empty, data: new Dictionary<string, object> { { "tourSubcategories", tourSubcategories } }));
    }

    [HttpGet("tourcategory/{tourCategoryId}")]
    public async Task<ActionResult<Response>> GetTourSubCategoriesByTourCategory(ulong tourCategoryId)
    {
        List<TourSubcategory> tourSubcategories = await _tourSubCategoryService.GetTourSubCategoriesByTourCategoryId(tourCategoryId);
        return Ok(RequestUtils.GetResponse(path: "api/tourSubCategory", code: 200, message: "Tour Sub categories retrieved", data: new Dictionary<string, object> { { "tourSubcategories", tourSubcategories } }));
    }

}

