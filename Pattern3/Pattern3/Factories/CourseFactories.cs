using Scheduling.Models.CourseWorks;
using Scheduling.Models.Sessions;
using Scheduling.Models.Teachers;

namespace Scheduling.Factories
{
    public interface ICourseFactory
    {
        LectureSession CreateLecture(string time, string room, ICanGiveLecture lecturer);
        PracticalSession CreatePractical(string time, string room, ICanLeadPractical assistant);
        CourseWork CreateCourseWork(string title, ICanSuperviseCourseWork supervisor);
    }

    public class ProgrammingCourseFactory : ICourseFactory
    {
        public LectureSession CreateLecture(string time, string room, ICanGiveLecture lecturer)
            => new LectureSession(time, room, lecturer);

        public PracticalSession CreatePractical(string time, string room, ICanLeadPractical assistant)
            => new PracticalSession(time, room, assistant);

        public CourseWork CreateCourseWork(string title, ICanSuperviseCourseWork supervisor)
        {
            var cw = new GitHubCourseWork(title + " (Programming Project)");
            cw.AssignSupervisor(supervisor);
            return cw;
        }
    }

    public class DatabasesCourseFactory : ICourseFactory
    {
        public LectureSession CreateLecture(string time, string room, ICanGiveLecture lecturer)
            => new LectureSession(time, room, lecturer);

        public PracticalSession CreatePractical(string time, string room, ICanLeadPractical assistant)
            => new PracticalSession(time, room, assistant);

        public CourseWork CreateCourseWork(string title, ICanSuperviseCourseWork supervisor)
        {
            var cw = new OnlineCourseWork(title + " (DB Lab)");
            cw.AssignSupervisor(supervisor);
            return cw;
        }
    }

    public class MathCourseFactory : ICourseFactory
    {
        public LectureSession CreateLecture(string time, string room, ICanGiveLecture lecturer)
            => new LectureSession(time, room, lecturer);

        public PracticalSession CreatePractical(string time, string room, ICanLeadPractical assistant)
            => new PracticalSession(time, room, assistant);

        public CourseWork CreateCourseWork(string title, ICanSuperviseCourseWork supervisor)
        {
            var cw = new OralDefenseCourseWork(title + " (Math Report)");
            cw.AssignSupervisor(supervisor);
            return cw;
        }
    }
}
