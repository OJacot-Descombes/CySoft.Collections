namespace UnitTests;

public abstract class CollectionTestBase<T>
{
    public abstract ICollection<T> CreateSut(int n);
    public abstract void AddPoints(ICollection<T> collection, int n);
    public abstract T CreateNonExistingItem();

    public virtual void CountAterCreation()
    {
        ICollection<T> sut = CreateSut(0);
        Assert.AreEqual(0, sut.Count);
    }

    public virtual void CountAterInsert()
    {
        ICollection<T> sut = CreateSut(100);
        Assert.AreEqual(100, sut.Count);
    }

    public virtual void CountAfterClear()
    {
        ICollection<T> sut = CreateSut(100);
        sut.Clear();
        Assert.AreEqual(0, sut.Count);
    }

    public virtual void CountAterClearAndReInsert()
    {
        ICollection<T> sut = CreateSut(100);
        sut.Clear();
        AddPoints(sut, 130);
        Assert.AreEqual(130, sut.Count);
    }

    public virtual void CountAfterRemove()
    {
        ICollection<T> sut = CreateSut(200);
        var toDelete = sut.Take(50);
        foreach (T item in toDelete) {
            sut.Remove(item);
        }
        Assert.AreEqual(150, sut.Count);
    }

    public virtual void CountAterRemoveAndReInsert()
    {
        ICollection<T> sut = CreateSut(200);
        var toDelete = sut.Take(50);
        foreach (T item in toDelete) {
            sut.Remove(item);
        }
        Assert.AreEqual(150, sut.Count, "After Remove");
        AddPoints(sut, 80);
        Assert.AreEqual(230, sut.Count, "After re-insert");
    }

    public virtual void Contains()
    {
        ICollection<T> sut = CreateSut(300);
        T item = sut.Skip(123).First();
        Assert.IsTrue(sut.Contains(item));
    }

    public virtual void ContainsNot()
    {
        ICollection<T> sut = CreateSut(300);
        _ = sut.Skip(123).First();
        T nonExisting = CreateNonExistingItem();
        Assert.IsFalse(sut.Contains(nonExisting));
    }
}
