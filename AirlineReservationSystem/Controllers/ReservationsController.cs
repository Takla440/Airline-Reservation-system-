using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirlineReservationSystem.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ReservationsController : Controller
{
    private readonly AirlineDbContext _context;

    public ReservationsController(AirlineDbContext context)
    {
        _context = context;
    }

    // GET: Reservations
    public async Task<IActionResult> Index()
    {
        var reservations = await _context.Reservations
            .Include(r => r.Flight)
            .Include(r => r.User)
            .ToListAsync();
        return View(reservations);
    }

    // GET: Reservations/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations
            .Include(r => r.Flight)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.ReservationId == id);
        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // GET: Reservations/Create
    public IActionResult Create()
    {
        ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightNumber");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
        return View();
    }

    // POST: Reservations/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ReservationId,UserId,FlightId,ReservationDate,Status,TotalPrice,SkyMilesEarned")] Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            _context.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightNumber", reservation.FlightId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", reservation.UserId);
        return View(reservation);
    }

    // GET: Reservations/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }
        ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightNumber", reservation.FlightId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", reservation.UserId);
        return View(reservation);
    }

    // POST: Reservations/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ReservationId,UserId,FlightId,ReservationDate,Status,TotalPrice,SkyMilesEarned")] Reservation reservation)
    {
        if (id != reservation.ReservationId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(reservation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(reservation.ReservationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightNumber", reservation.FlightId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", reservation.UserId);
        return View(reservation);
    }

    // GET: Reservations/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations
            .Include(r => r.Flight)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.ReservationId == id);
        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // POST: Reservations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ReservationExists(int id)
    {
        return _context.Reservations.Any(e => e.ReservationId == id);
    }
}
