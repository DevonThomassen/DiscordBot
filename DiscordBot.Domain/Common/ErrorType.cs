﻿namespace DiscordBot.Domain.Common;

public enum ErrorType
{
    Conflict,
    Failure,
    Forbidden,
    NotFound,
    Unauthorized,
    Unexpected,
    Validation
}