using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Mapping
{
    public class AccountMapper : IAccountMapper
    {
        public Account AccountCreateModelToAccount(AccountCreateModel model)
        {
            return new Account 
            { 
                AccountNumber = model.AccountNumber ,
                Balance=model.Balance,
                ApplicationUserId= model.ApplicationUserId ,
            
            };
        }
    }
}
