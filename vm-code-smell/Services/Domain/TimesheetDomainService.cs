using System;
using System.Collections.Generic;
using System.Linq;
using vm_code_smell.Models;
using vm_code_smell.Repositories;

namespace vm_code_smell.Services.Domain
{
    public class TimesheetDomainService : ITimesheetDomainService
    {
        private readonly ITimesheetRepository _repository;

        public TimesheetDomainService(ITimesheetRepository repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<TimesheetEntry> GetAll()
        {
            var entries = _repository.GetEntriesForRange(DateTime.MinValue, DateTime.MaxValue);

            return entries.Select(e => new TimesheetEntry
            {
                DateFormatted = e.Date.ToString("d"),
                EntryId = e.EntryId,
                ProjectId = e.ProjectId,
                TimeSpent = e.TimeSpent
            });
        }

        public void AddNew()
        {
            _repository.AddNewTimesheetEntry();
        }

        public void DeleteTimesheetEntry(int id)
        {
            _repository.DeleteEntry(id);
        }
    }
}