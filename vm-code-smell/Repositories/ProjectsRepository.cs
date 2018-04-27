namespace vm_code_smell.Repositories
{
    public class ProjectsRepository
    {
        
    }

    public class ProjectDbo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int OwnerId { get; set; }
    }
}