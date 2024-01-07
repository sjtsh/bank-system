using Microsoft.EntityFrameworkCore;
using Banking.Models;
using System.Collections.Generic;

namespace Banking.Services
{
    public class UserService : IUserService
    {
         
        UserModel? IUserService.FindUserByPhone(string phone)
        {

            using Context context = Context.Get();
            { 
                return context.Users.Where(user => user.PhoneNumber == phone && !user.IsDeleted).FirstOrDefault();
            }
        }

        UserModel IUserService.CreateUser(UserModel user)
        {
            using Context context = Context.Get();
            context.Add(user);
            return user;
        }

        List<UserModel> IUserService.GetUsers()
        {
            using Context context = Context.Get();
              return [.. context.Users.Where(user => !user.IsDeleted && !user.IsAdmin).Include(user => user.Bank)];
        }

        UserModel? IUserService.UpdateUser(UserModel information)
        {
            using Context context = Context.Get();
            {
                UserModel? user = context.Users.Where(element => information.Id == element.Id).FirstOrDefault();
                if (user == null) return user;
                user.FirstName = information.FirstName;
                user.MiddleName = information.MiddleName;
                user.LastName = information.LastName;
                user.Email = information.Email;
                user.IsDeleted = information.IsDeleted;
                return user;
            }
        }

        int? IUserService.GetGreatestAccountNumber()
        {
            using Context context = Context.Get();
            return context.Users.Max(element => element.AccountNumber);

        }

        int IUserService.CreateUsers(UserModel[] users)
        {
            return 0;
            //using Context context = Context.Get();
            //int count=   context.AddRange(users);
            //return count;
        }
    }
}
