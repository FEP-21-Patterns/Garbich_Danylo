using Scheduling.Models.Sessions;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Groups
{
    public static class ScheduleConflictDetector
    {
        public static List<(ClassSession A, ClassSession B)> DetectConflicts(IEnumerable<ClassSession> sessions)
        {
            var list = sessions.ToList();
            var result = new List<(ClassSession, ClassSession)>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (string.Equals(list[i].Time, list[j].Time))
                        result.Add((list[i], list[j]));
                }
            }
            return result;
        }
    }
}
