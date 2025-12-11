using System;

namespace Scheduling.Models.Teachers
{
    public abstract class Teacher
    {
        public string Name { get; }
        protected Teacher(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));
        public override string ToString() => $"{GetType().Name} {Name}";
    }

    public interface ICanGiveLecture { }
    public interface ICanLeadPractical { }
    public interface ICanSuperviseCourseWork { }

    public class Lecturer : Teacher, ICanGiveLecture, ICanSuperviseCourseWork
    {
        public Lecturer(string name) : base(name) { }
    }

    public class Assistant : Teacher, ICanLeadPractical, ICanSuperviseCourseWork
    {
        public Assistant(string name) : base(name) { }
    }

    public class ExternalMentor : Teacher, ICanSuperviseCourseWork
    {
        public ExternalMentor(string name) : base(name) { }
    }
}
