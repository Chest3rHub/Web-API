namespace RESTAPI1.Entities;

public class Country
{
    public int IdCountry { get; set; }
    
    public string Name { get; set; }
    
    public virtual ICollection<Country_Trip> CountryTrips { get; set; } = new List<Country_Trip>();
}