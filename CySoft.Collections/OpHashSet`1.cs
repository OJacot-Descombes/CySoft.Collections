#nullable enable
using System.Diagnostics.CodeAnalysis;

namespace CySoft.Collections;

public class OpHashSet<T> : HashSet<T>
{
    public OpHashSet() { }

    public OpHashSet(IEqualityComparer<T>? comparer) : base(comparer) { }

    public OpHashSet(int capacity) : base(capacity, null) { }

    public OpHashSet(IEnumerable<T> collection) : base(collection, null) { }

    public OpHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer) : base(collection, comparer) { }

    public OpHashSet(int capacity, IEqualityComparer<T>? comparer) : base(capacity, comparer) { }

    /// <summary>
    /// Produces the union of sets. The original collections are not modified.
    /// </summary>
    /// <param name="a">A <c>OpHashSet&lt;T&gt;</c> whose elements form the first set for the union.</param>
    /// <param name="b">A sequence whose elements form the second set for the union.</param>
    /// <returns>A new <c>OpHashSet&lt;T&gt;</c> containing the set union of both collections.</returns>
    /// <remarks>The union of sets A and B is the set of all objects that are a member of A, or B, or both.</remarks>
    /// <example>The union {1, 2, 3, 4} | {3, 4, 5, 6} is the set {1, 2, 3, 4, 5, 6}.</example>
    public static OpHashSet<T> operator |(OpHashSet<T> a, IEnumerable<T> b)
    {
        var result = new OpHashSet<T>(a, a.Comparer);
        foreach (var item in b) {
            result.Add(item);
        }
        return result;
    }

    /// <summary>
    /// Produces the set intersection. The original collections are not modified.
    /// </summary>
    /// <param name="a">A <c>OpHashSet&lt;T&gt;</c> whose elements form the first set for the intersection.</param>
    /// <param name="b">A sequence whose elements form the second set for the intersection.</param>
    /// <returns>A new <c>OpHashSet&lt;T&gt;</c> containing the set intersection of both collections.</returns>
    /// <remarks>The intersection of sets A and B is the set of all objects that are members of both A and B.</remarks>
    /// <example>The intersection {1, 2, 3, 4} &amp; {3, 4, 5, 6} is the set {3, 4}.</example>
    public static OpHashSet<T> operator &(OpHashSet<T> a, IEnumerable<T> b)
    {
        var result = new OpHashSet<T>(a.Comparer);
        foreach (var item in b) {
            if (a.Contains(item)) {
                result.Add(item);
            }
        }
        return result;
    }

    /// <summary>
    /// Produces the set difference. The original collections are not modified.
    /// </summary>
    /// <param name="a">A sequence whose elements form the first set for the difference.</param>
    /// <param name="b">A <c>OpHashSet&lt;T&gt;</c> whose elements form the second set for the difference.</param>
    /// <returns>A new <c>OpHashSet&lt;T&gt;</c> containing the set difference of both collections.</returns>
    /// <remarks>The set difference of A and B is the set of all members of A that are not members of B.</remarks>
    /// <example>The set difference {1, 2, 3, 4} - {3, 4, 5, 6} is {1, 2}.</example>
    public static OpHashSet<T> operator -(IEnumerable<T> a, OpHashSet<T> b)
    {
        var result = new OpHashSet<T>(b.Comparer);
        foreach (var item in a) {
            if (!b.Contains(item)) {
                result.Add(item);
            }
        }
        return result;
    }

    /// <summary>
    /// Produces the symmetric set difference. The original collections are not modified.
    /// </summary>
    /// <param name="a">A <c>OpHashSet&lt;T&gt;</c> whose elements form the first set for the difference.</param>
    /// <param name="b">A <c>OpHashSet&lt;T&gt;</c> whose elements form the second set for the difference.</param>
    /// <returns>A new <c>OpHashSet&lt;T&gt;</c> containing the set difference of both collections.</returns>
    /// <remarks>The symmetric difference of sets A and B is the set of all objects that are a member of exactly one of A and B 
    /// (elements which are in one of the sets, but not in both).</remarks>
    /// <example>The symmetric difference set {1, 2, 3, 4} / {3, 4, 5, 6} is {1, 2, 5, 6}.</example>
    public static OpHashSet<T> operator /(OpHashSet<T> a, HashSet<T> b)
    {
        var result = new OpHashSet<T>(b.Comparer);
        foreach (var item in a) {
            if (!b.Contains(item)) {
                result.Add(item);
            }
        }
        foreach (var item in b) {
            if (!a.Contains(item)) {
                result.Add(item);
            }
        }
        return result;
    }

    /// <summary>
    /// Produces the Cartesian product of two sets A and B, which is the set of all ordered pairs (a, b) where a is in A and b is in B.
    /// </summary>
    /// <param name="a">First set.</param>
    /// <param name="b">Second set.</param>
    /// <returns>The Cartesian product as OpHashSet&lt;(T, T)&gt;</returns>
    /// <remarks>Both sets must have the same element type T.
    /// Use the <c>CartesianProduct</c> method instead to combine two sets with different element types.
    /// <p>Takes into account the EqualityComparers of both sets.</p>
    /// </remarks>
    /// <example>The Cartesian product {1, 2} * {3, 4} is the set {(1, 3), (1, 4), (2, 3), (2, 4)}</example>
    public static OpHashSet<(T, T)> operator *(OpHashSet<T> a, HashSet<T> b) => a.CartesianProduct(b);

    /// <summary>
    /// Produces the Cartesian product of two sets A and B, which is the set of all ordered pairs (a, b) where a is in A and b is in B.
    /// </summary>
    /// <param name="a">First set.</param>
    /// <param name="b">Second set.</param>
    /// <returns>The Cartesian product as OpHashSet&lt;(T, U)&gt;</returns>
    /// <remarks>Both sets can have different element types.
    /// Use can also use the * operator to combine two sets having the same element type.
    /// <p>Takes into account the EqualityComparers of both sets.</p>
    /// </remarks>
    /// <example>The Cartesian product {1, 2} * {'a', 'b'} is the set {(1, 'a'), (1, 'b'), (2, 'a'), (2, 'b')}</example>
    public OpHashSet<(T, U)> CartesianProduct<U>(HashSet<U> other)
    {
        var result = new OpHashSet<(T, U)>(new TupleEqualityComparer<U>(Comparer, other.Comparer));
        foreach (T t in this) {
            foreach (U u in other) {
                result.Add((t, u));
            }
        }
        return result;
    }

    private class TupleEqualityComparer<U>(IEqualityComparer<T> tComparer, IEqualityComparer<U> uComparer)
        : IEqualityComparer<(T, U)>
    {
        public bool Equals((T, U) x, (T, U) y)
            => tComparer.Equals(x.Item1, y.Item1) && uComparer.Equals(x.Item2, y.Item2);

        public int GetHashCode([DisallowNull] (T, U) obj)
        {
            unchecked {
                int hash = 527 + (obj.Item1 is null ? 0 : tComparer.GetHashCode(obj.Item1));
                hash = hash * 31 + (obj.Item2 is null ? 0 : uComparer.GetHashCode(obj.Item2));
                return hash;
            }
        }
    }
}
