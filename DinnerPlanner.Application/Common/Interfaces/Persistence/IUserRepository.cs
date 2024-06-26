﻿using DinnerPlanner.Domain.Entities;

namespace DinnerPlanner.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);

    void AddUser(User user);

    bool ExistsByEmail(string email);
}