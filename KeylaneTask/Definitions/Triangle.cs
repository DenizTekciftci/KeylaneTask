using System;
using System.Collections.Generic;

namespace KeylaneTask.Definitions
{
    public class Triangle
    {
        public double Area { get; }
        public double Perimeter { get; }
        
        public double a { get; }
        public double b { get; }
        public double c { get; }
        
        public Triangle(double s1, double s2, double s3)
        {
            // Create list to order sides by size
            var listOfSides = new List<double>
            {
                s1, s2, s3
            };
            
            listOfSides.Sort();
            
            a = listOfSides[2];
            b = listOfSides[1];
            c = listOfSides[0];
            
            // Heron's formula
            // sqrt(s * (s - a)(s - b)(s - c))
            var s = (a + b + c) / 2;
            Area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));

            Perimeter = a + b + c;
        }
        
        public override bool Equals(object obj) { 
            var other = obj as Triangle;
            if (other == null) {
                return false;
            }
            return Math.Abs(a - other.a) < Globals.TOLERANCE &&
                   Math.Abs(b - other.b) < Globals.TOLERANCE &&
                   Math.Abs(c - other.c) < Globals.TOLERANCE;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(a, b, c);
        }
    }
}