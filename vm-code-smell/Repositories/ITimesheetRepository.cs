using System;
using System.Collections.Generic;

namespace vm_code_smell.Repositories
{
    public interface ITimesheetRepository
    {
        IEnumerable<EntryDbo> GetEntriesForRange(DateTime start, DateTime end);
        TimesheetDbo GetTimesheetForPeriod(DateTime periodStart);
        void AddNewTimesheetEntry();
        void DeleteEntry(int id);
    }
}