using System;
using System.Collections.Generic;
using System.Linq;

namespace vm_code_smell.Repositories
{
    public class TimesheetRepository : Repository, ITimesheetRepository
    {
        public IEnumerable<EntryDbo> GetEntriesForRange(DateTime start, DateTime end)
        {
            return Entries.Where(e => e.Date >= start && e.Date <= end);
        }

        public TimesheetDbo GetTimesheetForPeriod(DateTime periodStart)
        {
            return Timesheets.Single(t => t.Period == periodStart.Month);
        }

        public void AddNewTimesheetEntry()
        {
            var nextId = 1;
            if (Entries.Any())
            {
                nextId = Entries.Max(e => e.EntryId) + nextId;
            }
            
            var entry = new EntryDbo
            {
                Date = DateTime.Now,
                EntryId = nextId
            };
            
            Entries.Add(entry);
        }

        public void DeleteEntry(int id)
        {
            var toDelete = Entries.SingleOrDefault(e => e.EntryId == id);
            if (toDelete == null)
            {
                return;
            }

            Entries.Remove(toDelete);
        }
    }
}