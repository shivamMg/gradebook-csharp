using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Shivam's book");
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var stats = book.GetStatistics();
            Console.WriteLine($"The highest grade is {stats.Highest}");
            Console.WriteLine($"The lowest grade is {stats.Lowest}");
            Console.WriteLine($"The average grade is {stats.Average}");
        }

        static void OnGradeAdded(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"Grade added: {e.Grade}");
        }

        private static void EnterGrades(IBook book)
        {
            var grades = new double[] { 89.1, 90.5, 77.5 };
            for (int i = 0; i < grades.Length; i++)
            {
                book.AddGrade(grades[i]);
            }
        }
    }
}

