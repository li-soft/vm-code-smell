namespace vm_code_smell.Models
{
    public class TimesheetEntry
    {
        public int EntryId { get; set; }
        public string DateFormatted { get; set; }
        public int TimeSpent { get; set; }
        public int ProjectId { get; set; }
    }
}