using System.Collections.Generic;
using KeylaneTask.Definitions;

namespace KeylaneTask
{
    internal class Program
    {
        public static void Main()
        {
            var triangleAnalyzer = new TriangleAnalyzer();

            var testTriangles = new List<Triangle>
            {
                new Triangle(3,4,5),
                new Triangle(5,7,10),
                new Triangle(8,9,10),
                new Triangle(10,9,10),
                new Triangle(11,9,10),
            };

            foreach (var triangle in testTriangles)
            {
                triangleAnalyzer.GetTriangleType(triangle);
            }
            
            var triangles = triangleAnalyzer.GetAllTriangles();
            
            var constraint = 100;
            var maxAreaSubset = triangleAnalyzer.SubsetWithMaxAreaGivenConstraint(testTriangles, constraint);
        }
    }
}
