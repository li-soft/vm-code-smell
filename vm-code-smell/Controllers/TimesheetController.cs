using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vm_code_smell.Models;
using vm_code_smell.Services.Buisness;

namespace vm_code_smell.Controllers
{
    [Route("api/[controller]")]
    public class TimesheetController : Controller
    {
        private readonly ITimesheetBuisnessService _buisnessService;

        public TimesheetController(ITimesheetBuisnessService buisnessService)
        {
            _buisnessService = buisnessService;
        }

        // DateTime.Now.AddDays(id).ToString("d")
        [HttpGet("[action]")]
        public IEnumerable<TimesheetEntry> GetAll()
        {
            return _buisnessService.GetAll();
        }

        [HttpPost("[action]")]
        public IActionResult AddNew()
        {
            _buisnessService.AddNew();
            return Ok();
        }
        
        [HttpDelete("[action]")]
        public IActionResult DeleteTimesheetEntry(int id)
        {
            _buisnessService.DeleteTimesheetEntry(id);
            return Ok();

        }
    }
}