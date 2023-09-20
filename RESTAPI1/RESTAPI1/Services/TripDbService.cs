using Microsoft.EntityFrameworkCore;
using RESTAPI1.DTOs;
using RESTAPI1.Entities;

namespace RESTAPI1.Services;

public class TripDbService : ITripDbService
{
    private readonly _TravelDbContext _context;
    public TripDbService(_TravelDbContext context)
    {
        _context = context;
    }
   public async Task<IList<TripDTO>> GetTripsListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Trips
            .OrderByDescending(x => x.DateFrom)
            .Include(x => x.ClientTrips)
            .ThenInclude(ct => ct.Client)
            .Include(x => x.CountryTrips)
            .Select(x => new TripDTO(x.Name, x.Description, x.DateFrom, x.DateTo, x.MaxPeople, x.ClientTrips, ////))
            .ToListAsync(cancellationToken);

    }
}