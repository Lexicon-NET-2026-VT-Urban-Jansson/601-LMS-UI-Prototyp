namespace TestClaudeLMS01.Client.Services
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StudentCount { get; set; }
        public List<Module> Modules { get; set; } = new();
    }

    public class Module
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CompletionPercent { get; set; }
        public List<Activity> Activities { get; set; } = new();
    }

    public class Activity
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Föreläsning", "Övning", "E-learning", "Inlämning", etc.
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
    }

    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; } // "Inlämnad", "Ej inlämnad", "Försenad", "Returnerad"
        public DateTime? SubmittedDate { get; set; }
        public int? Grade { get; set; }
        public string Feedback { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CourseProgress { get; set; } // 0-100 %
        public DateTime? LastActivity { get; set; }
    }

    public class MockDataService
    {
        public List<Course> GetCourses()
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Frontend-utvecklare",
                    Code = "FED101",
                    Description = "En omfattande utbildning i modern webbutveckling med fokus på JavaScript och React.",
                    StartDate = new DateTime(2023, 08, 15),
                    EndDate = new DateTime(2023, 12, 15),
                    StudentCount = 15,
                    Modules = GetModulesForCourse(1)
                },
                new Course
                {
                    Id = 2,
                    Name = "Backend-utvecklare",
                    Code = "BED101",
                    Description = "Lär dig bygga robust backend-arkitektur med .NET och SQL.",
                    StartDate = new DateTime(2023, 09, 01),
                    EndDate = new DateTime(2024, 01, 31),
                    StudentCount = 12,
                    Modules = GetModulesForCourse(2)
                }
            };
            return courses;
        }

        public Course GetCourse(int courseId)
        {
            return GetCourses().FirstOrDefault(c => c.Id == courseId);
        }

        private List<Module> GetModulesForCourse(int courseId)
        {
            if (courseId == 1)
            {
                return new List<Module>
                {
                    new Module
                    {
                        Id = 1,
                        CourseId = 1,
                        Name = "HTML & CSS",
                        Description = "Grundläggande webbutveckling med HTML5 och CSS3",
                        StartDate = new DateTime(2023, 08, 15),
                        EndDate = new DateTime(2023, 09, 01),
                        CompletionPercent = 100,
                        Activities = new List<Activity>
                        {
                            new Activity { Id = 1, ModuleId = 1, Name = "Introduction till HTML", Type = "Föreläsning", Date = new DateTime(2023, 08, 15), Duration = TimeSpan.FromHours(2), Description = "Introduktion till HTML5" },
                            new Activity { Id = 2, ModuleId = 1, Name = "CSS Grunderna", Type = "Övning", Date = new DateTime(2023, 08, 16), Duration = TimeSpan.FromHours(3), Description = "CSS styling och layout" }
                        }
                    },
                    new Module
                    {
                        Id = 2,
                        CourseId = 1,
                        Name = "JavaScript",
                        Description = "JavaScript ES6+ och modern webbutveckling",
                        StartDate = new DateTime(2023, 09, 02),
                        EndDate = new DateTime(2023, 09, 20),
                        CompletionPercent = 60,
                        Activities = new List<Activity>
                        {
                            new Activity { Id = 3, ModuleId = 2, Name = "JS Grunderna", Type = "E-learning", Date = new DateTime(2023, 09, 02), Duration = TimeSpan.FromHours(4), Description = "Variabler, typer, funktioner" },
                            new Activity { Id = 4, ModuleId = 2, Name = "DOM-manipulation", Type = "Övning", Date = new DateTime(2023, 09, 05), Duration = TimeSpan.FromHours(2), Description = "Arbeta med DOM" }
                        }
                    },
                    new Module
                    {
                        Id = 3,
                        CourseId = 1,
                        Name = "React",
                        Description = "React-komponenter, hooks och state management",
                        StartDate = new DateTime(2023, 09, 21),
                        EndDate = new DateTime(2023, 12, 15),
                        CompletionPercent = 35,
                        Activities = new List<Activity>
                        {
                            new Activity { Id = 5, ModuleId = 3, Name = "React Components", Type = "Föreläsning", Date = new DateTime(2023, 09, 21), Duration = TimeSpan.FromHours(2), Description = "Komponenter och JSX" },
                            new Activity { Id = 6, ModuleId = 3, Name = "React Hooks Workshop", Type = "Övning", Date = new DateTime(2023, 09, 25), Duration = TimeSpan.FromHours(3), Description = "useState, useEffect" }
                        }
                    }
                };
            }

            return new List<Module>
            {
                new Module
                {
                    Id = 4,
                    CourseId = 2,
                    Name = ".NET Grunderna",
                    Description = "C# och .NET Framework",
                    StartDate = new DateTime(2023, 09, 01),
                    EndDate = new DateTime(2023, 09, 20),
                    CompletionPercent = 80,
                    Activities = new List<Activity>()
                }
            };
        }

        public List<Assignment> GetAssignmentsForStudent(int courseId)
        {
            return new List<Assignment>
            {
                new Assignment
                {
                    Id = 1,
                    CourseId = courseId,
                    Name = "HTML & CSS Projekt",
                    Deadline = new DateTime(2023, 09, 01),
                    Status = "Inlämnad",
                    SubmittedDate = new DateTime(2023, 08, 31),
                    Grade = 85,
                    Feedback = "Bra arbete! Layout var responsiv och clean."
                },
                new Assignment
                {
                    Id = 2,
                    CourseId = courseId,
                    Name = "JavaScript Quiz",
                    Deadline = new DateTime(2023, 09, 15),
                    Status = "Inlämnad",
                    SubmittedDate = new DateTime(2023, 09, 14),
                    Grade = 92,
                    Feedback = null
                },
                new Assignment
                {
                    Id = 3,
                    CourseId = courseId,
                    Name = "React Todo App",
                    Deadline = new DateTime(2023, 10, 15),
                    Status = "Ej inlämnad",
                    SubmittedDate = null,
                    Grade = null,
                    Feedback = null
                }
            };
        }

        public List<Student> GetStudentsForCourse(int courseId)
        {
            return new List<Student>
            {
                new Student { Id = 1, Name = "Anna Andersson", Email = "anna@example.com", CourseProgress = 100, LastActivity = DateTime.Now.AddDays(-1) },
                new Student { Id = 2, Name = "Erik Eriksson", Email = "erik@example.com", CourseProgress = 85, LastActivity = DateTime.Now.AddDays(-2) },
                new Student { Id = 3, Name = "Maria Svensson", Email = "maria@example.com", CourseProgress = 78, LastActivity = DateTime.Now.AddHours(-5) },
                new Student { Id = 4, Name = "Johan Nilsson", Email = "johan@example.com", CourseProgress = 65, LastActivity = DateTime.Now.AddDays(-4) },
                new Student { Id = 5, Name = "Sara Lindberg", Email = "sara@example.com", CourseProgress = 92, LastActivity = DateTime.Now.AddHours(-3) }
            };
        }

        public List<Activity> GetRecentActivities()
        {
            return new List<Activity>
            {
                new Activity { Id = 1, ModuleId = 1, Name = "Anna Andersson lämnade in JavaScript Quiz", Type = "Inlämning", Date = DateTime.Now.AddHours(-2), Duration = TimeSpan.Zero, Description = "Betyg: A" },
                new Activity { Id = 2, ModuleId = 2, Name = "Nytt material: React Hooks Guide", Type = "Dokument", Date = DateTime.Now.AddHours(-5), Duration = TimeSpan.Zero, Description = "Laddat upp av lärare" },
                new Activity { Id = 3, ModuleId = 1, Name = "Deadline närmar: React Todo App", Type = "Deadline", Date = DateTime.Now.AddDays(3), Duration = TimeSpan.Zero, Description = "Om 3 dagar" }
            };
        }
    }
}
