using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaspBier.Database;
using RaspBier.Helper;
using RaspBier.Models;

namespace RaspBier.Controllers
{
    public class ErrorController : Controller
    {
        private readonly CustomDBContext _context;

        public ErrorController(CustomDBContext context)
        {
            _context = context;
        }

        #region Default
        // GET: Error
        public async Task<IActionResult> Index()
        {
            return View(await _context.Errors.ToListAsync());
        }

        // GET: Error/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var error = await _context.Errors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (error == null)
            {
                return NotFound();
            }

            return View(error);
        }

        // GET: Error/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Error/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ErrorType,Message")] Error error)
        {
            if (ModelState.IsValid)
            {
                _context.Add(error);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(error);
        }

        // GET: Error/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var error = await _context.Errors.FindAsync(id);
            if (error == null)
            {
                return NotFound();
            }
            return View(error);
        }

        // POST: Error/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ErrorType,Message")] Error error)
        {
            if (id != error.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(error);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ErrorExists(error.ID))
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
            return View(error);
        }

        // GET: Error/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var error = await _context.Errors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (error == null)
            {
                return NotFound();
            }

            return View(error);
        }

        // POST: Error/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var error = await _context.Errors.FindAsync(id);
            _context.Errors.Remove(error);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ErrorExists(int id)
        {
            return _context.Errors.Any(e => e.ID == id);
        }

        #endregion

        [HttpPost]
        public async Task<bool> InsertError(int sensorId, int errorType, string message, string hash)
        {
            if (!hash.Equals(SecurityHelper.SecurityHash))
                return false;

            var err = new Error();
            err.SensorID = sensorId;
            err.ErrorType = (ErrorType)errorType;
            err.Message = message;
            err.TimeStamp = DateTime.Now;

            _context.Errors.Add(err);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
