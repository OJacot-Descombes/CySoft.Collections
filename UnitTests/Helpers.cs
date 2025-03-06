using CySoft.Collections.Geometry;

namespace UnitTests;

internal static class Helpers
{
    public static KDTree<int> CreateTreeWithRandomPoints(int n)
    {
        var rand = new Random(0);
        KDTree<int> kdTree;
        kdTree = new KDTree<int>(2);
        for (int i = 0; i < n; i++) {
            kdTree.Add(i, rand.NextDouble(), rand.NextDouble());
        }
        return kdTree;
    }

    public static KDTree<int> CreateTreeWithOrderedPoints(int n, Func<int, double[]> singlePointAccessor)
    {
        KDTree<int> kdTree;
        kdTree = new KDTree<int>(2, singlePointAccessor);
        for (int i = 0; i < n; i++) {
            kdTree.Add(i);
        }
        return kdTree;
    }
}
