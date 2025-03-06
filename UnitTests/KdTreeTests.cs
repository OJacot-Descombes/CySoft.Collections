using CySoft.Collections.Geometry;

namespace UnitTests;

[TestClass]
public class KdTreeTests
{

    [TestMethod]
    public void TestCountNoSizeLimit()
    {
        const int N = 1000;

        var sut = Helpers.CreateTreeWithRandomPoints(N);
        Assert.AreEqual(N, sut.Count);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        const int N = 1000;

        KDTree<int> sut = Helpers.CreateTreeWithRandomPoints(N);
#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
        Assert.AreEqual(N, sut.Count());
#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
    }

    [TestMethod]
    public void TestEnumeratorOnEmptyTree()
    {
        KDTree<int> sut = Helpers.CreateTreeWithRandomPoints(0);
#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
        Assert.AreEqual(0, sut.Count());
#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
    }

    [TestMethod]
    public void OrderedNeighbours()
    {
        var sut = Helpers.CreateTreeWithRandomPoints(400);
        Neighbor<int>[] neighbours = sut.NearestNeighbors(SquareEuclideanDistance.Instance, 77, 0, 0).ToArray();
        Assert.AreEqual(77, neighbours.Length, "Count");
        for (int i = 1; i < neighbours.Length; i++) {
            if (neighbours[i - 1].Distance > neighbours[i].Distance) {
                Assert.Fail("Order");
            }
        }
    }

    [TestMethod, ExpectedException(typeof(InvalidOperationException))]
    public void InvalidUseOfAddThrowsException()
    {
        var sut = new KDTree<int>(2);
        sut.Add(0);
    }

    [TestMethod]
    public void GetNeighborsAfterRemoveAll()
    {
        const int N = 60;

        var sut = new KDTree<int>(2);
        for (int i = 0; i < N; i++) {
            sut.Add(i, i % 7, i % 13);
        }
        for (int i = 0; i < N; i++) {
            sut.Remove(i, i % 7, i % 13);
        }
        var items = sut.NearestNeighbors(5, 0, 0);
        Assert.AreEqual(0, items.Count());
    }

    [TestMethod]
    public void GetNeighborsAfterRemoveSome()
    {
        const int N = 60;

        var sut = new KDTree<int>(2);
        for (int i = 0; i < N; i++) {
            sut.Add(i, i + i % 7, i % 13 - i);
        }
        for (int i = 0; i < N; i += 2) {
            sut.Remove(i, i + i % 7, i % 13 - i);
        }
        var items = sut.NearestNeighbors(100, 0, 0);
        Assert.AreEqual(N / 2, items.Count());
    }
}
