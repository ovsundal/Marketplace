﻿namespace Marketplace.Domain;

public record UserId
{
    private readonly Guid _value;

    public UserId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException(nameof(value), "User id cannot be empty");
        }

        _value = value;
    }
        public static implicit operator Guid(UserId self) => self._value;
}