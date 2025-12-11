using Scheduling.Factories;
using Scheduling.Models.Teachers;
using Scheduling.Groups;
using System;

namespace Scheduling
{
    class Program
    {
        static void Main()
        {
            var lecturer = new Lecturer("Dr. Oleh Sinkevych");
            var assistant = new Assistant("Dr. Mariia Petrenko");
            var mentor = new ExternalMentor("Industry Expert");

            ICourseFactory progFactory = new ProgrammingCourseFactory();
            ICourseFactory dbFactory = new DatabasesCourseFactory();
            
            var progLecture = progFactory.CreateLecture("Wed 15:05", "129", lecturer);
            var progPractical = progFactory.CreatePractical("Mon 13:30", "#3", assistant);
            var progCoursework = progFactory.CreateCourseWork("Advanced C# Project", mentor);

            var dbLecture = dbFactory.CreateLecture("Mon 13:30", "201", lecturer);
            var dbPractical = dbFactory.CreatePractical("Thu 10:00", "#7", assistant);
            var dbCoursework = dbFactory.CreateCourseWork("DB Normalization Lab", assistant);

            var groupA = new StudentGroup("FeP-21");
            var groupB = new StudentGroup("FeP-22");

            Console.WriteLine($"Adding {progLecture.Describe()} to {groupA.Name} => {groupA.AddSession(progLecture)}");
            Console.WriteLine($"Adding {progPractical.Describe()} to {groupA.Name} => {groupA.AddSession(progPractical)}");

            Console.WriteLine($"Adding {dbLecture.Describe()} to {groupA.Name} => {groupA.AddSession(dbLecture)}");

            var conflicts = groupA.CheckConflicts();
            Console.WriteLine($"Group {groupA.Name} has {conflicts.Count} conflict(s).");
            foreach (var (a, b) in conflicts)
                Console.WriteLine($"Conflict: {a.Describe()} <-> {b.Describe()}");

            Console.WriteLine($"Assigning coursework supervisor for {progCoursework.Title}: {progCoursework.Supervisor}");
            bool submitOk = progCoursework.Submit("https://github.com/student/repo");
            Console.WriteLine($"Programming coursework GitHub submission accepted? {submitOk}");

            bool onlineSubmitOk = dbCoursework.Submit("/home/student/db_lab.zip");
            Console.WriteLine($"DB coursework online file accepted? {onlineSubmitOk}");

        }
    }
}
