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
   
   public async Task<bool> AddClientToTripAsync(ClientTripDTO clientTrip)
   {
       var trip = _context.Trips.FirstOrDefault(t => t.IdTrip == clientTrip.IdTrip);

       if (trip == null) 
       { 
           throw new Exception("Taka wycieczka nie istnieje"); 
       }

       var existingClient = _context.Clients.FirstOrDefault(c => c.Pesel == clientTrip.Pesel);

       if (existingClient == null)
       {
           var newClient = new Client
           {
               IdClient = _context.Clients.Max(cl => cl.IdClient) + 1,
               FirstName = clientTrip.FirstName,
               LastName = clientTrip.LastName,
               Email = clientTrip.Email,
               Telephone = clientTrip.Telephone,
               Pesel = clientTrip.Pesel
           };

           _context.Clients.Add(newClient);
           _context.SaveChanges();

           existingClient = newClient;
       }

       bool isClientAssigned = _context.ClientTrips.Any(ct => ct.IdClient == existingClient.IdClient && ct.IdTrip == clientTrip.IdTrip);

       if (isClientAssigned)
       {
           throw new Exception("Klient jest już zapisany na ta wycieczke");
       }

       var assignClient = new Client_Trip
       {
           IdClient = existingClient.IdClient,
           IdTrip = clientTrip.IdTrip,
           RegisteredAt = DateTime.Now,
           PaymentDate = clientTrip.PaymentDate
       };

       _context.ClientTrips.Add(assignClient);
       _context.SaveChanges();

       return true;
   }
}