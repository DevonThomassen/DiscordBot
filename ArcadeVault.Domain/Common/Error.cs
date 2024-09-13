namespace ArcadeVault.Domain.Common;

public readonly record struct Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Error Conflict(
        string code = "Error.Conflict",
        string description = "A conflict error has occurred."
    ) => new(code, description, ErrorType.Conflict);

    public static Error Failure(
        string code = "Error.Failure",
        string description = "A failure error has occurred."
    ) => new(code, description, ErrorType.Failure);

    public static Error Forbidden(
        string code = "Error.Forbidden",
        string description = "A forbidden error has occurred."
    ) => new(code, description, ErrorType.Forbidden);

    public static Error NotFound(
        string code = "Error.NotFound",
        string description = "A not found error has occurred."
    ) => new(code, description, ErrorType.NotFound);

    public static Error Unauthorized(
        string code = "Error.Unauthorized",
        string description = "A unauthorized has occurred."
    ) => new(code, description, ErrorType.Unauthorized);

    public static Error Unexpected(
        string code = "Error.Unexpected",
        string description = "An unexpected error has occurred."
    ) => new(code, description, ErrorType.Unexpected);

    public static Error Validation(
        string code = "Error.Validation",
        string description = "A validation error has occurred."
    ) => new(code, description, ErrorType.Validation);

    public static Error Custom(
        string code,
        string description,
        ErrorType type
    ) => new(code, description, type);
}