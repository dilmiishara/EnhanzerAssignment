using Enhanzer.Api.Data;
using Enhanzer.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Enhanzer.Api.Services;

public class LocationService
{
    private readonly AppDbContext _context;

    public LocationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveOrUpdateLocationsAsync(
        IEnumerable<ExternalLocation> locations)
    {
        var validLocations = locations
            .Where(location =>
                !string.IsNullOrWhiteSpace(location.LocationCode) &&
                !string.IsNullOrWhiteSpace(location.LocationName))
            .GroupBy(location => location.LocationCode)
            .Select(group => group.First())
            .ToList();

        if (validLocations.Count == 0)
        {
            return 0;
        }

        var locationCodes = validLocations
            .Select(location => location.LocationCode)
            .ToList();

        var existingLocations = await _context.LocationDetails
            .Where(location =>
                locationCodes.Contains(location.LocationCode))
            .ToDictionaryAsync(
                location => location.LocationCode
            );

        foreach (var location in validLocations)
        {
            if (existingLocations.TryGetValue(
                location.LocationCode,
                out var existingLocation))
            {
                existingLocation.LocationName =
                    location.LocationName;
            }
            else
            {
                _context.LocationDetails.Add(
                    new LocationDetail
                    {
                        LocationCode = location.LocationCode,
                        LocationName = location.LocationName
                    }
                );
            }
        }

        await _context.SaveChangesAsync();

        return validLocations.Count;
    }
}