namespace CySoft.Collections;

/// <summary>
/// Implements a simple cache.
/// </summary>
/// <remarks>
/// Copyright (C) 2012 Olivier Jacot-Descombes.
/// 2018 implemented periodic flushing of timed out entries.
/// </remarks>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
/// <remarks>
/// Creates a cache which holds the cached values for a limited time only.
/// </remarks>
/// <param name="maxCachingTime">Maximum time for which the a value is to be hold in the cache.</param>
public class Cache<TKey, TValue>(TimeSpan maxCachingTime)
{
    private Dictionary<TKey, CacheItem> _cache = new(100);
    private readonly TimeSpan _flushingTime = TimeSpan.FromTicks(maxCachingTime.Ticks / 5);
    private DateTime _lastFlushing = DateTime.Now;

    /// <summary>
    /// Creates a cache which holds the cached values for an infinite time.
    /// </summary>
    public Cache()
        : this(TimeSpan.MaxValue)
    {
    }

    /// <summary>
    /// Tries to get a value from the cache. If the cache contains the value and the maximum caching time is
    /// not exceeded (if any is defined), then the cached value is returned, else a new value is created.
    /// </summary>
    /// <param name="key">Key of the value to get.</param>
    /// <param name="createValue">Fuction creating a new value.</param>
    /// <returns>A cached or a new value.</returns>
    public TValue Get(TKey key, Func<TValue> createValue)
    {
        if (DateTime.Now - _lastFlushing > _flushingTime) {
            Flush();
        }
        if (_cache.TryGetValue(key, out CacheItem cacheItem) &&
            DateTime.Now - cacheItem.CacheTime <= maxCachingTime) {

            return cacheItem.Item;
        }
        TValue value = createValue();
        _cache[key] = new CacheItem(value);
        return value;
    }

    private void Flush()
    {
        var newCache = new Dictionary<TKey, CacheItem>(Math.Max(100, _cache.Count / 2));
        foreach (KeyValuePair<TKey, CacheItem> item in _cache) {
            if (DateTime.Now - item.Value.CacheTime < maxCachingTime) {
                newCache.Add(item.Key, item.Value);
            }
        }
        _cache = newCache;
        _lastFlushing = DateTime.Now;
    }

    private struct CacheItem
    {
        public CacheItem(TValue item)
            : this()
        {
            Item = item;
            CacheTime = DateTime.Now;
        }

        public TValue Item { get; private set; }
        public DateTime CacheTime { get; private set; }
    }
}
