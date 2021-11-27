using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;

namespace Udemy.BankApp.Web.TagHelpers
{
    [HtmlTargetElement("getAccountCount")]
    public class GetBankAccountCount : TagHelper
    {
        private readonly AppDbContext _context;
        public int ApplicationUserId { get; set; }
        public GetBankAccountCount(AppDbContext context)
        {
            _context = context;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var accountCount = _context.Accounts.Count(x=>x.ApplicationUserId==ApplicationUserId);
            var html = $"<span class='badge bg-danger'>{accountCount}</span>";
            output.Content.SetHtmlContent(html);
            base.Process(context, output);
        }
    }
}
