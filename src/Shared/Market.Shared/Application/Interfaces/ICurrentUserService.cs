﻿namespace Market.Shared.Application.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }

    bool IsAdmin();

}