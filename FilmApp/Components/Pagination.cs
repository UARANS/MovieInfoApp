using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmApp.Components
{
    public class Pagination : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageViewModel result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
