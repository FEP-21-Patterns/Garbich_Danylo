using Scheduling.Models.Sessions;
using Scheduling.Models.Teachers;
using System;

namespace Scheduling.Factories
{
    public interface ISessionFactory<TSession> where TSession : ClassSession
    {
        TSession CreateSession(string time, string room, object teacher);
    }

    public class LectureFactory : ISessionFactory<LectureSession>
    {
        public LectureSession CreateSession(string time, string room, object teacher)
        {
            if (teacher is ICanGiveLecture lectureTeacher)
                return new LectureSession(time, room, lectureTeacher);
            throw new ArgumentException("teacher must be able to give lectures (ICanGiveLecture)", nameof(teacher));
        }
    }

    public class PracticalFactory : ISessionFactory<PracticalSession>
    {
        public PracticalSession CreateSession(string time, string room, object teacher)
        {
            if (teacher is ICanLeadPractical practicalTeacher)
                return new PracticalSession(time, room, practicalTeacher);
            throw new ArgumentException("teacher must be able to lead practicals (ICanLeadPractical)", nameof(teacher));
        }
    }
}
