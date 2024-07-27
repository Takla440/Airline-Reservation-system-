using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlineReservationSystem.Models;

namespace AirlineReservationSystem.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly AirlineDbContext _context;

        public BookingController(AirlineDbContext context)
        {
            _context = context;
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel bookingViewModel)
        {
            if (ModelState.IsValid)
            {
                // Find the flight that matches the 'From' and 'To' locations
                var flight = await _context.Flights
                    .FirstOrDefaultAsync(f => f.OriginCity == bookingViewModel.From && f.DestinationCity == bookingViewModel.To);

                if (flight == null)
                {
                    ModelState.AddModelError("", "No flight available for the selected route.");
                    return View(bookingViewModel);
                }

                // Create a new reservation for each passenger
                var reservation = new Reservation
                {
                    FlightId = flight.FlightId,
                    UserId = 001,
                    ReservationDate = bookingViewModel.DepartureDate,
                   
                };


                _context.Reservations.Add(reservation);
                

                await _context.SaveChangesAsync();

                if (bookingViewModel.Trip == "round" && bookingViewModel.ReturnDate != null)
                {
                    // Handle round trip reservation
                    var returnFlight = await _context.Flights
                        .FirstOrDefaultAsync(f => f.OriginCity == bookingViewModel.To && f.DestinationCity == bookingViewModel.From);

                    if (returnFlight != null)
                    {
                        
                            var returnReservation = new Reservation
                            {
                                FlightId = returnFlight.FlightId,
                                UserId = 001,
                                ReservationDate = bookingViewModel.ReturnDate,
                            };

                            _context.Reservations.Add(returnReservation);
                        

                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(bookingViewModel);
        }
    }
}
