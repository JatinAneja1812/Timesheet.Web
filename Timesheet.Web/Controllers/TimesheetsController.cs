using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimesheetApp.Web.Models;

namespace TimesheetApp.Web.Controllers
{
    public class TimesheetsController : Controller
    {
        private readonly TimesheetDbContext _context;

        public TimesheetsController(TimesheetDbContext context)
        {
            _context = context;
        }

        // GET: Timesheets
        public async Task<IActionResult> Index()
        {
            var timesheetDbContext = _context.Timesheets.Include(t => t.Client).Include(t => t.Location).Include(t => t.Staff);
            return View(await timesheetDbContext.ToListAsync());
        }

        // GET: Timesheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheet = await _context.Timesheets
                .Include(t => t.Client)
                .Include(t => t.Location)
                .Include(t => t.Staff)
                .FirstOrDefaultAsync(m => m.TimesheetId == id);
            if (timesheet == null)
            {
                return NotFound();
            }

            return View(timesheet);
        }

        // GET: Timesheets/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "BillingAddress");
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Address");
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Email");
            return View();
        }

        // POST: Timesheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimesheetId,MinutesWorked,StaffId,ClientId,LocationId")] TimesheetApp.Web.Models.Timesheet
             timesheet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timesheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "BillingAddress", timesheet.ClientId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Address", timesheet.LocationId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Email", timesheet.StaffId);
            return View(timesheet);
        }

        // GET: Timesheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheet = await _context.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "BillingAddress", timesheet.ClientId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Address", timesheet.LocationId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Email", timesheet.StaffId);
            return View(timesheet);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimesheetId,MinutesWorked,StaffId,ClientId,LocationId")] Models.Timesheet timesheet)
        {
            if (id != timesheet.TimesheetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timesheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimesheetExists(timesheet.TimesheetId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "BillingAddress", timesheet.ClientId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Address", timesheet.LocationId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Email", timesheet.StaffId);
            return View(timesheet);
        }

        // GET: Timesheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheet = await _context.Timesheets
                .Include(t => t.Client)
                .Include(t => t.Location)
                .Include(t => t.Staff)
                .FirstOrDefaultAsync(m => m.TimesheetId == id);
            if (timesheet == null)
            {
                return NotFound();
            }

            return View(timesheet);
        }

        // POST: Timesheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timesheet = await _context.Timesheets.FindAsync(id);
            _context.Timesheets.Remove(timesheet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimesheetExists(int id)
        {
            return _context.Timesheets.Any(e => e.TimesheetId == id);
        }
    }
}
