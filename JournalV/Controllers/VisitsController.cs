using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using JournalV.Data;
using JournalV.Models;

namespace JournalV.Controllers
{
    
    public class VisitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Все пользователи могут просматривать список посещений
        // GET: Visits
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var visits = from v in _context.Visits
                 .Include(v => v.Pet)
                 .Include(v => v.Veterinarian)
                 .Include(v => v.Procedure)
                         select v;

            // Поиск
            if (!string.IsNullOrEmpty(searchString))
            {
                visits = visits.Where(v => v.Pet.Name.Contains(searchString) || v.Veterinarian.FullName.Contains(searchString));
            }

            // Сортировка
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            visits = sortOrder switch
            {
                "Date" => visits.OrderBy(v => v.VisitDate),
                "date_desc" => visits.OrderByDescending(v => v.VisitDate),
                _ => visits.OrderBy(v => v.VisitDate),
            };

            return View(await visits.ToListAsync()); ;
        }

        // GET: Visits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Pet)
                .Include(v => v.Procedure)
                .Include(v => v.Veterinarian)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: Visits/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Id");
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Id");
            ViewData["VeterinarianId"] = new SelectList(_context.Veterinarians, "Id", "Id");
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,VisitDate,Notes,PetId,VeterinarianId,ProcedureId")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Id", visit.PetId);
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Id", visit.ProcedureId);
            ViewData["VeterinarianId"] = new SelectList(_context.Veterinarians, "Id", "Id", visit.VeterinarianId);
            return View(visit);
        }

        // GET: Visits/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Id", visit.PetId);
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Id", visit.ProcedureId);
            ViewData["VeterinarianId"] = new SelectList(_context.Veterinarians, "Id", "Id", visit.VeterinarianId);
            return View(visit);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VisitDate,Notes,PetId,VeterinarianId,ProcedureId")] Visit visit)
        {
            if (id != visit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.Id))
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
            ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Id", visit.PetId);
            ViewData["ProcedureId"] = new SelectList(_context.Procedures, "Id", "Id", visit.ProcedureId);
            ViewData["VeterinarianId"] = new SelectList(_context.Veterinarians, "Id", "Id", visit.VeterinarianId);
            return View(visit);
        }

        // GET: Visits/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Pet)
                .Include(v => v.Procedure)
                .Include(v => v.Veterinarian)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visit = await _context.Visits.FindAsync(id);
            if (visit != null)
            {
                _context.Visits.Remove(visit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(e => e.Id == id);
        }
    }
}
