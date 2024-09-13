namespace ArcadeVault.Application.Extensions;

internal static class EnumExtension
{
    /// <summary>
    /// Retrieves all values of a specified enum type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> GetAll<T>() where T : Enum
    {
        return (IEnumerable<T>)Enum.GetValues(typeof(T));
    }
}