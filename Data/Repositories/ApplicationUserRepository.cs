using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Interfaces;

namespace Udemy.BankApp.Web.Data.Repositories
{
    public class ApplicationUserRepository:IApplicationUserRepository
    {
        private readonly AppDbContext _context;

        public ApplicationUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ApplicationUser> GetAllUser()
        {
            return _context.ApplicationUsers.ToList();
        }

        public ApplicationUser GetUserById(int id)
        {
            return _context.ApplicationUsers.SingleOrDefault(u => u.Id == id);
        }
    }
}
