using Banking.Models;

namespace Banking.ViewModels;

public class RegisterVM
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int BankId { get; set; }
    public List<BankModel>? Banks { get; set; }
}
