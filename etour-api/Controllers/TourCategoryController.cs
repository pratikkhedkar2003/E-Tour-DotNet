
using etour_api.Models;
using etour_api.Payload.Response;
using etour_api.Services;
using etour_api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace etour_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourCategoryController : ControllerBase
{
    private readonly ITourCategoryService _tourCategoryService;

    public TourCategoryController(ITourCategoryService tourCategoryService)
    {
        _tourCategoryService = tourCategoryService;
    }

    [HttpGet]
    public async Task<ActionResult<Response>> GetAllTourCategories()
    {
        List<TourCategory> tourCategories = await _tourCategoryService.GetAllTourCategories(); 
        return Ok(RequestUtils.GetResponse(path: "api/tourCategory", code: 200, message: "Tour categories fetched successfully", data: new Dictionary<string, object> { { "tourCategories", tourCategories } }));
    }
}