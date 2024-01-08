using Microsoft.EntityFrameworkCore;
using Banking.Models;
using System.Collections.Generic;

namespace Banking.Services
{
    public class UserService(Context context) : IUserService
    {
        UserModel? IUserService.FindUserByPhone(string phone)
        {
            return context.Users.Where(user => user.PhoneNumber == phone && !user.IsDeleted).FirstOrDefault();
        }

        UserModel IUserService.CreateUser(UserModel user)
        {
            context.Add(user);
            context.SaveChanges();
            return user;
        }

        List<UserModel> IUserService.GetUsers()
        {
            return [.. context.Users.Where(user => !user.IsDeleted && !user.IsAdmin).Include(user => user.Bank)];
        }

        UserModel? IUserService.UpdateUser(UserModel information)
        {
            UserModel? user = context.Users.Where(element => information.Id == element.Id).FirstOrDefault();
            if (user == null) return user;
            user.FirstName = information.FirstName;
            user.MiddleName = information.MiddleName;
            user.LastName = information.LastName;
            user.SetEmail(information.GetEmail());
            user.IsDeleted = information.IsDeleted;
            context.SaveChanges();
            return user;
        }

        int? IUserService.GetGreatestAccountNumber()
        {
            return context.Users.Max(element => element.AccountNumber);
        }
    }
}
