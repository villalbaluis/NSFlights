namespace NSFlightsBusiness.NewFolder
{
    public class Flights
    {
        public string DepartureStation { get; set; } = null!;
        public string ArrivalStation { get; set; } = null!;
        public double FlightCarrier { get; set; }
        public double FlightNumber { get; set; }
        public double Price { get; set; }
    }
}
