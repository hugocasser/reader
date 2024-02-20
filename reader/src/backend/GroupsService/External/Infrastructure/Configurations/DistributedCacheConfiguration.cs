namespace Infrastructure.Configurations;

public static class DistributedCacheConfiguration
{
    public static TimeSpan CacheExpiryTime { get; set; } = TimeSpan.FromSeconds(120);
}