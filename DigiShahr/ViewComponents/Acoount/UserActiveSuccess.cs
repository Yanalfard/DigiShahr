using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.Acoount
{
    public class UserActiveSuccess : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(bool Status)
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Account/Components/Success.cshtml", Status));
        }
    }
}
