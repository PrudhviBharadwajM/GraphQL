namespace GraphQL.API.Schema.Queries
{
    public enum Subject
    {
        Math,
        Science,
        History
    }

    public class CourseType
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Subject Subject { get; set; }
        public IEnumerable<InstructorType>? Instructors { get; set; }
        public IEnumerable<StudentType>? Students { get; set; }
    }
}
