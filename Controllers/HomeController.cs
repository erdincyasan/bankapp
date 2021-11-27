using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Interfaces;
using Udemy.BankApp.Web.Data.UnitOfWork;
using Udemy.BankApp.Web.Mapping;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserMapper _userMapper;
        private readonly IUow _uow;
        public HomeController(IUserMapper userMapper, IUow uow)
        {
            _userMapper = userMapper;
            _uow = uow;
        }
        public IActionResult Index()
        {
            return View(_userMapper.MapToUserList(_uow.GetRepository<ApplicationUser>().GetAll()));
        }
    }
}
