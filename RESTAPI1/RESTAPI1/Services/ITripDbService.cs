using RESTAPI1.DTOs;

namespace RESTAPI1.Services;

public interface ITripDbService
{
    Task<IList<TripDTO>> GetTripsListAsync(CancellationToken cancellation = default);
    
    Task<bool> DeleteClientAsync(int idClient);
    
    Task<bool> AddClientToTripAsync(ClientTripDTO clientTrip);
}