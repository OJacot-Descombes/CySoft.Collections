using CySoft.Collections.Geometry.Implementation;

namespace UnitTests;

[TestClass]
public class PointCollectionTests : CollectionTestBase<double[]>
{
    public override ICollection<double[]> CreateSut(int n)
    {
        return Helpers.CreateTreeWithOrderedPoints(
            n,
            i => [i * i * Math.PI % 1.0, i * Math.E % 1.0]
        ).Points;
    }

    public override void AddPoints(ICollection<double[]> collection, int n)
    {
        var coll = (KDPointsCollection<int>)collection;
        for (int i = 0; i < n; i++) {
            coll.KDTree.Add(i + 1000);
        }
    }

    public override double[] CreateNonExistingItem()
    {
        return [7, 8];
    }

    [TestMethod]
    public void ReadOnly()
    {
        ICollection<double[]> sut = CreateSut(0);
        Assert.IsTrue(sut.IsReadOnly);
    }

    [TestMethod]
    public override void CountAterCreation()
    {
        base.CountAterCreation();
    }

    [TestMethod]
    public override void CountAterInsert()
    {
        base.CountAterInsert();
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public override void CountAfterClear()
    {
        base.CountAfterClear();
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public override void CountAterClearAndReInsert()
    {
        base.CountAterClearAndReInsert();
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public override void CountAfterRemove()
    {
        base.CountAfterRemove();
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public override void CountAterRemoveAndReInsert()
    {
        base.CountAterRemoveAndReInsert();
    }

    [TestMethod]
    public override void Contains()
    {
        base.Contains();
    }

    [TestMethod]
    public override void ContainsNot()
    {
        base.ContainsNot();
    }
}
