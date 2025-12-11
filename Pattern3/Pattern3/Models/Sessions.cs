using Scheduling.Models.Teachers;
using System;

namespace Scheduling.Models.Sessions
{
    public abstract class ClassSession
    {
        public string Time { get; }
        public string Room { get; }
        public Teacher Teacher { get; }

        protected ClassSession(string time, string room, Teacher teacher)
        {
            Time = time ?? throw new ArgumentNullException(nameof(time));
            Room = room ?? throw new ArgumentNullException(nameof(room));
            Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        }

        public virtual string Describe() => $"{GetType().Name} at {Time} in {Room} by {Teacher}";
    }

    public class LectureSession : ClassSession
    {
        public LectureSession(string time, string room, ICanGiveLecture teacher)
            : base(time, room, teacher as Teacher) { }
    }

    public class PracticalSession : ClassSession
    {
        public PracticalSession(string time, string room, ICanLeadPractical teacher)
            : base(time, room, teacher as Teacher) { }
    }
}
