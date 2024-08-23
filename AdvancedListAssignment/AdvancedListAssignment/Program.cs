using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedListAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task 1: Create a list of integers
            List<int> grades = new List<int> { 85, 92, 78, 64, 89 };

            // Task 2: Add elements
            grades.Add(95);
            grades.Add(70);

            // Task 3: Access elements
            Console.WriteLine("First grade: " + grades[0]);
            Console.WriteLine("Last grade: " + grades[grades.Count - 1]);

            // Task 4: Remove elements
            grades.Remove(78);
            grades.RemoveAt(2);

            // Print grades after removals
            Console.WriteLine("Grades after removals: " + string.Join(", ", grades));

            // Task 5: Iterate through the list
            Console.WriteLine("Grades (foreach loop):");
            foreach (int grade in grades)
            {
                Console.WriteLine(grade);
            }

            Console.WriteLine("Grades (for loop):");
            for (int i = 0; i < grades.Count; i++)
            {
                Console.WriteLine(grades[i]);
            }

            // Task 6: Common list methods
            Console.WriteLine("Number of grades: " + grades.Count);
            Console.WriteLine("Contains grade 85: " + grades.Contains(85));
            Console.WriteLine("Index of grade 89: " + grades.IndexOf(89));

            grades.Sort();
            Console.WriteLine("Sorted grades: " + string.Join(", ", grades));

            // Task 7: Practical example
            double average = CalculateAverage(grades);
            Console.WriteLine("Average grade: " + average);

            static double CalculateAverage(List<int> grades)
            {
                if (grades.Count == 0)
                    return 0;

                int sum = 0;
                foreach (int grade in grades)
                {
                    sum += grade;
                }
                return (double)sum / grades.Count;
            }
        }
    }
}