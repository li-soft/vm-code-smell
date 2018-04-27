using System;
using System.Collections.Generic;
using System.Linq;

namespace vm_code_smell.Repositories
{
    public class Repository
    {
        private static readonly Lazy<IEnumerable<TimesheetDbo>> LazyTimesheet = new Lazy<IEnumerable<TimesheetDbo>>(GetTimesheets);
        protected static List<EntryDbo> Entries => LazyTimesheet.Value.First().Entries;
        protected static IEnumerable<TimesheetDbo> Timesheets => LazyTimesheet.Value;

        private static IEnumerable<TimesheetDbo> GetTimesheets()
        {
            var rng = new Random();
            var entries = Enumerable.Range(1, 5).Select(id => new EntryDbo
            {
                Date = DateTime.Now.AddDays(id),
                EntryId = id,
                TimeSpent = 10 - rng.Next(1, 4),
                ProjectId = rng.Next(1, 4)
            }).ToList();
            
            yield return new TimesheetDbo
            {
                Entries = entries,
                Id = 1,
                Period = DateTime.Now.Month
            };
        }
    }
}