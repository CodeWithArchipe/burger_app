using CUT_Burger.Models;

namespace CUT_Burger.Interfaces;


    public interface IUser
    {
        IEnumerable<User> GetUsers();
        User Details(int userId);
        User Create(User user);
        User Edit(User user);
        User Delete(User user);
        bool IsExist(int userId);
    }
