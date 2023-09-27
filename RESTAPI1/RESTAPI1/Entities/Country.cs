namespace RESTAPI1.Entities;

public class Country
{
    public int IdCountry { get; set; }
    
    public string Name { get; set; }
    
    public virtual ICollection<Trip> IdTrips { get; set; } = new List<Trip>();
}