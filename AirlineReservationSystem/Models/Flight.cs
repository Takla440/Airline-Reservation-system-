namespace AirlineReservationSystem.Models
{
    // Models/Flight.cs
    public class Flight
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public string OriginCity { get; set; }
        public string DestinationCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int Duration { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public int Price { get; set; }
    }
}
