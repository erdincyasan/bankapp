using Microsoft.AspNetCore.Mvc;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Mapping;
using Udemy.BankApp.Web.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Udemy.BankApp.Web.Data.UnitOfWork;

namespace Udemy.BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserMapper _userMapper;
        private readonly IAccountMapper _accountMapper;
        private readonly IUow _uow;

        public AccountController(IUserMapper userMapper, IAccountMapper accountMapper, IUow uow)
        {
            _userMapper = userMapper;
            _accountMapper = accountMapper;
            _uow = uow;
        }

        public IActionResult Create(int id)
        {
            return View(_userMapper.MaptoUserModel(_uow.GetRepository<ApplicationUser>().GetById(id)));
        }
        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            _uow.GetRepository<Account>().Create(_accountMapper.AccountCreateModelToAccount(model));
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult GetByUserId(int userId)
        {
            var accountList = _uow.GetRepository<Account>().GetQueryable().Where(x => x.ApplicationUserId == userId).ToList();
            var user = _uow.GetRepository<ApplicationUser>().GetById(userId);
            var accountListModel = new List<AccountListModel>();
            ViewBag.FullName = $"{user.Name} {user.SurName}";
            foreach (var account in accountList)
            {
                accountListModel.Add(new AccountListModel
                {
                    Id = account.Id,
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance,
                    ApplicationUserId = account.ApplicationUserId,
                });
            }
            return View(accountListModel);
        }
        [HttpGet]
        public IActionResult SendMoney(int accountId)
        {
            var query = _uow.GetRepository<Account>().GetQueryable();
            var accounts = query.Where(x => x.Id != accountId).ToList();
            var accountInf = _uow.GetRepository<Account>().GetById(accountId);
            ViewBag.Balance = accountInf.Balance;
            ViewBag.SenderId = accountId;
            var list = new List<AccountListModel>();
            foreach (var account in accounts)
            {
                list.Add(new AccountListModel
                {
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId = account.ApplicationUserId,
                    Balance = account.Balance,
                    Id = account.Id,
                });
            }

            return View(new SelectList(list, "Id", "AccountNumber"));
        }
        [HttpPost]
        public IActionResult SendMoney(SendMoneyModel model)
        {
            var senderAccount = _uow.GetRepository<Account>().GetById(model.SenderId);
            var receiverAccount = _uow.GetRepository<Account>().GetById(model.ReceiverId);
            senderAccount.Balance = senderAccount.Balance - model.Amount;
            receiverAccount.Balance = receiverAccount.Balance + model.Amount;
            _uow.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
