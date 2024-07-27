using System.ComponentModel.DataAnnotations;

namespace AirlineReservationSystem.Models
{
    public class BookingViewModel
    {
       
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Trip { get; set; } // "round" or "one-way"

        public int NumberOfPassengers { get; set; }
    }
}
