using CUT_Burger.Interfaces;
using CUT_Burger.Models;

namespace CUT_Burger.Repositories;

public class UserRepo : IUser
{
    
    public UserRepo()
    {
    }
    public IEnumerable<User> GetUsers()
    {
        throw new NotImplementedException();
    }

    public User Details(int id)
    {
        throw new NotImplementedException();
    }

    public User Create(User user)
    {
        throw new NotImplementedException();
    }

    public User Edit(User user)
    {
        throw new NotImplementedException();
    }

    public User Delete(User user)
    {
        throw new NotImplementedException();
    }

    public bool IsExist(int id)
    {
        throw new NotImplementedException();
    }
}