using Enhanzer.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Enhanzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LocationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public LocationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        var locations = await _context.LocationDetails
            .OrderBy(location => location.LocationName)
            .Select(location => new
            {
                locationCode = location.LocationCode,
                locationName = location.LocationName
            })
            .ToListAsync();

        return Ok(locations);
    }
}