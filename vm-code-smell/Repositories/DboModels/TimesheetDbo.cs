using System;
using System.Collections.Generic;

namespace vm_code_smell.Repositories
{
    public class TimesheetDbo
    {
        public int Id { get; set; }
        public int Period { get; set; }
        public List<EntryDbo> Entries { get; set; }
    }
}