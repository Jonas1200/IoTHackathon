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
    public class NotificationEntryController : Controller
    {
        private readonly CustomDBContext _context;

        #region Default
        public NotificationEntryController(CustomDBContext context)
        {
            _context = context;
        }

        // GET: NotificationEntry
        public async Task<IActionResult> Index()
        {
            return View(await _context.NotificationEntry.ToListAsync());
        }

        // GET: NotificationEntry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationEntry = await _context.NotificationEntry
                .FirstOrDefaultAsync(m => m.ID == id);
            if (notificationEntry == null)
            {
                return NotFound();
            }

            return View(notificationEntry);
        }

        // GET: NotificationEntry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotificationEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SensorID,NotificationType,Message,TimeStamp")] NotificationEntry notificationEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificationEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notificationEntry);
        }

        // GET: NotificationEntry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationEntry = await _context.NotificationEntry.FindAsync(id);
            if (notificationEntry == null)
            {
                return NotFound();
            }
            return View(notificationEntry);
        }

        // POST: NotificationEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SensorID,NotificationType,Message,TimeStamp")] NotificationEntry notificationEntry)
        {
            if (id != notificationEntry.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificationEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationEntryExists(notificationEntry.ID))
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
            return View(notificationEntry);
        }

        // GET: NotificationEntry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationEntry = await _context.NotificationEntry
                .FirstOrDefaultAsync(m => m.ID == id);
            if (notificationEntry == null)
            {
                return NotFound();
            }

            return View(notificationEntry);
        }

        // POST: NotificationEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notificationEntry = await _context.NotificationEntry.FindAsync(id);
            _context.NotificationEntry.Remove(notificationEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationEntryExists(int id)
        {
            return _context.NotificationEntry.Any(e => e.ID == id);
        }
        #endregion

        [HttpPost]
        public async Task<bool> InsertNotification(int sensorId, int notificationType, string message, string hash)
        {
            if (!hash.Equals(SecurityHelper.SecurityHash))
                return false;

            var not = new NotificationEntry();
            not.SensorID = sensorId;
            not.NotificationType = (NotificationType)notificationType;
            not.Message = message;
            not.TimeStamp = DateTime.Now;

            _context.NotificationEntry.Add(not);
            await _context.SaveChangesAsync();

            var body = String.Format("Message: {0}", not.Message);
            var subject = String.Format("Notification: {0} Sensor: {1}",not.NotificationType, not.SensorID);

            MailHelper.SendMail(subject, body);
            return true;
        }
    }
}
