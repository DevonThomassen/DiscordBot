namespace ArcadeVault.Domain.Common;

internal static class EmptyError
{
    public static IReadOnlyList<Error> Instance { get; } = [];
}