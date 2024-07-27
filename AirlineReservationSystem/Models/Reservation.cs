namespace AirlineReservationSystem.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; }
        public int TotalPrice { get; set; }
        public int SkyMilesEarned { get; set; }
    }

}
