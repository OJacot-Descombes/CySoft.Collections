namespace UnitTests;

[TestClass]
public class ValueCollectionTests : CollectionTestBase<int>
{
    public override ICollection<int> CreateSut(int n)
    {
        return Helpers.CreateTreeWithOrderedPoints(
            n,
            i => [i * i * Math.PI % 1.0, i * Math.E % 1.0]
        ).Values;
    }

    public override void AddPoints(ICollection<int> collection, int n)
    {
        for (int i = 0; i < n; i++) {
            collection.Add(i + 1000);
        }
    }

    public override int CreateNonExistingItem()
    {
        return 1234567;
    }

    [TestMethod]
    public void ReadOnly()
    {
        ICollection<int> sut = CreateSut(0);
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
