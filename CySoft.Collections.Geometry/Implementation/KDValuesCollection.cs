namespace CySoft.Collections.Geometry.Implementation;

public class KDValuesCollection<T> : KDCollectionBase<T, T>, ICollection<T>
{
    internal KDValuesCollection(KDTree<T> kdTree)
        : base(kdTree)
    {
    }

    #region ICollection<T> Members

    public void Add(T value)
    {
        _kdTree.Add(value);
    }

    public void Clear()
    {
        _kdTree.Clear();
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public bool Remove(T value)
    {
        return _kdTree.Remove(value);
    }

    #endregion

    #region IEnumerable<T> Members

    public override IEnumerator<T> GetEnumerator()
    {
        var stack = new Stack<KDNode<T>>();
        stack.Push(_kdTree);
        do {
            KDNode<T> current = stack.Pop();
            if (current._leafEntries == null) {
                stack.Push(current._left);
                stack.Push(current._right);
            } else {
                for (int i = 0; i < current._count; i++) {
                    yield return current._leafEntries[i].Value;
                }
            }
        } while (stack.Count > 0);
    }

    #endregion
}
