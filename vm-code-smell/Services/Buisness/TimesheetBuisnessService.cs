using System.Collections.Generic;
using vm_code_smell.Models;
using vm_code_smell.Services.Domain;

namespace vm_code_smell.Services.Buisness
{
    public class TimesheetBuisnessService : ITimesheetBuisnessService
    {
        private readonly ITimesheetDomainService _domainService;

        public TimesheetBuisnessService(ITimesheetDomainService domainService)
        {
            _domainService = domainService;
        }
        
        public IEnumerable<TimesheetEntry> GetAll()
        {
            return _domainService.GetAll();
        }

        public void AddNew()
        {
            _domainService.AddNew();
        }

        public void DeleteTimesheetEntry(int id)
        {
            _domainService.DeleteTimesheetEntry(id);
        }
    }
}