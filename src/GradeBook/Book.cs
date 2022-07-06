using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection.Metadata;

namespace GradeBook
{
    public class CustomEventArgs: EventArgs
    {
        public readonly double Grade;

        public CustomEventArgs(double grade)
        {
            Grade = grade;
        }
    }

    public delegate void GradeAddedDelegate(object sender, CustomEventArgs e);
    
    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name; 
        }

        public string Name
        {
            get;
            set;  // prefix with private to make Name read-only
        
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract void AddGrade(double grade);
        
        public virtual Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
        
        public abstract event GradeAddedDelegate? GradeAdded;
    }

    public class DiskBook: Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate? GradeAdded;

        public override void AddGrade(double grade)
        {
            using(var writer = File.AppendText($"{Name}.txt"))  // writer.Dispose() is called in case of exception
            {
                writer.WriteLine(grade);   
                if (GradeAdded != null)
                {
                    GradeAdded(this, new CustomEventArgs(grade));
                }
            }
        }

        public override Statistics GetStatistics()
        {
            var stats = new Statistics();
            
            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    stats.Add(number);
                    line = reader.ReadLine();
                }
            }

            return stats;
        }
    }

    public class InMemoryBook : Book
    {
        private List<double> grades;
        
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new CustomEventArgs(grade));
                }
            }
            else
            {
                throw new ArgumentException("Invalid grade");
            }
        }

        public override event GradeAddedDelegate? GradeAdded;  // ? makes GradeAdded a nullable

        public override Statistics GetStatistics()
        {
            var stats = new Statistics();
            
            foreach (var grade in grades)
            {
                stats.Add(grade);
            }
            return stats;
        }

        public void Print()
        {
            Console.Write($"{Name}: ");
            foreach (double grade in grades)
            {
                Console.Write($"{grade} ");
            }
            Console.Write("\n");
        }
    }
}