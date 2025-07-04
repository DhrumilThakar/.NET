using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseRegistrationSystem
{
    class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public List<int> RegisteredCourseIds { get; set; }

        public Student(int studentId, string name)
        {
            StudentId = studentId;
            Name = name;
            RegisteredCourseIds = new List<int>();
        }
    }

    class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<int> RegisteredStudentIds { get; set; }

        public Course(int courseId, string courseName)
        {
            CourseId = courseId;
            CourseName = courseName;
            RegisteredStudentIds = new List<int>();
        }
    }

    class CourseRegistrationSystem
    {
        private List<Student> students = new List<Student>();
        private List<Course> courses = new List<Course>();

        public void AddStudent()
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid ID. Try again.");
                return;
            }
            if (students.Any(s => s.StudentId == studentId))
            {
                Console.WriteLine("Student ID already exists.");
                return;
            }
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();
            students.Add(new Student(studentId, name));
            Console.WriteLine("Student added successfully.\n");
        }

        public void AddCourse()
        {
            Console.Write("Enter Course ID: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid ID. Try again.");
                return;
            }
            if (courses.Any(c => c.CourseId == courseId))
            {
                Console.WriteLine("Course ID already exists.");
                return;
            }
            Console.Write("Enter Course Name: ");
            string name = Console.ReadLine();
            courses.Add(new Course(courseId, name));
            Console.WriteLine("Course added successfully.\n");
        }

        public void RegisterStudentToCourse()
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid Student ID.");
                return;
            }
            var student = students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter Course ID: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID.");
                return;
            }
            var course = courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (student.RegisteredCourseIds.Count >= 4)
            {
                Console.WriteLine("Student already registered in 4 courses.");
                return;
            }
            if (course.RegisteredStudentIds.Count >= 10)
            {
                Console.WriteLine("Course already has 10 students.");
                return;
            }
            if (student.RegisteredCourseIds.Contains(courseId))
            {
                Console.WriteLine("Student already registered in this course.");
                return;
            }
            student.RegisteredCourseIds.Add(courseId);
            course.RegisteredStudentIds.Add(studentId);
            Console.WriteLine("Registration successful.\n");
        }

        public void ViewCourseRegistrations()
        {
            foreach (var course in courses)
            {
                Console.WriteLine($"Course ID: {course.CourseId}, Course Name: {course.CourseName}");
                if (course.RegisteredStudentIds.Count == 0)
                {
                    Console.WriteLine("  No students registered.");
                }
                else
                {
                    foreach (var studentId in course.RegisteredStudentIds)
                    {
                        var student = students.FirstOrDefault(s => s.StudentId == studentId);
                        if (student != null)
                        {
                            Console.WriteLine($"  Student ID: {student.StudentId}, Name: {student.Name}");
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CourseRegistrationSystem crs = new CourseRegistrationSystem();
            while (true)
            {
                Console.WriteLine("--- Student Course Registration System ---");
                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. Add New Course");
                Console.WriteLine("3. Register Student to Course");
                Console.WriteLine("4. View Course Registrations");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                string input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1":
                        crs.AddStudent();
                        break;
                    case "2":
                        crs.AddCourse();
                        break;
                    case "3":
                        crs.RegisterStudentToCourse();
                        break;
                    case "4":
                        crs.ViewCourseRegistrations();
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.\n");
                        break;
                }
            }
        }
    }
}