using System.Collections;

namespace RESTAPI1.Entities;

public class Trip
{
    public int IdTrip { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }

    public virtual ICollection<Country_Trip> CountryTrips { get; set; } = new List<Country_Trip>();

    public virtual ICollection<Client_Trip> ClientTrips { get; set; } = new List<Client_Trip>();
    
}