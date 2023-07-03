using System.Collections.Generic;
using KeylaneTask.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class TriangleAnalyzerUnitTests
    {
        [TestMethod]
        public void TriangleWithArea6()
        {
            // Arrange
            var triangle = new Triangle(3, 4, 5);

            // Act

            
            // Assert
            Assert.AreEqual(triangle.Area, 6);
        }
        
        [TestMethod]
        public void EquilateralTriangle()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var triangle = new Triangle(3, 3, 3);

            // Act
            var triangleType = triangleAnalyzer.GetTriangleType(triangle);
            
            // Assert
            Assert.AreEqual(triangleType, TriangleType.Equilateral);
        }
        
        [TestMethod]
        public void IsoscelesTriangle()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var triangle = new Triangle(3, 4, 3);

            // Act
            var triangleType = triangleAnalyzer.GetTriangleType(triangle);
            
            // Assert
            Assert.AreEqual(triangleType, TriangleType.Isosceles);
        }
        
        [TestMethod]
        public void ScaleneTriangle()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var triangle = new Triangle(5, 4, 3);

            // Act
            var triangleType = triangleAnalyzer.GetTriangleType(triangle);
            
            // Assert
            Assert.AreEqual(triangleType, TriangleType.Scalene);
        }
        
        [TestMethod]
        public void InvalidTriangle_NegativeValue()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var triangle = new Triangle(-3, 4, 3);

            // Act
            var triangleType = triangleAnalyzer.GetTriangleType(triangle);
            
            // Assert
            Assert.AreEqual(triangleType, TriangleType.Invalid);
        }
        
        [TestMethod]
        public void InvalidTriangle_SumOfSidesTooLarge()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var triangle = new Triangle(6, 3, 3);

            // Act
            var triangleType = triangleAnalyzer.GetTriangleType(triangle);
            
            // Assert
            Assert.AreEqual(triangleType, TriangleType.Invalid);
        }
        
        [TestMethod]
        public void Create3Triangle_ShouldOnlyInsert1()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var testTriangles = new List<Triangle>
            {
                new Triangle(4, 4, 6),
                new Triangle(4, 4, 6),
                new Triangle(6, 4, 4),
            };

            // Act
            foreach (var triangle in testTriangles)
            {
                triangleAnalyzer.GetTriangleType(triangle);
            }
            var uniqueTriangles = triangleAnalyzer.GetAllTriangles();

            // Assert
            Assert.AreEqual(uniqueTriangles.Count, 1);
        }
        
        
        [TestMethod]
        public void TriangleSubset_Returns4Of5Triangles()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var testTriangles = new List<Triangle>
            {
                new Triangle(3,4,5),
                new Triangle(5,7,10),
                new Triangle(8,9,10),
                new Triangle(10,9,10),
                new Triangle(11,9,10),
            };
            var constraint = 100;
            
            // Act
            var optimalSubset = triangleAnalyzer.SubsetWithMaxAreaGivenConstraint(testTriangles, constraint);

            
            // Assert
            Assert.AreEqual(optimalSubset.Count, 4);
            Assert.IsTrue(optimalSubset.Contains(testTriangles[0]));
            Assert.IsTrue(!optimalSubset.Contains(testTriangles[1]));
            Assert.IsTrue(optimalSubset.Contains(testTriangles[2]));
            Assert.IsTrue(optimalSubset.Contains(testTriangles[3]));
            Assert.IsTrue(optimalSubset.Contains(testTriangles[4]));
        }
        
        [TestMethod]
        public void TriangleSubset_ZeroConstraint_Returns0Of5Triangles()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var testTriangles = new List<Triangle>
            {
                new Triangle(3,4,5),
                new Triangle(5,7,10),
                new Triangle(8,9,10),
                new Triangle(10,9,10),
                new Triangle(11,9,10),
            };
            
            var constraint = 0;
            
            // Act
            var optimalSubset = triangleAnalyzer.SubsetWithMaxAreaGivenConstraint(testTriangles, constraint);

            
            // Assert
            Assert.AreEqual(optimalSubset.Count, 0);
        }
        
        [TestMethod]
        public void TriangleSubset_NegativeConstraint_Returns0Of5Triangles()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var testTriangles = new List<Triangle>
            {
                new Triangle(3,4,5),
                new Triangle(5,7,10),
                new Triangle(8,9,10),
                new Triangle(10,9,10),
                new Triangle(11,9,10),
            };
            
            var constraint = -100;
            
            // Act
            var optimalSubset = triangleAnalyzer.SubsetWithMaxAreaGivenConstraint(testTriangles, constraint);

            
            // Assert
            Assert.AreEqual(optimalSubset.Count, 0);
        }
        
        [TestMethod]
        public void TriangleSubset_EmptyList_Returns0Triangles()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var testTriangles = new List<Triangle>();
            
            var constraint = 100;
            
            // Act
            var optimalSubset = triangleAnalyzer.SubsetWithMaxAreaGivenConstraint(testTriangles, constraint);

            
            // Assert
            Assert.AreEqual(optimalSubset.Count, 0);
        }
        
        
        [TestMethod]
        public void TriangleSubset_SameTriangle_Returns2Triangles()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var testTriangles = new List<Triangle>
            {
                new Triangle(3,4,5),
                new Triangle(3,4,5),
                new Triangle(3,4,5),
                new Triangle(3,4,5),
                new Triangle(3,4,5),
                new Triangle(3,4,5),
            };
            
            var constraint = 30;
            
            // Act
            var optimalSubset = triangleAnalyzer.SubsetWithMaxAreaGivenConstraint(testTriangles, constraint);

            
            // Assert
            Assert.AreEqual(optimalSubset.Count, 2);
        }
        
        
        [TestMethod]
        public void TriangleSubset_InvalidTriangle_Returns0Triangles()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var testTriangles = new List<Triangle>
            {
                new Triangle(10,4,5),
            };
            
            var constraint = 30;
            
            // Act
            var optimalSubset = triangleAnalyzer.SubsetWithMaxAreaGivenConstraint(testTriangles, constraint);

            
            // Assert
            Assert.AreEqual(optimalSubset.Count, 0);
        }
        
        [TestMethod]
        public void TriangleSubset_ValidAndInvalidTriangle_Returns0Triangles()
        {
            // Arrange
            var triangleAnalyzer = new TriangleAnalyzer();
            var testTriangles = new List<Triangle>
            {
                new Triangle(10,4,5),
                new Triangle(8.9,4,5),
            };
            
            var constraint = 30;
            
            // Act
            var optimalSubset = triangleAnalyzer.SubsetWithMaxAreaGivenConstraint(testTriangles, constraint);

            
            // Assert
            Assert.AreEqual(optimalSubset.Count, 1);
        }
    }
}
