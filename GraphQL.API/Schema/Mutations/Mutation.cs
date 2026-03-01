using GraphQL.API.Schema.Queries;

namespace GraphQL.API.Schema.Mutations;

public class Mutation
{
    private readonly List<CourseResult> _courses;

    public Mutation()
    {
        _courses = new List<CourseResult>();
    }

    public CourseResult CreateCourse(CourseInputType inputType)
    {
        var course = new CourseResult
        {
            Id = Guid.NewGuid(),
            Name = inputType.Name,
            Subject = inputType.Subject,
            InstructorId = inputType.InstructorId
        };
        _courses.Add(course);
        return course;
    }

    public CourseResult Update(Guid id, CourseInputType inputType)
    {
        var course = _courses.FirstOrDefault(c => c.Id == id);
        if (course == null)
        {
            throw new GraphQLException(new Error("Course not found", "COURSE_NOT FOUND"));
        }
        course.Name = inputType.Name;
        course.Subject = inputType.Subject;
        course.InstructorId = inputType.InstructorId;
        return course;
    }

    public bool Delete(Guid id)
    {
        var course = _courses.FirstOrDefault(c => c.Id == id);
        if (course == null)
        {
            throw new GraphQLException(new Error("Course not found", "COURSE_NOT FOUND"));
        }
        return _courses.Remove(course);
    }

}
