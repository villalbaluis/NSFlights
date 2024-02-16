namespace NSFlightsBusiness.Entities
{
    public class Journey
    {
        public int Id { get; set; }

        public List<Flight> Flights { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public double Price { get; set; }
    }
}
