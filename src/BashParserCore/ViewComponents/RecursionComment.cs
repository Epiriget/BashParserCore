using BashParserCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.ViewComponents
{
    public class RecursionComment : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Comment>comments, int parentId)
        {
            ViewBag.parentId = parentId;
            return View("~/Views/Shared/Components/RecursionComment.cshtml", comments);
        }
    }
}
