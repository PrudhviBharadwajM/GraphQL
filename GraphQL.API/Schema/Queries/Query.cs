using Bogus;

namespace GraphQL.API.Schema.Queries;

public class Query
{

    private readonly Faker<InstructorType> _instructorFaker;
    private readonly Faker<StudentType> _studentFaker;
    private readonly Faker<CourseType> _courseFaker;

    public Query()
    {
        _instructorFaker = new Faker<InstructorType>()
            .RuleFor(i => i.Id, f => f.Random.Guid())
            .RuleFor(i => i.FirstName, f => f.Name.FirstName())
            .RuleFor(i => i.LastName, f => f.Name.LastName())
            .RuleFor(i => i.Salary, f => f.Random.Double(0, 10000));

        _studentFaker = new Faker<StudentType>()
            .RuleFor<Guid>(s => s.Id, f => f.Random.Guid())
            .RuleFor(s => s.FirstName, f => f.Name.FirstName())
            .RuleFor(s => s.LastName, f => f.Name.LastName())
            .RuleFor(s => s.GPA, f => f.Random.Double(0, 10000));

        _courseFaker = new Faker<CourseType>()
            .RuleFor<Guid>(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Name, f => f.Name.JobTitle())
            .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
            .RuleFor(c => c.Instructors, f => _instructorFaker.Generate(2))
            .RuleFor(c => c.Students, f => _studentFaker.Generate(2));


    }
    public IEnumerable<CourseType> GetCourses()
    {
        return _courseFaker.Generate(4);
    }

    public async Task<CourseType> GetCourseByIdAsync(Guid id)
    {
        await Task.Delay(1000); // Simulate async work
        var course = _courseFaker.Generate();
        course.Id = id;
        
        return course;
    }

    [Obsolete("Use the classes instead.")]
    public string Hello => "Hello, World!";
}
