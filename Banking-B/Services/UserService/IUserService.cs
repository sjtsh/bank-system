using Banking_B.Models;

namespace Banking_B.Services
{
    internal interface IUserService
    {
        /// <summary>   
        /// Get Users
        /// That is not deleted
        /// Sorted by recent activity
        /// </summary>
        /// <returns>
        /// 1. Users
        /// 2. Their Bank
        /// 3. Not Admin
        /// </returns>
         List<UserModel> GetUsers();

        /// <summary>
        /// Find user summary
        /// Might not return the user if phone not in use
        /// Wont return if the user has been deleted
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>Maybe User</returns>
        UserModel? FindUserByPhone(string phone);

        /// <summary>
        /// Creates user
        /// 1. Creates a £10000 sign-in bonus deposit
        /// 2. Unique indexing on account number
        /// 3. Unique indexing on emails
        /// </summary>
        /// <param name="information"></param>
        /// <returns>the created user</returns>
        UserModel CreateUser(UserModel information);

        /// <summary>
        /// Updates user
        /// Allowed by the admin or the user himself
        /// </summary>
        /// <param name="information"></param>
        /// <returns>the updated user</returns>
        UserModel? UpdateUser(UserModel information);

        /// <summary>
        /// Should provide the greatest present account number in the database
        /// </summary>
        /// <returns></returns>
        int? GetGreatestAccountNumber();

        /// <summary>
        /// Creates many users
        /// Only for seeding
        /// </summary>
        /// <returns>Number of users created</returns>
        int CreateUsers(string[] names);
    }
}
