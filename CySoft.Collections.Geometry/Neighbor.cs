namespace CySoft.Collections.Geometry;

/// <summary>
/// Stores a distance and value to output.
/// </summary>
/// <typeparam name="T">Value type</typeparam>
public class Neighbor<T> : KDEntry<T>
{
    private readonly double _distance;
    public double Distance { get { return _distance; } }

    internal Neighbor(double distance, KDEntry<T> entry)
        : base(entry)
    {
        _distance = distance;
    }

    public override string ToString()
    {
        Type t = typeof(Neighbor<T>);
        return String.Format("{0}<{1}> [{2:0.0000}, {3}]",
            t.Name.Remove(t.Name.Length - 2, 2),
            t.GetGenericArguments()[0].Name,
            _distance,
            _value);
    }

    public override bool Equals(object obj)
    {
        if (obj is not Neighbor<T> other) {
            return false;
        }
        return _distance == other._distance && base.Equals(other);
    }

    public override int GetHashCode()
    {
        return _distance.GetHashCode() ^ base.GetHashCode();
    }
}
