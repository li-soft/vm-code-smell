using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace vm_code_smell.Controllers
{
    [Route("api/[controller]")]
    public class TimesheetController : Controller
    {
        private static Lazy<List<TimesheetEntry>> _entries;

        public TimesheetController()
        {
            InitializeData();
        }

        [HttpGet("[action]")]
        public IEnumerable<TimesheetEntry> GetAll()
        {
            return _entries.Value;
        }

        [HttpGet("[action]")]
        public IActionResult AddNew()
        {
            var nextId = _entries.Value.Max(e => e.EntryId) + 1;
            var newEntry = new TimesheetEntry{ EntryId = nextId, DateFormatted = DateTime.Now.ToString("d")};
            _entries.Value.Add(newEntry);

            return Ok();
        }
        
        [HttpGet("[action]")]
        public IActionResult DeleteTimesheetEntry(int id)
        {
            var toDelete = _entries.Value.SingleOrDefault(e => e.EntryId == id);
            if (toDelete == null)
            {
                return BadRequest();
            }
            
            _entries.Value.Remove(toDelete);
            return Ok();

        }
        
        [HttpPatch("[action]")]
        public IActionResult UpdateTimesheetEntry(TimesheetEntry entry)
        {
            var toDelete = _entries.Value.SingleOrDefault(e => e.EntryId == entry.EntryId);
            if (toDelete == null)
            {
                return NotFound();
            }
            
            _entries.Value.Remove(toDelete);
            _entries.Value.Add(entry);

            return Ok(entry);
        }
        
        private static void InitializeData()
        {
            if (_entries != null)
            {
                return;
            }
            
            var rng = new Random();
            var entries = Enumerable.Range(1, 5).Select(id => new TimesheetEntry
            {
                DateFormatted = DateTime.Now.AddDays(id).ToString("d"),
                EntryId = id,
                TimeSpent = 10 - rng.Next(1, 4),
                ProjectId = rng.Next(1, 4)
            }).ToList();
            
            _entries = new Lazy<List<TimesheetEntry>>(() => entries);
        }
    }

    public class TimesheetEntry
    {
        public int EntryId { get; set; }
        public string DateFormatted { get; set; }
        public int TimeSpent { get; set; }
        public int ProjectId { get; set; }
    }
}