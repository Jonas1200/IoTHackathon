﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class SensorValueController : Controller
    {
        private readonly CustomDBContext _context;

        public SensorValueController(CustomDBContext context)
        {
            _context = context;
        }

        #region Default
        // GET: SensorValue
        public async Task<IActionResult> Index(int sensorId = -1)
        {
            List<SensorValue> values; 
            if (sensorId >= 0)
                values = await _context.SensorValues.Where(value => value.SensorID == sensorId).ToListAsync();
            else
                values = await _context.SensorValues.ToListAsync();

            values.Reverse();
            return View(values);
        }

        // GET: SensorValue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensorValue = await _context.SensorValues
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sensorValue == null)
            {
                return NotFound();
            }

            return View(sensorValue);
        }

        // GET: SensorValue/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SensorValue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SensorType,SensorID,Value,TimeStamp")] SensorValue sensorValue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sensorValue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sensorValue);
        }

        // GET: SensorValue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensorValue = await _context.SensorValues.FindAsync(id);
            if (sensorValue == null)
            {
                return NotFound();
            }
            return View(sensorValue);
        }

        // POST: SensorValue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SensorType,SensorID,Value,TimeStamp")] SensorValue sensorValue)
        {
            if (id != sensorValue.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensorValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensorValueExists(sensorValue.ID))
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
            return View(sensorValue);
        }

        // GET: SensorValue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensorValue = await _context.SensorValues
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sensorValue == null)
            {
                return NotFound();
            }

            return View(sensorValue);
        }

        // POST: SensorValue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sensorValue = await _context.SensorValues.FindAsync(id);
            _context.SensorValues.Remove(sensorValue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SensorValueExists(int id)
        {
            return _context.SensorValues.Any(e => e.ID == id);
        }

    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        [HttpPost]
        public async Task<bool> InsertValue(int sensorType, int sensorId, int value, string hash)
        {
            if (!hash.Equals(SecurityHelper.SecurityHash))
                return false;

            var sv = new SensorValue();
            sv.SensorID = sensorId;
            sv.SensorType = (SensorType)sensorType;
            sv.Value = value;
            sv.TimeStamp = DateTime.Now;

            _context.SensorValues.Add(sv);
            await _context.SaveChangesAsync();

            return true;
        }

        // POST: SensorValue/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAllValues(int sensorId)
        {
            var sensorValues = await _context.SensorValues.Where(value => value.SensorID == sensorId).ToArrayAsync();
            _context.SensorValues.RemoveRange(sensorValues);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
