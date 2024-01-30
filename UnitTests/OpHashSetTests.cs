using CySoft.Collections;

namespace UnitTests;

#pragma warning disable CA1861 // Avoid constant arrays as arguments

[TestClass]
public class OpHashSetTests
{

    [TestMethod]
    public void Union()
    {
        // Arrange
        OpHashSet<int> a = [1, 2, 3, 4];
        OpHashSet<int> b = [3, 4, 5, 6];

        // Act
        OpHashSet<int> c = a | b;

        // Assert
        CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 4, 5, 6 }, c.ToList());
    }

    [TestMethod]
    public void Intersection()
    {
        // Arrange
        OpHashSet<int> a = [1, 2, 3, 4];
        OpHashSet<int> b = [3, 4, 5, 6];

        // Act
        OpHashSet<int> c = a & b;

        // Assert
        CollectionAssert.AreEquivalent(new[] { 3, 4 }, c.ToList());
    }

    [TestMethod]
    public void Difference()
    {
        // Arrange
        OpHashSet<int> a = [1, 2, 3, 4];
        OpHashSet<int> b = [3, 4, 5, 6];

        // Act
        OpHashSet<int> c = a - b;

        // Assert
        CollectionAssert.AreEquivalent(new[] { 1, 2 }, c.ToList());
    }

    [TestMethod]
    public void SymmetricDifference()
    {
        // Arrange
        OpHashSet<int> a = [1, 2, 3, 4];
        OpHashSet<int> b = [3, 4, 5, 6];

        // Act
        OpHashSet<int> c = a / b;

        // Assert
        CollectionAssert.AreEquivalent(new[] { 1, 2, 5, 6 }, c.ToList());
    }

    [TestMethod]
    public void CartesianProductOperator()
    {
        // Arrange
        OpHashSet<int> a = [1, 2];
        OpHashSet<int> b = [3, 4];

        // Act
        OpHashSet<(int, int)> c = a * b;

        // Assert
        CollectionAssert.AreEquivalent(new[] { (1, 3), (1, 4), (2, 3), (2, 4) }, c.ToList());
    }

    [TestMethod]
    public void CartesianProductMethod()
    {
        // Arrange
        OpHashSet<int> a = [1, 2];
        OpHashSet<char> b = ['a', 'b'];

        // Act
        OpHashSet<(int, char)> c = a.CartesianProduct(b);

        // Assert
        CollectionAssert.AreEquivalent(new[] { (1, 'a'), (1, 'b'), (2, 'a'), (2, 'b') }, c.ToList());
    }
}