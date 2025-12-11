using Scheduling.Models.Teachers;
using System;

namespace Scheduling.Models.CourseWorks
{
    public abstract class CourseWork
    {
        public string Title { get; }
        public ICanSuperviseCourseWork Supervisor { get; private set; }

        protected CourseWork(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public void AssignSupervisor(ICanSuperviseCourseWork sup) => Supervisor = sup ?? throw new ArgumentNullException(nameof(sup));

        public abstract bool Submit(object submissionData);
    }

    public class OnlineCourseWork : CourseWork
    {
        public bool IsSubmitted { get; private set; }
        public string FilePath { get; private set; }

        public OnlineCourseWork(string title) : base(title) { }

        public override bool Submit(object submissionData)
        {
            if (submissionData is string path && !string.IsNullOrWhiteSpace(path))
            {
                FilePath = path;
                IsSubmitted = true;
                return true;
            }

            return false;
        }
    }

    public class GitHubCourseWork : CourseWork
    {
        public string RepoUrl { get; private set; }

        public GitHubCourseWork(string title) : base(title) { }

        public override bool Submit(object submissionData)
        {
            if (submissionData is string url && Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                RepoUrl = url;
                return true;
            }
            return false;
        }
    }

    public class OralDefenseCourseWork : CourseWork
    {
        public DateTime? DefenseDate { get; private set; }

        public OralDefenseCourseWork(string title) : base(title) { }

        public override bool Submit(object submissionData)
        {
            if (submissionData is DateTime dt)
            {
                DefenseDate = dt;
                return true;
            }
            return false;
        }
    }
}
