using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> users = [];

    public User? GetUserByEmail(string email)
    {
        return users.FirstOrDefault(u => u.Email == email);
    }

    public void Add(User user)
    {
        users.Add(user);
    }
}