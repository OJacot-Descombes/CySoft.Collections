using CySoft.Collections.Geometry;

namespace UnitTests;

[TestClass]
public class DistanceFunctionTests
{
    [TestMethod]
    public void SquareEuclidian()
    {
        var sut = SquareEuclideanDistance.Instance;
        Assert.AreEqual(25.0, sut.Distance([10, 20], [14, 23]));
    }

    [TestMethod]
    public void Manhattan()
    {
        var sut = ManhattanDistance.Instance;
        Assert.AreEqual(7.0, sut.Distance([10, 20], [14, 23]));
    }

    [TestMethod]
    public void Chebyshev()
    {
        var sut = ChebyshevDistance.Instance;
        Assert.AreEqual(4.0, sut.Distance([10, 20], [14, 23]));
    }

    [TestMethod]
    public void SquareEuclidianInverse()
    {
        var sut = SquareEuclideanDistance.Instance;
        Assert.AreEqual(25.0, sut.Distance([14, 23], [10, 20]));
    }

    [TestMethod]
    public void ManhattanInverse()
    {
        var sut = ManhattanDistance.Instance;
        Assert.AreEqual(7.0, sut.Distance([14, 23], [10, 20]));
    }

    [TestMethod]
    public void ChebyshevInverse()
    {
        var sut = ChebyshevDistance.Instance;
        Assert.AreEqual(4.0, sut.Distance([14, 23], [10, 20]));
    }
}
