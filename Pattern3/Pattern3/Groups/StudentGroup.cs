using Scheduling.Models.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Groups
{
    public class StudentGroup
    {
        public string Name { get; }
        private readonly List<ClassSession> _schedule = new List<ClassSession>();

        public IReadOnlyList<ClassSession> Schedule { get { return _schedule.AsReadOnly(); } }

        public StudentGroup(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public bool AddSession(ClassSession session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            var tentative = _schedule.Concat(new[] { session });
            var conflicts = ScheduleConflictDetector.DetectConflicts(tentative);
            if (conflicts.Count > 0) return false;
            _schedule.Add(session);
            return true;
        }

        public List<(ClassSession A, ClassSession B)> CheckConflicts()
        {
            return ScheduleConflictDetector.DetectConflicts(_schedule);
        }
    }
}
