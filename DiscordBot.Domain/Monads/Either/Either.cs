namespace DiscordBot.Domain.Monads.Either;

public readonly record struct Either<TLeft, TRight>
{
    public TLeft? LeftValue { get; }
    public TRight? RightValue { get; }
    public bool IsLeft { get; } = false;
    public bool IsRight => !IsLeft;

    private Either(TLeft left)
    {
        LeftValue = left;
        IsLeft = true;
    }

    private Either(TRight right)
    {
        RightValue = right;
    }

    public static Either<TLeft, TRight> Left(TLeft left) => new(left);
    public static Either<TLeft, TRight> Right(TRight right) => new(right);

    public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc)
    {
        return IsLeft
            ? leftFunc(LeftValue!)
            : rightFunc(RightValue!);
    }
}