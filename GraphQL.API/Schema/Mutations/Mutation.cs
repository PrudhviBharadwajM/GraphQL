using GraphQL.API.Schema.Queries;
using GraphQL.API.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQL.API.Schema.Mutations;

public class Mutation
{
    private readonly List<CourseResult> _courses;

    public Mutation()
    {
        _courses = new List<CourseResult>();
    }

    public async Task<CourseResult> CreateCourse(CourseInputType inputType, [Service] ITopicEventSender topicEventSender)
    {
        var course = new CourseResult
        {
            Id = Guid.NewGuid(),
            Name = inputType.Name,
            Subject = inputType.Subject,
            InstructorId = inputType.InstructorId
        };
        _courses.Add(course);

        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);
        return course;
    }

    public async Task<CourseResult> Update(Guid id, CourseInputType inputType, [Service] ITopicEventSender topicEventSender)
    {
        var course = _courses.FirstOrDefault(c => c.Id == id);
        if (course == null)
        {
            throw new GraphQLException(new Error("Course not found", "COURSE_NOT FOUND"));
        }
        course.Name = inputType.Name;
        course.Subject = inputType.Subject;
        course.InstructorId = inputType.InstructorId;

        string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
        await topicEventSender.SendAsync(updateCourseTopic, course);

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
