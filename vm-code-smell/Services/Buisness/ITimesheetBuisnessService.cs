using System.Collections.Generic;
using vm_code_smell.Models;

namespace vm_code_smell.Services.Buisness
{
    public interface ITimesheetBuisnessService
    {
        IEnumerable<TimesheetEntry> GetAll();
        void AddNew();
        void DeleteTimesheetEntry(int id);
    }
}