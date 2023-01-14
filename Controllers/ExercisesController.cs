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
    public class ExercisesController : Controller
    {
        private readonly GymContext _context;

        public ExercisesController(GymContext context)
        {
            _context = context;
        }

        // GET: Exercises/ INDEX
        /*
        public async Task<IActionResult> Index()
        {
              return View(await _context.Exercises.ToListAsync());
        }
        */



        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            IQueryable<Exercise> exercise = _context.Exercises.AsNoTracking();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ExerciseNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "exercise_name_desc" : "exercise_name";
            ViewData["MuscleGroupSortParm"] = sortOrder == "muscle_group" ? "muscle_group_desc" : "muscle_group";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                exercise = exercise.Where(s => s.MuscleGroup.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "exercise_name_desc":
                    exercise = exercise.OrderByDescending(x => x.ExerciseName);
                    break;
                case "exercise_name":
                    exercise = exercise.OrderBy(x => x.ExerciseName);
                    break;
                case "muscle_group_desc":
                    exercise = exercise.OrderByDescending(x => x.MuscleGroup);
                    break;
                case "muscle_group":
                    exercise = exercise.OrderBy(x => x.MuscleGroup);
                    break;
                default:
                    exercise = exercise.OrderBy(x => x.ExerciseName);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Exercise>.CreateAsync(exercise, pageNumber ?? 1, pageSize));
        }




        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exercises == null)
            {
                return NotFound();
            }
            /*
            var exercise = await _context.Exercises
                 .Include(s => s.Measurements)
                 .ThenInclude(e => e.User)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.ID == id);
            */
            var exercise = await _context.Exercises
                 .Include(s => s.Trainer)
                 .FirstOrDefaultAsync(m => m.ID == id);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            var trainers = _context.Trainer.Select(x => new
            {
                x.Id,
                FullName = x.FirstName + " " + x.LastName
            });
            ViewData["TrainerId"] = new SelectList(trainers, "Id", "FullName");
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseName,TrainerId,Description,MuscleGroup,DifficultyLevel")] Exercise exercise)
        {
            try
            {

                if (ModelState.IsValid)
                {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateException)
            {

                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }
            ViewData["TrainerId"] = new SelectList(_context.Set<Trainer>(), "Id", "Id", exercise.TrainerId);

            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exercises == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var exerciseToUpdate = await _context.Exercises.FirstOrDefaultAsync(s => s.ID == id);
            
            if (await TryUpdateModelAsync<Exercise>(exerciseToUpdate,"",s=>s.ExerciseName,s=>s.Trainer,s=>s.Description,s=>s.MuscleGroup,s=>s.DifficultyLevel))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");

                }
                
            }
            return View(exerciseToUpdate);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Exercises == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (exercise == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            catch (DbUpdateException )
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

        }




        private bool ExerciseExists(int id)
        {
          return _context.Exercises.Any(e => e.ID == id);
        }
    }
}
