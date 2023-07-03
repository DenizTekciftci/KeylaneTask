using System;
using System.Collections.Generic;
using System.Linq;

namespace KeylaneTask.Definitions
{
    public class TriangleAnalyzer
    {
        private readonly HashSet<Triangle> _hashSet = new HashSet<Triangle>();
        
        /// <summary>
        /// Returns the type of the given triangle construction
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        public TriangleType GetTriangleType(Triangle triangle)
        {
            if (!IsValid(triangle))
            {
                return TriangleType.Invalid;
            }

            _hashSet.Add(triangle);
            
            // If a is the same as b anc c the triangle is equilateral
            if (Math.Abs(triangle.a - triangle.b) < Globals.TOLERANCE && Math.Abs(triangle.a - triangle.c) < Globals.TOLERANCE)
            {
                return TriangleType.Equilateral;
            }
            
            // If a equals b or c, or b equals c, the triangle is isosceles
            if (Math.Abs(triangle.a - triangle.b) < Globals.TOLERANCE ||
                Math.Abs(triangle.a - triangle.c) < Globals.TOLERANCE ||
                Math.Abs(triangle.b - triangle.c) < Globals.TOLERANCE)
            {
                return TriangleType.Isosceles;
            }
            
            return TriangleType.Scalene;
        }
        
        /// <summary>
        /// Returns true if given triangle construction produces a valid triangle
        /// false otherwise
        /// </summary>
        /// <param name="triangle"></param>
        public bool IsValid(Triangle triangle)
        {
            // All sides must be strictly positive
            if (triangle.a < 0 || triangle.b < 0 || triangle.c < 0)
            {
                return false;
            }
            
            // If longest side exceeds sum of remaining sides it cannot be constructed
            if (triangle.c + triangle.b <= triangle.a)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Returns all seen valid triangles
        /// </summary>
        public List<Triangle> GetAllTriangles()
        {
            return _hashSet.ToList();
        }
        
        /// <summary>
        /// Returns the subset of triangles with maximal sum of areas given constraint on sum of perimeters
        /// 0/1 knapsack problem
        /// Dynamic programming solution
        /// </summary>
        /// <param name="triangles"></param>
        /// <param name="constraint"></param>
        public List<Triangle> SubsetWithMaxAreaGivenConstraint(List<Triangle> triangles, double constraint)
        {
            var consideredTriangles = triangles.Where(IsValid).ToList();
            
            // Only keep valid triangles
            for (int i = 0; i < consideredTriangles.Count; i++)
            {
                if (!IsValid(consideredTriangles[i]))
                {
                    consideredTriangles.RemoveAt(i);
                    i--;
                }
            }

            // Always begin with empty set
            var allCombination = new List<List<(double, double)>>
            {
                new List<(double, double)>
                {
                    (0,0)
                }
            };
            
            // Add one triangle at a time to considered combinations
            foreach(var triangle in consideredTriangles)
            {
                var listOfCombinations = new List<(double, double)>();
                var prev = allCombination.Last();
                
                foreach (var pre in prev)
                {
                    // Only add combinations adhering to constraint
                    var newArea = pre.Item1 + triangle.Area;
                    var newPerimeter = pre.Item2 + triangle.Perimeter;

                    if (newPerimeter < constraint)
                    {
                        listOfCombinations.Add((newArea, newPerimeter));
                    }
                }
                
                listOfCombinations.AddRange(prev);
                listOfCombinations.Sort((x,y) => x.Item1.CompareTo(y.Item1));
                
                var previousPair = (0.0, 0.0);
                for(int i = 1; i < listOfCombinations.Count; i++)
                {
                    var currentPair = listOfCombinations[i];
                    
                    // If the previous pair is dominated by the current pair, remove it
                    if (previousPair.Item1 <= currentPair.Item1 && previousPair.Item2 > currentPair.Item2)
                    {
                        listOfCombinations.RemoveAt(i - 1);
                        i--;
                    }
                    
                    previousPair = currentPair;
                }
                
                allCombination.Add(listOfCombinations);
            }


            var solution = allCombination.Last().Last();
            // Figure out which triangles compose the solution
            var optimalTriangleSet = new List<Triangle>();
            
            for (int i = allCombination.Count - 1; i >= 0; i--)
            {
                var currentRow = allCombination[i];
                if (!currentRow.Contains(solution))
                {
                    optimalTriangleSet.Add(consideredTriangles[i]);
                    
                    // Due to floating point comparisons
                    // next solution is found by subtracting the added triangle from the previous solution and finding 
                    // the pair absolutely closest to this
                    solution = currentRow.OrderBy(p =>
                        Math.Abs(p.Item1 - (solution.Item1 - consideredTriangles[i].Area)) + 
                        Math.Abs(p.Item2 - (solution.Item2 - consideredTriangles[i].Perimeter))).First();
                }
            }
            
            return optimalTriangleSet;
        }
    }
}