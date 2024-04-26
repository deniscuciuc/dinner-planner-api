using DinnerPlanner.Application.Common.Interfaces.Persistence;
using DinnerPlanner.Domain.Entities;

namespace DinnerPlanner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    // static because this service is registered as a scoped
    // (so for each request will be created one separate repo)
    private static readonly List<User> Users = [];

    public User? GetUserByEmail(string email)
    {
        return Users.SingleOrDefault(u => u.Email == email);
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }
}