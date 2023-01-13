using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymSharp.Data;
using GymSharp.Models;

namespace GymSharp.Controllers
{
    public class MeasurementsController : Controller
    {
        private readonly GymContext _context;

        public MeasurementsController(GymContext context)
        {
            _context = context;
        }

        // GET: Measurements
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.Measurements.Include(m => m.Exercise).Include(m => m.User);
            return View(await gymContext.ToListAsync());
        }

        // GET: Measurements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Measurements == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements
                .Include(m => m.Exercise)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (measurement == null)
            {
                return NotFound();
            }

            return View(measurement);
        }

        // GET: Measurements/Create
        public IActionResult Create()
        {
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID");
            return View();
        }

        // POST: Measurements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,ExerciseID,Date,Weight,BodyFatPercentage,Chest,Waist,Hips")] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(measurement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", measurement.ExerciseID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", measurement.UserID);
            return View(measurement);
        }

        // GET: Measurements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Measurements == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", measurement.ExerciseID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", measurement.UserID);
            return View(measurement);
        }

        // POST: Measurements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,ExerciseID,Date,Weight,BodyFatPercentage,Chest,Waist,Hips")] Measurement measurement)
        {
            if (id != measurement.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(measurement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasurementExists(measurement.ID))
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
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", measurement.ExerciseID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", measurement.UserID);
            return View(measurement);
        }

        // GET: Measurements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Measurements == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements
                .Include(m => m.Exercise)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (measurement == null)
            {
                return NotFound();
            }

            return View(measurement);
        }

        // POST: Measurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Measurements == null)
            {
                return Problem("Entity set 'GymContext.Measurements'  is null.");
            }
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement != null)
            {
                _context.Measurements.Remove(measurement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasurementExists(int id)
        {
          return _context.Measurements.Any(e => e.ID == id);
        }
    }
}
