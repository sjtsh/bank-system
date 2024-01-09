using Banking.Models;

namespace Banking.ViewModels
{
    public class AdminPageVM
    {
        public List<UserModel>? Users { get; set; }
        public List<BankModel>? Banks { get; set; }
    }
}
