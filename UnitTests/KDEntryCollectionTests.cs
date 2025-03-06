using CySoft.Collections.Geometry;

namespace UnitTests;

[TestClass]
public class KDEntryCollectionTests : CollectionTestBase<KDEntry<int>>
{
    public override ICollection<KDEntry<int>> CreateSut(int n)
    {
        return Helpers.CreateTreeWithOrderedPoints(
            n,
            i => [i * i * Math.PI % 1.0, i * Math.E % 1.0]
        );
    }

    public override void AddPoints(ICollection<KDEntry<int>> collection, int n)
    {
        var coll = (KDTree<int>)collection;
        for (int i = 0; i < n; i++) {
            coll.Add(i + 1000);
        }
    }

    public override KDEntry<int> CreateNonExistingItem()
    {
        return new KDEntry<int>(7777777, [7, 8]);
    }

    [TestMethod]
    public void ReadOnly()
    {
        ICollection<KDEntry<int>> sut = CreateSut(0);
        Assert.IsFalse(sut.IsReadOnly);
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

    [TestMethod]
    public override void CountAfterClear()
    {
        base.CountAfterClear();
    }

    [TestMethod]
    public override void CountAterClearAndReInsert()
    {
        base.CountAterClearAndReInsert();
    }

    [TestMethod]
    public override void CountAfterRemove()
    {
        base.CountAfterRemove();
    }

    [TestMethod]
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
