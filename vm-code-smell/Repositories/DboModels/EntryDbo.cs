using System;

namespace vm_code_smell.Repositories
{
    public class EntryDbo
    {
        public int EntryId { get; set; }
        public DateTime Date { get; set; }
        public int TimeSpent { get; set; }
        public int ProjectId { get; set; }
    }
}