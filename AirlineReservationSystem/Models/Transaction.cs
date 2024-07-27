namespace AirlineReservationSystem.Models
{
    // Models/Transaction.cs
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
        public string TransactionType { get; set; }
    }

}
