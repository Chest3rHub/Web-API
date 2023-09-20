using RESTAPI1.Entities;

namespace RESTAPI1.DTOs;

public class TripDTO
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }

    public ICollection<ClientDTO> Clients { get; set; }

    public ICollection<CountryDTO> Countries { get; set; }

    public TripDTO(string name, string description, DateTime datefrom, DateTime dateto, int maxPeople,
        ICollection<Client_Trip> clientTrip, ICollection<Country> country)
    {
        Name = name;
        Description = description;
        DateFrom = datefrom;
        DateTo = dateto;
        MaxPeople = maxPeople;

        Countries = country.Select(c => new CountryDTO { Name = c.Name }).ToList();

        Clients = clientTrip.Select(ct => new ClientDTO
        {
            FirstName = ct.Client.FirstName,
            LastName = ct.Client.LastName
        }).ToList();
    }
}