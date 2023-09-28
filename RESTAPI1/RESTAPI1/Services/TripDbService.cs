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
            .ThenInclude(ct => ct.IdClient)
            .Include(x => x.IdCountries)
            .Select(x => new TripDTO(x.Name, x.Description, x.DateFrom, x.DateTo, x.MaxPeople, x.ClientTrips, x.IdCountries))
            .ToListAsync(cancellationToken);
    }
   
   public async Task<bool> DeleteClientAsync(int idClient)
   {
       bool hasAssignedTrips = _context.ClientTrips.Any(ct => ct.IdClient == idClient);

       if (hasAssignedTrips)
       {
           return false;
       }
            
       var client = _context.Clients.Find(idClient);

       if (client == null)
       {
           return false;
       }

       _context.Clients.Remove(client);
       _context.SaveChanges();

       return true;
   }
}