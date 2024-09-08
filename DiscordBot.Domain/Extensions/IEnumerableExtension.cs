namespace DiscordBot.Domain.Extensions;

public static class EnumerableExtension
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/>.
    /// <source>https://stackoverflow.com/questions/8582344/does-c-sharp-have-isnullorempty-for-list-ienumerable</source>
    /// </summary>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
    {
        return enumerable switch
        {
            null => true,
            // If this is a list, use the Count property for efficiency.
            // The Count property is O(1) while IEnumerable.Count() is O(N).
            ICollection<T> collection => collection.Count == 0,
            IReadOnlyList<T> list => list.Count == 0,
            _ => !enumerable.GetEnumerator().MoveNext()
        };
    }
}